using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using MITD.Core;
using MITD.PMSSecurity.Application.Contracts;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class SecurityServiceFacade : ISecurityServiceFacade
    {
        private readonly ISecurityService securityService;
        private readonly IMapper<List<User>, ClaimsPrincipal> pmsUsersMapper;
        private readonly IMapper<List<ActionType>, ClaimsPrincipal> userActionMapper;
        private readonly IUserRepository userRepository;

        public SecurityServiceFacade(ISecurityService securityService,
            IMapper<List<User>, ClaimsPrincipal> pmsUsersMapper,
            IMapper<List<ActionType>, ClaimsPrincipal> userActionMapper,
             IUserRepository userRepository)
        {
            this.securityService = securityService;
            this.pmsUsersMapper = pmsUsersMapper;
            this.userActionMapper = userActionMapper;
            this.userRepository = userRepository;

        }

        public bool IsAuthorized(ClaimsPrincipal user, List<ActionType> actions)
        {
            List<ActionType> userActions = userActionMapper.MapToEntity(user);
            return securityService.IsAuthorized(userActions, actions);
        }

        public List<ActionType> GetUserAuthorizedActions(ClaimsPrincipal currentUsername)
        {
            var pmsUsers = pmsUsersMapper.MapToEntity(currentUsername);
            return securityService.GetAllAuthorizedActions(pmsUsers);
        }

        public List<ActionType> GetUserAuthorizedActions(string userName)
        {
            var result = new List<ActionType>();
            var user = userRepository.GetUserById(new PartyId(userName));
            ServicePointManager.ServerCertificateValidationCallback
                   += (sender, certificate, chain, errors) => true;

            // Fake User
            User fakeUser = new AdminUser(new PartyId(userName), "", "", "");

            //var ums = new UserManagementServiceClient();
            //ums.Open();
            //var res = ums.GetRolesForUser(userName);
            //var actionRole = _actionTypeDTOMapper.MapToDtoModel(_securityServiceChecker.GetAllAuthorizedActionTypesForRole(res.ToList())).ToList();

            result.AddRange(fakeUser.Actions);
            //user.Groups.ForEach(c =>
            //{
            //    foreach (var action in c.CustomActions)
            //    {
            //        if (result.All(d => d.Id != action.ActionTypeId))
            //        {
            //            result.Add(_actionTypeDTOMapper.MapToDtoModel(action.ActionType));
            //        }
            //        else
            //        {
            //            if (!action.IsGranted)
            //            {
            //                result.Remove(result.Find(f => f.Id == action.ActionType.Id));
            //            }
            //        }
            //    }
            //});

            foreach (var action in user.CustomActions)
            {
                if (result.All(d => (int)d != action.Key))
                {
                    result.Add((ActionType)action.Key);
                }
                else
                {
                    if (!action.Value)
                    {
                        result.Remove(result.Find(f => (int)f == action.Key));
                    }
                }
            }
            return result;
        }

        public List<string> GetPermittedWorkListUserNameFor(string username)
        {
            var user = securityService.GetUser(new PartyId(username));
            return securityService.GetPermittedWorkListFor(user).Select(u => u.Id.PartyName).ToList();
        }

        public void AddUpdateUser(ClaimsPrincipal user)
        {
            var pmsUsers = pmsUsersMapper.MapToEntity(user);

            User u = pmsUsers.First();
            securityService.AddUpdate(u.Id, u.FirstName, u.LastName, u.Email);
        }

        public User GetLogonUser()
        {
            return securityService.GetLogonUser();
        }

        public EmployeeUser GetCurrentEmployeeUser()
        {
            if (ClaimsPrincipal.Current == null)
                return null;
            var pmsUsers = pmsUsersMapper.MapToEntity(ClaimsPrincipal.Current);
            return pmsUsers.SingleOrDefault(c => c is EmployeeUser) as EmployeeUser;

        }
    }
}
