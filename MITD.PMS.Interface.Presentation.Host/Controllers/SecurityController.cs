using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace MITD.PMS.Interface.Presentation.Host.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class SecurityController : Controller
    {
        //
        // GET: /Security/

        public ActionResult Index()
        {
            var result = "";
            var context = (User.Identity as ClaimsIdentity).BootstrapContext as BootstrapContext;
            if ( context != null)
            {
                if (context.Token != null)
                    result = context.Token;
                else
                {
                    StringBuilder sb = new StringBuilder();
                    using (var writer = XmlWriter.Create(sb))
                    {
                        context.SecurityTokenHandler.WriteToken(writer, context.SecurityToken);
                    }
                    result = sb.ToString();    
                }
                
            }
            else
            {
                var message = FederatedAuthentication.WSFederationAuthenticationModule.CreateSignInRequest("passive", Request.RawUrl, false);
                return new RedirectResult(message.RequestUrl);
            }
            return this.Content(result, "application/xml", Encoding.UTF8);
        }
        
        public ActionResult LogOut()
        {
            if (!this.User.Identity.IsAuthenticated) return new EmptyResult();
            var authModule = FederatedAuthentication.WSFederationAuthenticationModule;
            authModule.SignOut();
            // string signoutUrl = (WSFederationAuthenticationModule.GetFederationPassiveSignOutUrl(authModule.Issuer, authModule.Realm, null));
            var baseUrl = VirtualPathUtility.AppendTrailingSlash( Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath);
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
            WSFederationAuthenticationModule.FederatedSignOut(new Uri(authModule.Issuer),new Uri(baseUrl+"HPMS.aspx") );
            return new EmptyResult();

        }


    }
}
