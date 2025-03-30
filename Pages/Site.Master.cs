using System;

namespace DevGenie
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected System.Web.UI.WebControls.HyperLink lnkLogin;
        protected System.Web.UI.WebControls.LinkButton lnkLogout;
        protected System.Web.UI.WebControls.HyperLink lnkDashboard;
        protected System.Web.UI.WebControls.HyperLink lnkGenerator;
        protected System.Web.UI.WebControls.HyperLink lnkOutput;
        protected System.Web.UI.WebControls.HyperLink lnkReview;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Show/hide nav links based on authentication
            bool isAuthenticated = AuthUtility.IsAuthenticated();

            lnkLogin.Visible = !isAuthenticated;
            lnkLogout.Visible = isAuthenticated;
            lnkDashboard.Visible = isAuthenticated;

            // Set visibility for tool links
            lnkGenerator.Visible = isAuthenticated;
            lnkOutput.Visible = isAuthenticated;
            lnkReview.Visible = isAuthenticated;
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            // Clear all session variables
            Session.Clear();
            Session.Abandon();

            // Redirect to home page
            Response.Redirect("Home.aspx");
        }
    }
}