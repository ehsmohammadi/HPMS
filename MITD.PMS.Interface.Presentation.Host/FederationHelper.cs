using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Services;

namespace MITD.PMS.Interface.Presentation.Host
{
    public static class FederationHelper
    {
        public static string GetSignInRequest(string returnUrl)
        {
            return FederatedAuthentication.WSFederationAuthenticationModule.CreateSignInRequest("Passive", returnUrl, false).RequestUrl;
        }
    }
}