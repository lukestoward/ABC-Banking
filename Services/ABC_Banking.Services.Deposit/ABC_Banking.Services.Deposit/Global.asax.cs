using System.Web.Http;
using System.Web.Mvc;

namespace ABC_Banking.Services.Deposit
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();

        }
    }
}
