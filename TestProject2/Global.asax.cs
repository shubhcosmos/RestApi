using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace TestProject2
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Default;
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }


        protected void Application_Error(object sender , EventArgs e)

        {


            Exception ex = Server.GetLastError();

            Context.Trace.Write("Application Level Error : " + ex.ToString());
        }
    }


}
