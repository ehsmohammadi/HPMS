using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Castle.Core.Internal;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public partial class UserServiceWrapper : IUserServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string baseAddressUsers = PMSClientConfig.BaseApiAddress + "Users";
        private readonly string baseAddressUserGroups = PMSClientConfig.BaseApiAddress + "UserGroups";
        private readonly string baseAddressActionTypes = PMSClientConfig.BaseApiAddress + "ActionTypes";

        public UserServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        public void GetAllUsers(Action<PageResultDTO<UserDTOWithActions>, Exception> action, int pageSize, int pageIndex, Dictionary<string, string> sortBy, UserCriteria criteria)
        {
            var url = baseAddressUsers + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex + getFilterUser(criteria);
            if (sortBy.Count > 0)
                url += "&SortBy=" + QueryConditionHelper.GetSortByQueryString(sortBy);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        private string getFilterUser(UserCriteria userCriteria)
        {
            if (userCriteria == null)
                return string.Empty;

            var qs = string.Empty;

            if (!string.IsNullOrEmpty(userCriteria.Fname))
                qs += "FirstName:" + userCriteria.Fname + ";";

            if (!string.IsNullOrEmpty(userCriteria.Lname))
                qs += "LastName:" + userCriteria.Lname + ";";

            if (!string.IsNullOrEmpty(userCriteria.PartyName))
                qs += "PartyName:" + userCriteria.PartyName + ";";

            if (string.IsNullOrEmpty(qs))
                return string.Empty;

            return "&Filter=" + qs;

        }

        public void GetUser(Action<UserDTO, Exception> action, string username)
        {
            var url = string.Format(baseAddressUsers + "?partyName=" + username);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddUser(Action<UserDTO, Exception> action, UserDTO userDto)
        {
            var url = string.Format(baseAddressUsers);
            WebClientHelper.Post(new Uri(url, UriKind.Absolute), action, userDto, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateUser(Action<UserDTO, Exception> action, UserDTO userDto)
        {
            var url = string.Format(baseAddressUsers + "?partyname=" + userDto.PartyName);
            WebClientHelper.Put(new Uri(url, UriKind.Absolute), action, userDto, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteUser(Action<string, Exception> action, string username)
        {
            var url = string.Format(baseAddressUsers + "?partyName=" + username);
            WebClientHelper.Delete(new Uri(url, UriKind.Absolute), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllUserGroups(Action<List<UserGroupDTOWithActions>, Exception> action)
        {
            WebClientHelper.Get(new Uri(baseAddressUserGroups, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetUserGroup(Action<UserGroupDTO, Exception> action, string groupName)
        {
            var url = string.Format(baseAddressUserGroups + "?partyName=" + groupName);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddUserGroup(Action<UserGroupDTO, Exception> action, UserGroupDTO userGroupDto)
        {
            var url = string.Format(baseAddressUserGroups);
            WebClientHelper.Post(new Uri(url, UriKind.Absolute), action, userGroupDto, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateUserGroup(Action<UserGroupDTO, Exception> action, UserGroupDTO userGroupDto)
        {
            var url = string.Format(baseAddressUserGroups + "?partyname=" + userGroupDto.PartyName);
            WebClientHelper.Put(new Uri(url, UriKind.Absolute), action, userGroupDto, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteUserGroup(Action<string, Exception> action, string groupName)
        {
            var url = string.Format(baseAddressUserGroups + "?partyName=" + groupName);
            WebClientHelper.Delete(new Uri(url, UriKind.Absolute), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetToken(Action<string, Exception> action, string userName, string password)
        {
            var url = string.Format(baseAddressUsers + "?userName=" + userName + "&password=" + password);
            WebClientHelper.GetString(new Uri(url, PMSClientConfig.UriKind), action);
        }

        public void GetSessionToken(Action<string, Exception> action, string token, string newCurrentWorkListUser)
        {
            var url = string.Format(PMSClientConfig.BaseApiAddress + "/token");
            if (!string.IsNullOrWhiteSpace(newCurrentWorkListUser))
                url = url + "?CurrentWorkListUserName=" + newCurrentWorkListUser;
            WebClientHelper.GetString(new Uri(url, PMSClientConfig.UriKind), action,
                new Dictionary<string, string> { { "SilverLight", "1" }, { "Authorization", "SAML " + token } });
        }

        public void GetToken(Action<string, Exception> action)
        {
            var x = "/Security";
            var url = string.Format(x);
            var client = new WebClient();

            client.DownloadStringCompleted += (s, a) =>
            {
                action(a.Result, a.Error);
            };
            client.DownloadStringAsync(new Uri(url, UriKind.Relative));
        }

        public void GetLogonUser(Action<UserStateDTO, Exception> action)
        {
            var url = string.Format(baseAddressUsers);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void LogoutUser(Action<string, Exception> action)
        {
            var url = string.Format(baseAddressUsers);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllUserGroupsDescriptions(Action<List<UserGroupDescriptionDTO>, Exception> action)
        {
            WebClientHelper.Get(new Uri(baseAddressUserGroups, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllActionTypes(Action<List<ActionType>, Exception> action)
        {
            WebClientHelper.Get(new Uri(baseAddressActionTypes, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllUserActionTypes(Action<List<ActionType>, Exception> action, string userName, bool isGroup, string groupId)
        {
            if (groupId.IsNullOrEmpty())
                groupId = "0";

            var url = baseAddressActionTypes + "?userName=" + userName + "&isGroup=" + isGroup + "&groupId=" + groupId;
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void ChangeCurrentWorkListUserName(Action<string, Exception> action, string logonUserName, string currentPermittedUser)
        {
            var url = string.Format(baseAddressUsers + "/" + logonUserName + "/CurrentPermittedUser");
            WebClientHelper.Put(new Uri(url, UriKind.Absolute), action, currentPermittedUser, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllUserDescriptions(Action<PageResultDTO<UserDescriptionDTO>, Exception> action, int pageSize, int pageIndex, Dictionary<string, string> sortBy, UserCriteria criteria)
        {
            var url = baseAddressUsers + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex + getFilterUser(criteria);
            if (sortBy.Count > 0)
                url += "&SortBy=" + QueryConditionHelper.GetSortByQueryString(sortBy);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void UpdateUserAccess(Action<UserGroupDTO, Exception> action, string username, Dictionary<int, bool> actionList)
        {
            var url = string.Format(baseAddressActionTypes + "?username=" + username);
            WebClientHelper.Put(new Uri(url, UriKind.Absolute), action, actionList, WebClientHelper.MessageFormat.Json
               , PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public bool IsUserPermissionGranted(Type controllerType, string methodName, List<ActionType> authorizedActions)
        {
            bool result = false;
            RequiredPermissionAttribute[] permissionAttributes = (RequiredPermissionAttribute[])
                controllerType.GetMethod(methodName).GetCustomAttributes(typeof(RequiredPermissionAttribute), true);

            if (permissionAttributes.Length > 0)
            {
                if (authorizedActions == null)
                    return false;

                foreach (RequiredPermissionAttribute attr in permissionAttributes)
                {
                    if (!authorizedActions.Contains(attr.ActionType))
                    {
                        // Break the loop without setting the result to true
                        break;
                    }
                    result = true;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        public void ChangePassWord(Action<string, Exception> action, string newPass, string oldPass)
        {
            throw new NotImplementedException();
        }
    }
}
