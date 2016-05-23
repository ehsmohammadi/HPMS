using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Castle.Core;
using Castle.Core.Internal;
using Castle.Windsor.Installer;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Application.Contracts;
using MITD.PMSSecurity.Domain;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
    [Interceptor(typeof(Interception))]
    public class UserServiceFacade : IUserServiceFacade
    {
        private readonly ISecurityService securityService;
        private readonly IUserRepository userRep;
        private IMapper<ClaimsPrincipal, UserStateDTO> userStateMapper;
        private IMapper<List<User>, ClaimsPrincipal> pmsUsersMapper;
        private IMapper<User, UserDTOWithActions> userDTOWithActionsMapper;
        private IMapper<Group, UserGroupDTOWithActions> userGroupDTOWithActionsMapper;
        private IMapper<User, UserDTO> userDTOMapper;
        private IMapper<Group, UserGroupDTO> userGroupDTOMapper;
        private IMapper<Group, UserGroupDescriptionDTO> userGroupDescriptionDTOMapper;
        private IMapper<User, UserDescriptionDTO> userDescriptionDTOMapper;
        private ISecurityService _securityApplicationService;
        private readonly IUserManagementService userManagementService;
        private readonly ISecurityServiceFacade securityServiceFacade;

        public UserServiceFacade(ISecurityService securityService,
            IMapper<ClaimsPrincipal, UserStateDTO> userStateMapper,
            IMapper<List<User>, ClaimsPrincipal> pmsUsersMapper,
            IUserRepository userRep,
            IMapper<User, UserDTOWithActions> userDTOWithActionsMapper,
            IMapper<User, UserDTO> userDTOMapper,
            IMapper<Group, UserGroupDTOWithActions> userGroupDTOWithActionsMapper,
            IMapper<Group, UserGroupDTO> userGroupDTOMapper,
            //IMapper<ActionType, ActionTypeDTO> actionTypeDTOMapper ,
            IMapper<Group, UserGroupDescriptionDTO> userGroupDescriptionDTOMapper,
            IMapper<User, UserDescriptionDTO> userDescriptionDTOMapper,

            ISecurityServiceFacade securityServiceFacade,
            ISecurityService _securityApplicationService,IUserManagementService userManagementService
            )
        {
            this.securityService = securityService;
            this.userStateMapper = userStateMapper;
            this.pmsUsersMapper = pmsUsersMapper;
            this.userRep = userRep;
            this.userDTOWithActionsMapper = userDTOWithActionsMapper;
            this.userDTOMapper = userDTOMapper;
            this.userGroupDTOWithActionsMapper = userGroupDTOWithActionsMapper;
            this.userGroupDTOMapper = userGroupDTOMapper;

            this.userGroupDescriptionDTOMapper = userGroupDescriptionDTOMapper;
            this.userDescriptionDTOMapper = userDescriptionDTOMapper;

            this.securityServiceFacade = securityServiceFacade;
            this._securityApplicationService = _securityApplicationService;
            this.userManagementService = userManagementService;
        }

        //[RequiredPermission(ActionType.ShowUser)]
        public UserStateDTO GetUserState(ClaimsPrincipal u)
        {
            if (u == null)
                throw new NullReferenceException("Principal is null");
            var pmsUsers = pmsUsersMapper.MapToEntity(u);
            if (pmsUsers == null || pmsUsers.Count == 0)
                throw new Exception("pmsUsers is null or pmsUsers.count is zero");

            var userState = userStateMapper.MapToModel(u);

            var user = securityService.GetUser(new PartyId(u.Identity.Name));
            userState.PermittedUsersOnMyWorkList = new List<UserDescriptionDTO>();
            if (user != null)
            {
                userState.PermittedUsersOnMyWorkList.Add(new UserDescriptionDTO { FirstName = user.FirstName, LastName = user.LastName, PartyName = user.Id.PartyName });
                var permittedWorkList = securityService.GetPermittedWorkListFor(user);
                userState.PermittedUsersOnMyWorkList.AddRange(
                    permittedWorkList.Select(w => new UserDescriptionDTO { FirstName = w.FirstName, LastName = w.LastName, PartyName = w.Id.PartyName }).ToList());
            }
            return userState;
        }

        public UserStateDTO ChangeCurrentUserWorkList(string logonUser, string currentPermittedUser, ClaimsPrincipal claimsPrincipal)
        {
            return GetUserState(claimsPrincipal);
        }

        public string LogoutUser(ClaimsPrincipal current)
        {
            //Log to logout
            return "با موفقیت از سایت خارج شدید";
        }


        //public PageResultDTO<UserDTOWithActions> GetAllUsers(int pageSize, int pageIndex, string filter)
        //{
        //    var fs = new ListFetchStrategy<User>(Enums.FetchInUnitOfWorkOption.NoTracking);
        //    fs.WithPaging(pageSize, pageIndex);
        //    fs.OrderBy(e => e.LastName);
        //    var criterias = filter.Split(';');
        //    var predicate = getUserPredicate(criterias);
        //    userRep.FindUsers(predicate, fs);
        //    var res = new PageResultDTO<UserDTOWithActions>();
        //    res.InjectFrom(fs.PageCriteria.PageResult);
        //    res.Result =
        //        fs.PageCriteria.PageResult.Result.Select(p => userDTOWithActionsMapper.MapToModel(p)).ToList();
        //    return res;

        //}

        //private Expression<Func<User, bool>> getUserPredicate(IEnumerable<string> criterias)
        //{
        //    Expression<Func<User, bool>> res = user => true;

        //    foreach (var criteria in criterias)
        //    {
        //        var sp = criteria.Split(':');
        //        if (sp[0] == "FirstName" && !string.IsNullOrEmpty(sp[1]))
        //            res = res.And(e => e.FirstName.Contains(sp[1]));
        //        if (sp[0] == "LastName" && !string.IsNullOrEmpty(sp[1]))
        //            res = res.And(e => e.LastName.Contains(sp[1]));
        //        if (sp[0] == "PartyName" && !string.IsNullOrEmpty(sp[1]))
        //            res = res.And(e => e.Id.PartyName.Contains(sp[1]));
        //    }
        //    return res;
        //}

        #region Fake Identity

        public class FakeIdentity : IIdentity
        {
            // for without security
            public string Name { get; private set; }
            public string AuthenticationType { get; private set; }
            public bool IsAuthenticated { get; private set; }

            public FakeIdentity(string name, string authenticationType, bool isAuthenticated)
            {
                Name = name;
                AuthenticationType = authenticationType;
                IsAuthenticated = isAuthenticated;
            }
        }

        public static ClaimsPrincipal CreateFakePrincipal()
        {
            var identity = new FakeIdentity("ehsan", "Basic", true);

            var incomingPrincipal = new ClaimsPrincipal(identity);
            incomingPrincipal.Identities.First().AddClaims(new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Employee"),
                new Claim("http://identityserver.thinktecture.com/claims/profileclaims/employeeno", "30000"),
                new Claim("http://identityserver.thinktecture.com/claims/profileclaims/firstname", "احسان"),
                new Claim("http://identityserver.thinktecture.com/claims/profileclaims/lastname", "محمدی"),
                new Claim("http://identityserver.thinktecture.com/claims/profileclaims/jobpositionnames", "مدیر فنی"),
            });
            return incomingPrincipal;
        }

        public static ClaimsPrincipal configureAuthorizationClaim(ClaimsPrincipal incomingPrincipal)
        {
            string currentUsername;
            var roles = new List<string>();
            var currentUserActions = new List<string>();


            currentUsername = incomingPrincipal.Identity.Name;
            var claimsRoles = incomingPrincipal.Claims.Where(c => c.Type == ClaimTypes.Role);
            if (claimsRoles != null && claimsRoles.Any())
                roles = claimsRoles.Select(c => c.Value).ToList();


            incomingPrincipal.Identities.First().AddClaim(new Claim("CurrentUsername", currentUsername));
            var strRolesBuilder = mergStringList(roles);
            incomingPrincipal.Identities.First().AddClaim(new Claim("CurrentUserRoles", strRolesBuilder.ToString()));



            currentUserActions = getCurrentUserActions(incomingPrincipal);
            var strActionsbuilder = mergStringList(currentUserActions);
            incomingPrincipal.Identities.First().AddClaim(new Claim("CurrentUserActions", strActionsbuilder.ToString()));



            return incomingPrincipal;
        }

        private static StringBuilder mergStringList(IEnumerable<string> listString)
        {
            var builder = new StringBuilder();
            foreach (var str in listString)
            {
                builder.Append(str + ",");
            }

            if (builder.Length > 0)
                builder.Remove(builder.Length - 1, 1);
            return builder;
        }

        private static List<string> getCurrentUserActions(ClaimsPrincipal incomingPrincipal)
        {
            var currentUserActions = new List<string>();
            var securityService = SecurityServiceFacadeFactory.Create();
            try
            {
                var actions = securityService.GetUserAuthorizedActions(incomingPrincipal);
                if (actions != null && actions.Any())
                {
                    currentUserActions = actions.Select(c => ((int)c).ToString()).ToList();
                }
            }
            finally
            {
                SecurityServiceFacadeFactory.Release(securityService);
            }
            return currentUserActions;
        }


        #endregion

        [RequiredPermission(ActionType.ShowUser)]
        public UserDTO GetUserByUsername(string username)
        {
            var user = userRep.GetUserById(new PartyId(username));
            var userDTO = userDTOMapper.MapToModel(user);
            userDTO.Groups = user.GroupList.Select(g => userGroupDescriptionDTOMapper.MapToModel(g)).ToList();
            userDTO.WorkListPermittedUsers = user.WorkListUserList.Select(g => userDescriptionDTOMapper.MapToModel(g)).ToList();

            // Added by Sharif
            //ClaimsPrincipal fakePrincipal = CreateFakePrincipal();
            //configureAuthorizationClaim(fakePrincipal);
            //this.securityServiceFacade.GetUserAuthorizedActions(fakePrincipal)
            //    .ForEach(actiontype => userDTO.CustomActions.Add((int)actiontype, true));

            return userDTO;
        }

        public UserDTO AddUser(UserDTO user)
        {
            var groupIdList = user.Groups.Select(p => new PartyId(p.PartyName)).ToList();
            var workListUserIdList = user.WorkListPermittedUsers.Select(p => new PartyId(p.PartyName)).ToList();
            var u = securityService.AddUser(new PartyId(user.PartyName), user.FirstName, user.LastName, user.Email, user.IsActive, user.CustomActions, groupIdList, workListUserIdList);
            return userDTOMapper.MapToModel(u);
        }

        public UserDTO UpdateUser(UserDTO user)
        {
            var groupIdList = user.Groups.Select(p => new PartyId(p.PartyName)).ToList();
            var workListUserIdList = user.WorkListPermittedUsers.Select(p => new PartyId(p.PartyName)).ToList();
            var u = securityService.UpdateUser(new PartyId(user.PartyName), user.FirstName, user.LastName, user.Email, user.IsActive, user.CustomActions, groupIdList, workListUserIdList);
            return userDTOMapper.MapToModel(u);
        }

        public string DeleteUser(string username)
        {
            securityService.Delete(new PartyId(username));
            return "user deleted successfully ";
        }

        [RequiredPermission(ActionType.ShowUser)]
        public PageResultDTO<UserDTOWithActions> GetAllUsers(int pageSize, int pageIndex, string filter)
        {
            var fs = new ListFetchStrategy<User>(Enums.FetchInUnitOfWorkOption.NoTracking);
            fs.WithPaging(pageSize, pageIndex);

            var criterias = filter.Split(';');
            var predicate = getUserPredicate(criterias);
            var rs = getPredicate(criterias);
            if (criterias.Count() > 1)
            {
                //TODO:
                userRep.FindUsers(predicate, fs);//, rs[0], rs[1], rs[2], pageSize, pageIndex);
            }
            else
            {
                //TODO:
                userRep.FindUsers(predicate, fs);//, "", "", "", pageSize, pageIndex);
            }
            var res = new PageResultDTO<UserDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            List<UserDTOWithActions> result = new List<UserDTOWithActions>();
            fs.PageCriteria.PageResult.Result.ForEach(p =>
            {
                var u = userDTOWithActionsMapper.MapToModel(p);
                //u.CompanyDto = new CompanyDto() { Id = p.CompanyId };
                result.Add(u);

            });
            res.Result = result;
            return res;

        }

        [RequiredPermission(ActionType.ShowUserGroup)]
        public List<UserGroupDTOWithActions> GetAllUserGroups()
        {
            var userGroups = userRep.GetAllUserGroup();
            return userGroups.Select(p => userGroupDTOWithActionsMapper.MapToModel(p)).ToList();
        }

        [RequiredPermission(ActionType.ShowUserGroup)]
        public UserGroupDTO GetUserGroupByName(string groupName)
        {
            Group group = userRep.GetUserGroupById(new PartyId(groupName));
            return userGroupDTOMapper.MapToModel(group);
        }

        public UserGroupDTO AddUserGroup(UserGroupDTO userGroup)
        {
            Group u = securityService.AddUserGroup(new PartyId(userGroup.PartyName), userGroup.Description, userGroup.CustomActions);
            return userGroupDTOMapper.MapToModel(u);
        }

        public UserGroupDTO UpdateUserGroup(UserGroupDTO userGroup)
        {
            Group u = securityService.UpdateUserGroup(new PartyId(userGroup.PartyName), userGroup.Description, userGroup.CustomActions);
            return userGroupDTOMapper.MapToModel(u);
        }

        public string DeleteUserGroup(string groupName)
        {
            securityService.Delete(new PartyId(groupName));
            return "user group deleted successfully ";
        }

        //public List<ActionTypeDTO> GetAllActionTypes()
        //{
        //    var actionTypes = Enum.GetValues(typeof(ActionType));
        //    List<ActionTypeDTO> result = new List<ActionTypeDTO>();
        //    foreach (ActionType actionType in actionTypes)
        //    {
        //        result.Add(actionTypeDTOMapper.MapToModel(actionType));
        //    }
        //    return result.ToList();
        //}

        // Imported

        #region methods


        public void UpdateUserAccess(string username, Dictionary<int, bool> actionTyps)
        {
            _securityApplicationService.UpdateUserAccess(new PartyId(username), actionTyps);

        }

        #endregion

        public List<ActionType> GetAllActionTypes()
        {
            var actionTypes = Enum.GetValues(typeof(ActionType));
            List<ActionType> result = new List<ActionType>();
            foreach (ActionType actionType in actionTypes)
            {
                result.Add((actionType));
            }
            return result.ToList();
        }
        //public List<ActionType> GetAllUserActionTypes(long userId)
        //{
        //    var actionTypes = _securityApplicationService.GetAllAuthorizedActions(new List<User>()
        //    {
        //        new User(userId,"","","","",true,"")

        //    });
        //    return actionTypes;
        //}

        private Expression<Func<User, bool>> getUserPredicate(IEnumerable<string> criterias)
        {
            Expression<Func<User, bool>> res = user => true;

            foreach (var criteria in criterias)
            {
                var sp = criteria.Split(':');
                if (sp[0] == "FirstName" && !string.IsNullOrEmpty(sp[1]))
                    res = res.And(e => e.FirstName.Contains(sp[1]));
                if (sp[0] == "LastName" && !string.IsNullOrEmpty(sp[1]))
                    res = res.And(e => e.LastName.Contains(sp[1]));
                //if (sp[0] == "PartyName" && !string.IsNullOrEmpty(sp[1]))
                //    res = res.And(e => e.PartyName.Contains(sp[1]));
            }
            return res;
        }

        private List<string> getPredicate(IEnumerable<string> criterias)
        {
            var res = new List<string>();
            var fname = "";
            var lname = "";
            var uname = "";
            foreach (var criteria in criterias)
            {
                var sp = criteria.Split(':');
                if (sp[0] == "FirstName" && !string.IsNullOrEmpty(sp[1]))
                    fname = sp[1];
                else if (sp[0] == "LastName" && !string.IsNullOrEmpty(sp[1]))
                    lname = sp[1];
                else if (sp[0] == "PartyName" && !string.IsNullOrEmpty(sp[1]))
                    uname = sp[1];

            }

            res.Add(fname);
            res.Add(lname);
            res.Add(uname);
            return res;
        }


        public List<ActionType> GetGroupActionType(string groupId)
        {
            var res = new List<ActionType>();
        TODO:
            //var userGroup = userRep.GetAllUserGroup().SingleOrDefault(d => d.Id == groupId);
            //if (userGroup != null)
            //    userGroup.CustomActions.ForEach(c => res.Add(_actionTypeDTOMapper.MapToDTOModel(c.ActionType)));
            return res;
        }

        public string ChangePassword(ChangePasswordDTO changePassword)
        {
            userManagementService.ChangePassword(ClaimsPrincipal.Current.Identity.Name, changePassword.NewPassword,
                changePassword.OldPassword);
            return "Change password have done successfully";
        }
    }
}
