using System.Collections.Generic;
using System.Linq;
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

        public SecurityServiceFacade(ISecurityService securityService,
            IMapper<List<User>, ClaimsPrincipal> pmsUsersMapper,
            IMapper<List<ActionType>, ClaimsPrincipal> userActionMapper
            )
        {
            this.securityService = securityService;
            this.pmsUsersMapper = pmsUsersMapper;
            this.userActionMapper = userActionMapper;
        }

        public bool IsAuthorized(ClaimsPrincipal user, List<ActionType> actions)
        {
            var methodMapper = new MethodMapper();
            //var methodRequiredActions = methodMapper.Map(className, methodName);
            List<ActionType> userActions = userActionMapper.MapToEntity(user);
            return securityService.IsAuthorized(userActions, actions);          
        }

        public List<ActionType> GetUserAuthorizedActions(ClaimsPrincipal currentUsername)
        {
             var pmsUsers = pmsUsersMapper.MapToEntity(currentUsername);
            return securityService.GetAllAuthorizedActions(pmsUsers);
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
