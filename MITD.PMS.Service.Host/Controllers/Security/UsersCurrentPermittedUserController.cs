using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Services;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Xml;
using MITD.PMS.Interface;
using MITD.PMS.Presentation.Contracts.Fasade;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Exceptions;
using Thinktecture.IdentityModel.WSTrust;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.IdentityModel.Tokens;
using System.Net.Http;
using System.Text;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class UsersCurrentPermittedUserController : ApiController
    {
        private readonly ISecurityServiceFacade securityServiceFacade;
        private readonly IUserServiceFacade userServiceFacade;

        public UsersCurrentPermittedUserController(
            ISecurityServiceFacade securityServiceFacade,
            IUserServiceFacade userServiceFacade
            )
        {
            this.securityServiceFacade = securityServiceFacade;
            this.userServiceFacade = userServiceFacade;
        }

        
        //public UserStateDTO GetUserState()
        //{
        //    userServiceFacade.AddUpdatePMSUser(ClaimsPrincipal.Current);
        //    return userServiceFacade.GetUserState(ClaimsPrincipal.Current);
        //}

        public UserStateDTO PutCurrentUserWorkList(string logonUserName, string currentPermittedUser)
        {
            return userServiceFacade.ChangeCurrentUserWorkList(logonUserName, currentPermittedUser, ClaimsPrincipal.Current);
        }
        
    }

}