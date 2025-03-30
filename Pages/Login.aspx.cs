using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace DevGenie
{
    public partial class Login : Page
    {
        protected System.Web.UI.WebControls.TextBox txtUsername;
        protected System.Web.UI.WebControls.TextBox txtPassword;
        protected System.Web.UI.WebControls.Label lblMessage;

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string connString = "Data Source=DESKTOP-P70QF2S;Initial Catalog=devgenie;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT Id, Username FROM Users WHERE Username=@Username AND PasswordHash=@PasswordHash";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@PasswordHash", txtPassword.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Create session variables
                            Session["UserId"] = reader["Id"].ToString();
                            Session["Username"] = reader["Username"].ToString();
                            Session["IsAuthenticated"] = true;

                            Response.Redirect("Home.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Invalid username or password.";
                        }
                    }
                }
            }
        }
    }
}