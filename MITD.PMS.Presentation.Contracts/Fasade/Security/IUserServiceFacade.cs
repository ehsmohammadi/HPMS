using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    public interface IUserServiceFacade : IFacadeService
    {
        
        UserStateDTO GetUserState(ClaimsPrincipal current);
        string LogoutUser(ClaimsPrincipal current);
        PageResultDTO<UserDTOWithActions> GetAllUsers(int pageSize, int pageIndex, string filter);
        UserDTO GetUserByUsername(string username);
        UserDTO AddUser(UserDTO user);
        UserDTO UpdateUser(UserDTO user);

        string DeleteUser(string username);
        List<UserGroupDTOWithActions> GetAllUserGroups();
        UserGroupDTO GetUserGroupByName(string groupName);
        UserGroupDTO AddUserGroup(UserGroupDTO userGroup);
        UserGroupDTO UpdateUserGroup(UserGroupDTO userGroup);
        string DeleteUserGroup(string groupName);
        List<ActionTypeDTO> GetAllActionTypes();
        UserStateDTO ChangeCurrentUserWorkList(string logonUser, string currentPermittedUser, ClaimsPrincipal claimsPrincipal);
    }
}
