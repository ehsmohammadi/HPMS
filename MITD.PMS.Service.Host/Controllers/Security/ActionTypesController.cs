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

        public ActionTypesController(IUserServiceFacade userServiceFacade)
        {
            this.userServiceFacade = userServiceFacade;
        }

        public List<ActionTypeDTO> GetAllActionTypes()
        {
            return userServiceFacade.GetAllActionTypes();
        }
        
    }

}