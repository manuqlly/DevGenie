// AuthUtility.cs - Add to your project
using System;
using System.Web;

namespace DevGenie
{
    public static class AuthUtility
    {
        public static bool IsAuthenticated()
        {
            return HttpContext.Current.Session["IsAuthenticated"] != null &&
                  (bool)HttpContext.Current.Session["IsAuthenticated"];
        }

        public static void EnsureAuthenticated()
        {
            if (!IsAuthenticated())
            {
                HttpContext.Current.Response.Redirect("Login.aspx?returnUrl=" +
                    HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.Url.PathAndQuery));
            }
        }

        public static string GetCurrentUserId()
        {
            return HttpContext.Current.Session["UserId"]?.ToString();
        }
    }
}