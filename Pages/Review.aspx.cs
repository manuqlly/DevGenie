using System;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;
using System.Threading.Tasks; // Required for async methods
using System.Text.RegularExpressions;

namespace DevGenie
{
    public partial class Review : Page
    {
        protected System.Web.UI.WebControls.Label reviewOutput;
        protected System.Web.UI.WebControls.TextBox code;
        protected System.Web.UI.WebControls.Label explanation;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is authenticated
            AuthUtility.EnsureAuthenticated();
        }

        protected async void ReviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                using var openAIService = new OpenAIService();
                string reviewResult = await openAIService.ReviewCodeAsync(code.Text.Trim(), "any");

                if (!string.IsNullOrEmpty(reviewResult))
                {
                    explanation.Text = reviewResult; // Fix: Show review in explanation box
                    await SaveToHistoryAsync(code.Text, reviewResult);
                }
                else
                {
                    explanation.Text = "No review output generated.";
                }
            }
            catch (Exception ex)
            {
                explanation.Text = $"Error: {ex.Message}";
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
    }
}
