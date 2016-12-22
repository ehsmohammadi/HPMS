using System.Collections.Generic;
using System.Security.Claims;
using MITD.Core;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public interface ISecurityServiceFacade : IService
    {
        bool IsAuthorized(ClaimsPrincipal user, List<ActionType> actions);
        
        List<ActionType> GetUserAuthorizedActions(ClaimsPrincipal currentUsername);

        List<ActionType> GetUserAuthorizedActions(string userName);

        void AddUpdateUser(ClaimsPrincipal incomingPrincipal);

        User GetLogonUser();

        EmployeeUser GetCurrentEmployeeUser();

        List<string> GetPermittedWorkListUserNameFor(string username);
        bool VerifyEmail(string veriCode);
    }
}
