using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevGenie
{
    public partial class Dashboard : Page
    {
        private string connectionString = "Data Source=DESKTOP-P70QF2S;Initial Catalog=devgenie;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is authenticated
            AuthUtility.EnsureAuthenticated();

            if (!IsPostBack)
            {
                // Display username
                lblUsername.Text = Session["Username"].ToString();

                // Load user statistics
                LoadUserStatistics();

                // Load code generation history
                LoadCodeHistory();
            }
        }

        protected void gvCodeHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Handle the row command event
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCodeHistory.Rows[index];

            if (e.CommandName == "SomeCommand") // Replace with actual command name
            {
                // Perform necessary actions
            }
        }

        private void LoadUserStatistics()
        {
            string userId = AuthUtility.GetCurrentUserId();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Get code generation count
                string codeGenQuery = "SELECT COUNT(*) FROM CodeHistory WHERE UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(codeGenQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    lblCodeGenCount.Text = cmd.ExecuteScalar().ToString();
                }

                // Get review count if table exists
                try
                {
                    string reviewQuery = "SELECT COUNT(*) FROM ReviewHistory WHERE UserId = @UserId";
                    using (SqlCommand cmd = new SqlCommand(reviewQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        lblReviewCount.Text = cmd.ExecuteScalar().ToString();
                    }
                }
                catch
                {
                    lblReviewCount.Text = "0";
                }

                // Get explanation count if table exists
                try
                {
                    string explainQuery = "SELECT COUNT(*) FROM ExplanationHistory WHERE UserId = @UserId";
                    using (SqlCommand cmd = new SqlCommand(explainQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        lblExplanationCount.Text = cmd.ExecuteScalar().ToString();
                    }
                }
                catch
                {
                    lblExplanationCount.Text = "0";
                }
            }
        }

        private void LoadCodeHistory()
        {
            string userId = AuthUtility.GetCurrentUserId();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT TOP 10 HistoryId, Description, GeneratedCode, Language, CreatedAt " +
                              "FROM CodeHistory WHERE UserId = @UserId ORDER BY CreatedAt DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    gvCodeHistory.DataSource = dt;
                    gvCodeHistory.DataBind();
                }
            }
        }

    }
}