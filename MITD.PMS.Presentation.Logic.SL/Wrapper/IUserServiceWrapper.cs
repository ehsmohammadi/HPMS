using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IUserServiceWrapper:IServiceWrapper
    {
        void GetAllUsers(Action<PageResultDTO<UserDTOWithActions>, Exception> action, int pageSize,
                                 int pageIndex, Dictionary<string, string> sortBy, UserCriteria criteria);

        void GetAllUserActionTypes(Action<List<ActionType>, Exception> action, string userName, bool isGroup,
            string groupId);

        void GetUser(Action<UserDTO, Exception> action, string username);
        void AddUser(Action<UserDTO, Exception> action, UserDTO userDto);
        void UpdateUser(Action<UserDTO, Exception> action, UserDTO userDto);
        void DeleteUser(Action<string, Exception> action, string  username);

        void GetAllUserGroups(Action<List<UserGroupDTOWithActions>, Exception> action);
        void GetUserGroup(Action<UserGroupDTO, Exception> action, string groupName);
        void AddUserGroup(Action<UserGroupDTO, Exception> action, UserGroupDTO userGroupDto);
        void UpdateUserGroup(Action<UserGroupDTO, Exception> action, UserGroupDTO userGroupDto);
        void DeleteUserGroup(Action<string, Exception> action, string groupName);
        
        void GetToken(Action<string, Exception> action, string userName, string passWord);
        void GetToken(Action<string, Exception> action);
        void GetSessionToken(Action<string, Exception> action, string token, string newCurrentWorkListUser);
        void GetLogonUser(Action<UserStateDTO, Exception> action);
        void LogoutUser(Action<string,Exception> action);
        void GetAllUserGroupsDescriptions(Action<List<UserGroupDescriptionDTO>, Exception> action);
        void GetAllActionTypes(Action<List<ActionType>, Exception> action);

        void ChangeCurrentWorkListUserName(Action<string, Exception> action, string currentUsername,
            string newUserName);

        void GetAllUserDescriptions(Action<PageResultDTO<UserDescriptionDTO>, Exception> action, int pageSize,
                                 int pageIndex, Dictionary<string, string> sortBy, UserCriteria criteria);

        void UpdateUserAccess(Action<UserGroupDTO, Exception> action, string username, Dictionary<int, bool> actionList);
        bool IsUserPermissionGranted(Type controllerType, string methodName, List<ActionType> authorizedActions);
        void ChangePassWord(Action<string, Exception> action, string newPass, string oldPass);
    }
}
