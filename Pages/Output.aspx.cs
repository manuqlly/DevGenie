using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace DevGenie
{
    public partial class Output : Page
    {
        protected System.Web.UI.WebControls.Label explanation;  // Changed from TextBox to Label
        protected System.Web.UI.WebControls.TextBox code;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is authenticated
            AuthUtility.EnsureAuthenticated();
        }

        protected async void ExplainButton_Click(object sender, EventArgs e)
        {
            try
            {
                using var openAIService = new OpenAIService();
                explanation.Text = await openAIService.ExplainCodeAsync(code.Text.Trim(), "any");
            }
            catch (Exception ex)
            {
                explanation.Text = $"Error: {ex.Message}";
            }

            // Save explanation to history if it's not empty
            if (!string.IsNullOrEmpty(explanation.Text) && !string.IsNullOrEmpty(code.Text))
            {
                SaveToHistory(code.Text, explanation.Text);
            }
        }

        private void SaveToHistory(string codeSubmitted, string explanationText)
        {
            string userId = AuthUtility.GetCurrentUserId();
            string connString = "Data Source=DESKTOP-P70QF2S;Initial Catalog=devgenie;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "INSERT INTO ExplanationHistory (UserId, CodeSubmitted, Explanation, CreatedAt) " +
                              "VALUES (@UserId, @CodeSubmitted, @Explanation, @CreatedAt)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@CodeSubmitted", codeSubmitted);
                    cmd.Parameters.AddWithValue("@Explanation", explanationText);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
