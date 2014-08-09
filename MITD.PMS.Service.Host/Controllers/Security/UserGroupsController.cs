using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Interface;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class UserGroupsController : ApiController
    {
        private readonly IUserServiceFacade userServiceFacade;

        public UserGroupsController(
            IUserServiceFacade userServiceFacade
            )
        {
            this.userServiceFacade = userServiceFacade;
        }

        public List<UserGroupDTOWithActions> GetAllUserGroups()
        {
            return userServiceFacade.GetAllUserGroups();
        }
        public UserGroupDTO GetUserGroup(string partyName)
        {
            return userServiceFacade.GetUserGroupByName(partyName);
        }
        public UserGroupDTO PostUserGroup(UserGroupDTO userGroup)
        {
            return userServiceFacade.AddUserGroup(userGroup);
        }
        public UserGroupDTO PutUserGroup(UserGroupDTO userGroup)
        {
            return userServiceFacade.UpdateUserGroup(userGroup);
        }
        public string DeleteUserGroup(string partyName)
        {
            return userServiceFacade.DeleteUserGroup(partyName);
        }
        
    }
}