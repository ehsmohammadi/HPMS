using System;
using System.IdentityModel.Services;
using System.Web;
namespace MITD.PMS.Service.Host
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
               
        protected void Application_Start()
        {
            BootStrapper.Execute();
         }

        void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var ctx = HttpContext.Current;
            if (ctx.Request.IsAuthenticated)
            {
                var x = User.Identity.Name;
            }
        }

    }
}