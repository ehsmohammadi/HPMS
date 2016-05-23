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
    public class UsersController : ApiController
    {
        private readonly ISecurityServiceFacade securityServiceFacade;
        private readonly IUserServiceFacade userServiceFacade;

        public UsersController(
            ISecurityServiceFacade securityServiceFacade,
            IUserServiceFacade userServiceFacade
            )
        {
            this.securityServiceFacade = securityServiceFacade;
            this.userServiceFacade = userServiceFacade;
        }

        [AllowAnonymous]
        public HttpResponseMessage GetToken(string username, string password)
        {
            string relyingPartyId = System.Configuration.ConfigurationManager.AppSettings["AudianceUri"];
            string identityServerEndpoint = System.Configuration.ConfigurationManager.AppSettings["SecurityEndPoint"];

            var binding = new UserNameWSTrustBinding(SecurityMode.TransportWithMessageCredential);

            var credentials = new ClientCredentials();
            credentials.UserName.UserName = username;
            credentials.UserName.Password = password;
            try
            {
                var token = WSTrustClient.Issue(
                    new EndpointAddress(identityServerEndpoint),
                    new EndpointAddress(relyingPartyId),
                    binding,
                    credentials) as GenericXmlSecurityToken;

                return new HttpResponseMessage()
                    {
                        Content = new StringContent(token.TokenXml.OuterXml, Encoding.UTF8, "application/xml")
                    };
            }
            catch (Exception)
            {
                throw new PMSSecurityIdentityException();
            }
        }

        public UserStateDTO GetUserState()
        {
            return userServiceFacade.GetUserState(ClaimsPrincipal.Current);
        }

        public string DeleteUserState()
        {
            return userServiceFacade.LogoutUser(ClaimsPrincipal.Current);
            //return "";
        }

        public PageResultDTO<UserDTOWithActions> GetAllUsers(int pageSize, int pageIndex, string filter = "")
        {
            return userServiceFacade.GetAllUsers(pageSize, pageIndex, filter);
        }
        public UserDTO GetUser(string partyName)
        {
            return userServiceFacade.GetUserByUsername(partyName);
        }
        public UserDTO PostUser(UserDTO user)
        {
            return userServiceFacade.AddUser(user);
        }
        //public UserDTO PutUser(UserDTO user)
        //{
        //    return userServiceFacade.UpdateUser(user);
        //}


        public string DeleteUser(string partyName)
        {
            return userServiceFacade.DeleteUser(partyName);
        }
    }

}