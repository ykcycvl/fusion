using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Fusion
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static V83.COMConnector connector;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            connector = new V83.COMConnector();
            connector.PoolCapacity = 5;
            connector.PoolTimeout = 30;
            connector.MaxConnections = 10;
            Application.Add("connector", connector);
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            System.Globalization.CultureInfo nf_info = new System.Globalization.CultureInfo("RU-ru", false);
            nf_info.NumberFormat.NumberDecimalSeparator = ".";
            nf_info.NumberFormat.NumberGroupSeparator = " ";
            nf_info.NumberFormat.CurrencyDecimalSeparator = ".";
            nf_info.NumberFormat.CurrencyGroupSeparator = " ";
            System.Threading.Thread.CurrentThread.CurrentCulture = nf_info;
        }
    }
}
