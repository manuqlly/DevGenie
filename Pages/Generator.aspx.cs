using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.UI;

namespace DevGenie
{
    public partial class Generator : Page
    {
        protected System.Web.UI.WebControls.TextBox description;
        protected System.Web.UI.WebControls.DropDownList language;
        protected System.Web.UI.WebControls.TextBox generatedCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is authenticated
            AuthUtility.EnsureAuthenticated();

            if (!IsPostBack)
            {
                // Check for query parameters from dashboard regenerate
                if (!string.IsNullOrEmpty(Request.QueryString["desc"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["lang"]))
                {
                    description.Text = Request.QueryString["desc"];
                    language.SelectedValue = Request.QueryString["lang"];
                }
            }
        }

        protected async void GenerateButton_Click(object sender, EventArgs e)
        {
            try
            {
                using var aiService = new OpenAIService();
                string lang = language.SelectedValue;
                string prompt = description.Text.Trim();

                if (string.IsNullOrWhiteSpace(prompt))
                {
                    generatedCode.Text = "Please enter a valid description.";
                    return;
                }

                // Generate code using OpenAI (Async)
                string generatedCodeText = await aiService.GenerateCodeAsync(prompt, lang);

                // Display the generated code
                generatedCode.Text = generatedCodeText;

                // Save the generated code to history asynchronously
                await SaveToHistoryAsync(prompt, generatedCodeText, lang);
            }
            catch (Exception ex)
            {
                generatedCode.Text = $"Error: {ex.Message}";
            }
        }

        private async Task SaveToHistoryAsync(string description, string generatedCode, string language)
        {
            string userId = AuthUtility.GetCurrentUserId();
            string connString = "Data Source=DESKTOP-P70QF2S;Initial Catalog=devgenie;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                string query = "INSERT INTO CodeHistory (UserId, Description, GeneratedCode, Language, CreatedAt) " +
                              "VALUES (@UserId, @Description, @GeneratedCode, @Language, @CreatedAt)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@GeneratedCode", generatedCode);
                    cmd.Parameters.AddWithValue("@Language", language);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
