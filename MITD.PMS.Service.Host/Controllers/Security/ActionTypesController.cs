using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Web.Http;
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
    public class ActionTypesController : ApiController
    {
        private readonly IUserServiceFacade userServiceFacade;
        private ISecurityServiceFacade _securityFacadeService;

        public ActionTypesController(IUserServiceFacade userServiceFacade, ISecurityServiceFacade securityFacadeService)
        {
            this.userServiceFacade = userServiceFacade;
            this._securityFacadeService = securityFacadeService;
        }

        public List<ActionType> GetAllActionTypes()
        {
            return userServiceFacade.GetAllActionTypes();
        }
        public List<ActionType> GetAllUserActionTypes(string userName, bool isGroup, string groupId)
        {

            if (isGroup)
            {
                return userServiceFacade.GetGroupActionType(groupId);
            }
            else
            {
                return _securityFacadeService.GetUserAuthorizedActions(userName);
            }


        }

        public void Put([FromBody] Dictionary<int, bool> entity, string username)
        {
            this.userServiceFacade.UpdateUserAccess(username, entity);
        }
        
    }

}