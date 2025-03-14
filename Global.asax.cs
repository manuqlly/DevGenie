using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using DevGenie;

public class MvcApplication : HttpApplication
{
    protected void Application_Start()
    {
        // Register Web API routes
        GlobalConfiguration.Configure(WebApiConfig.Register);

        // Register MVC routes
        RouteConfig.RegisterRoutes(RouteTable.Routes);

    }
}
