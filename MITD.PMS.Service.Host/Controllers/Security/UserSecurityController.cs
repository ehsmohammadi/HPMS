using System.Web.Http;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Service.Host.Controllers.Security
{
    public class UserSecurityController:ApiController
    {
        #region Fields
        private readonly IUserServiceFacade userService;
        
        #endregion

        #region Constructors
        public UserSecurityController(IUserServiceFacade userService)
        {
            this.userService = userService;
        }

        #endregion

        #region Methods

        public string PutUser([FromBody]ChangePasswordDTO changePassword)
        {
            return userService.ChangePassword(changePassword);
        }
 
        #endregion
    }
}