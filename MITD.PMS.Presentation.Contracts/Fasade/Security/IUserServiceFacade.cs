using System.Collections.Generic;
using System.Security.Claims;
using MITD.Core;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IUserServiceFacade : IFacadeService
    {
        UserStateDTO GetUserState(ClaimsPrincipal current);
        string LogoutUser(ClaimsPrincipal current);
        //PageResultDTO<UserDTOWithActions> GetAllUsers(int pageSize, int pageIndex, string filter);
        UserDTO GetUserByUsername(string username);
        UserDTO AddUser(UserDTO user);
        UserDTO UpdateUser(UserDTO user);

        string DeleteUser(string username);
        List<UserGroupDTOWithActions> GetAllUserGroups();
        PageResultDTO<UserDTOWithActions> GetAllUsers(int pageSize, int pageIndex, string filter);

        UserGroupDTO GetUserGroupByName(string groupName);
        UserGroupDTO AddUserGroup(UserGroupDTO userGroup);
        UserGroupDTO UpdateUserGroup(UserGroupDTO userGroup);
        string DeleteUserGroup(string groupName);
        List<ActionType> GetAllActionTypes();
        UserStateDTO ChangeCurrentUserWorkList(string logonUser, string currentPermittedUser, ClaimsPrincipal claimsPrincipal);

        // Imported

        void UpdateUserAccess(string username, Dictionary<int, bool> actionTyps);
        List<ActionType> GetGroupActionType(string groupId);
     
        //List<ActionType> GetGroupActionType(long groupId);
        //List<ActionType> GetAllUserActionTypes(long userId);
        
        //UserDTO GetUserByUserName(string username);
        
        //UserDTO GetUserWithCompany(int id);

        //FuelUserDTO GetCurrentFuelUser();


        string ChangePassword(ChangePasswordDTO changePassword);
    }
}

    