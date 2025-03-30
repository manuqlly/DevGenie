using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace DevGenie
{
    public partial class ResetPassword : Page
    {
        protected void btnReset_Click(object sender, EventArgs e)
        {
            string connString = "Data Source=DESKTOP-P70QF2S;Initial Catalog=devgenie;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT Id FROM Users WHERE Email=@Email";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                    object userId = cmd.ExecuteScalar();
                    if (userId != null)
                    {
                        string token = Guid.NewGuid().ToString();
                        string insertTokenQuery = "INSERT INTO PasswordResetTokens (UserId, Token, Expiry) VALUES (@UserId, @Token, DATEADD(HOUR, 1, GETDATE()))";

                        using (SqlCommand insertCmd = new SqlCommand(insertTokenQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@UserId", userId);
                            insertCmd.Parameters.AddWithValue("@Token", token);
                            insertCmd.ExecuteNonQuery();

                            lblMessage.Text = "A password reset link has been sent to your email.";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Email not found.";
                    }
                }
            }
        }
    }
}
