using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace DevGenie
{
    public partial class Register : Page
    {
        protected global::System.Web.UI.WebControls.TextBox txtUsername;
        protected global::System.Web.UI.WebControls.TextBox txtEmail;
        protected global::System.Web.UI.WebControls.TextBox txtPassword;
        protected global::System.Web.UI.WebControls.Label lblMessage;
        protected global::System.Web.UI.WebControls.Button btnRegister;
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string connString = "Data Source=DESKTOP-P70QF2S;Initial Catalog=devgenie;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Username, Email, PasswordHash) VALUES (@Username, @Email, @PasswordHash)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@PasswordHash", txtPassword.Text); // Hash the password in a real-world scenario

                    try
                    {
                        cmd.ExecuteNonQuery();
                        lblMessage.Text = "Registration Successful!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    catch (SqlException)
                    {
                        lblMessage.Text = "Username or Email already exists.";
                    }
                }
            }
        }
    }
}
