using System;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;
using System.Threading.Tasks; // Required for async methods

namespace DevGenie
{
    public partial class Review : Page
    {
        protected System.Web.UI.WebControls.Literal reviewOutput;
        protected System.Web.UI.WebControls.TextBox code;
        protected System.Web.UI.WebControls.TextBox explanation;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is authenticated
            AuthUtility.EnsureAuthenticated();
        }

        protected async void ReviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure both code and reviewOutput have valid content before proceeding
                if (string.IsNullOrWhiteSpace(code.Text))
                {
                    reviewOutput.Text = "<div class='error-message'>Error: Code input is empty.</div>";
                    return;
                }

                // Generate AI review (assuming an AI-based service handles this)
                using var openAIService = new OpenAIService();
                string reviewText = await openAIService.ReviewCodeAsync(code.Text.Trim(), "any");

                if (string.IsNullOrWhiteSpace(reviewText))
                {
                    reviewOutput.Text = "<div class='error-message'>Error: Review generation failed.</div>";
                    return;
                }

                // Format and display the review
                reviewOutput.Text = FormatReviewWithCodeBlocks(reviewText);

                // Save to history
                await SaveToHistoryAsync(code.Text, reviewOutput.Text);

                // Register JavaScript to apply syntax highlighting after update
                ScriptManager.RegisterStartupScript(this, GetType(), "highlight", "initHighlighting();", true);
            }
            catch (Exception ex)
            {
                reviewOutput.Text = $"<div class='error-message'>Error: {HttpUtility.HtmlEncode(ex.Message)}</div>";
            }
        }

        private async Task SaveToHistoryAsync(string codeSubmitted, string reviewComments)
        {
            string userId = AuthUtility.GetCurrentUserId();
            string connString = "Data Source=DESKTOP-P70QF2S;Initial Catalog=devgenie;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    await conn.OpenAsync();

                    string query = @"INSERT INTO ReviewHistory (UserId, CodeSubmitted, ReviewComments, CreatedAt) 
                                     VALUES (@UserId, @CodeSubmitted, @ReviewComments, @CreatedAt)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@CodeSubmitted", codeSubmitted);
                        cmd.Parameters.AddWithValue("@ReviewComments", reviewComments);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error (optional: replace with a proper logging system)
                System.Diagnostics.Debug.WriteLine($"Database Error: {ex.Message}");
            }
        }

        private string FormatReviewWithCodeBlocks(string review)
        {
            // Replace code blocks with proper formatting
            return review.Replace("```", "<pre><code>").Replace("```", "</code></pre>");
        }
    }
}
