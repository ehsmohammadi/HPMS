using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Application.Contracts;
using MITD.PMSSecurity.Domain;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
   // [Interceptor(typeof(Interception))]
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
        private IMapper<ActionType, ActionTypeDTO> actionTypeDTOMapper;

        public UserServiceFacade(ISecurityService securityService,
            IMapper<ClaimsPrincipal, UserStateDTO> userStateMapper, 
            IMapper<List<User>, ClaimsPrincipal> pmsUsersMapper,
            IUserRepository userRep ,
            IMapper<User, UserDTOWithActions> userDTOWithActionsMapper,
            IMapper<User, UserDTO> userDTOMapper,
            IMapper<Group, UserGroupDTOWithActions> userGroupDTOWithActionsMapper,
            IMapper<Group, UserGroupDTO> userGroupDTOMapper,
            IMapper<ActionType, ActionTypeDTO> actionTypeDTOMapper ,
            IMapper<Group, UserGroupDescriptionDTO> userGroupDescriptionDTOMapper,
            IMapper<User, UserDescriptionDTO> userDescriptionDTOMapper

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
            this.actionTypeDTOMapper = actionTypeDTOMapper;
            this.userGroupDescriptionDTOMapper = userGroupDescriptionDTOMapper;
            this.userDescriptionDTOMapper = userDescriptionDTOMapper;
        }

        
        public UserStateDTO GetUserState(ClaimsPrincipal u)
        {
            if(u == null)
                throw new NullReferenceException("Principal is null");
            var pmsUsers = pmsUsersMapper.MapToEntity(u);
            if(pmsUsers == null || pmsUsers.Count ==0)
                throw new Exception("pmsUsers is null or pmsUsers.count is zero");

            var userState = userStateMapper.MapToModel(u);

            var user=securityService.GetUser(new PartyId(u.Identity.Name));
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


        public PageResultDTO<UserDTOWithActions> GetAllUsers(int pageSize, int pageIndex, string  filter)
        {
            var fs = new ListFetchStrategy<User>(Enums.FetchInUnitOfWorkOption.NoTracking);
            fs.WithPaging(pageSize, pageIndex);
            fs.OrderBy(e => e.LastName);
            var criterias = filter.Split(';');
            var predicate = getUserPredicate(criterias);
            userRep.FindUsers(predicate, fs);
            var res = new PageResultDTO<UserDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result =
                fs.PageCriteria.PageResult.Result.Select(p => userDTOWithActionsMapper.MapToModel(p)).ToList();
            return res;

        }

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
                if (sp[0] == "PartyName" && !string.IsNullOrEmpty(sp[1]))
                    res = res.And(e => e.Id.PartyName.Contains(sp[1]));
            }
            return res;
        }


        public UserDTO GetUserByUsername(string username)
        {
            var user = userRep.GetUserById(new PartyId(username));
            var userDto = userDTOMapper.MapToModel(user);
            userDto.Groups = user.GroupList.Select(g => userGroupDescriptionDTOMapper.MapToModel(g)).ToList();
            userDto.WorkListPermittedUsers = user.WorkListUserList.Select(g => userDescriptionDTOMapper.MapToModel(g)).ToList();
            return userDto;
        }

        public UserDTO AddUser(UserDTO user)
        {
            var groupIdList = user.Groups.Select(p => new PartyId(p.PartyName)).ToList();
            var workListUserIdList = user.WorkListPermittedUsers.Select(p => new PartyId(p.PartyName)).ToList();
            var u = securityService.AddUser(new PartyId(user.PartyName), user.FirstName, user.LastName, user.Email, user.IsActive, user.CustomActions, groupIdList,workListUserIdList);
            return userDTOMapper.MapToModel(u);
        }

        public UserDTO UpdateUser(UserDTO user)
        {
            var groupIdList = user.Groups.Select(p => new PartyId(p.PartyName)).ToList();
            var workListUserIdList = user.WorkListPermittedUsers.Select(p => new PartyId(p.PartyName)).ToList();
            var u = securityService.UpdateUser(new PartyId(user.PartyName), user.FirstName, user.LastName, user.Email, user.IsActive, user.CustomActions, groupIdList,workListUserIdList);
            return userDTOMapper.MapToModel(u);
        }

        public string  DeleteUser(string username)
        {
            securityService.Delete(new PartyId(username));
            return "user deleted successfully ";
        }


        public List<UserGroupDTOWithActions> GetAllUserGroups()
        {
            var userGroups = userRep.GetAllUserGroup();
            return userGroups.Select(p => userGroupDTOWithActionsMapper.MapToModel(p)).ToList();
        }

        public UserGroupDTO GetUserGroupByName(string groupName)
        {
            Group group = userRep.GetUserGroupById(new PartyId(groupName));
            return userGroupDTOMapper.MapToModel(group);
        }

        public UserGroupDTO AddUserGroup(UserGroupDTO userGroup)
        {
            Group u = securityService.AddUserGroup(new PartyId(userGroup.PartyName), userGroup.Description,userGroup.CustomActions);
            return userGroupDTOMapper.MapToModel(u);
        }

        public UserGroupDTO UpdateUserGroup(UserGroupDTO userGroup)
        {
            Group u = securityService.UpdateUserGroup(new PartyId(userGroup.PartyName), userGroup.Description,userGroup.CustomActions);
            return userGroupDTOMapper.MapToModel(u);
        }

        public string  DeleteUserGroup(string groupName)
        {
            securityService.Delete(new PartyId(groupName));
            return "user group deleted successfully ";
        }

        public List<ActionTypeDTO> GetAllActionTypes()
        {
            var actionTypes = ActionType.GetAll<ActionType>();
            return actionTypes.Select(p => actionTypeDTOMapper.MapToModel(p)).ToList();
        }


    }
}
