using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;


namespace RentingGown
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application.Add("CounterVisiters", 0);
            Application.Add("CounterCurrentVisiters", 0);
            //HttpConfiguration config = GlobalConfiguration.Configuration;

            //config.Formatters.JsonFormatter
            //            .SerializerSettings
            //            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
        protected void Session_Start()
        {
            Application["CounterVisiters"] = Convert.ToInt32(Application["CounterVisiters"]) + 1;
            Application["CounterCurrentVisiters"] = Convert.ToInt32(Application["CounterCurrentVisiters"]) + 1;


        }
        protected void Session_End()
        {
            Application["CounterCurrentVisiters"] = Convert.ToInt32(Application["CounterCurrentVisiters"]) - 1;
        }
    }
}
