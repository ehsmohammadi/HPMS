using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public interface ISecurityServiceFacade : IService
    {
        //bool IsAuthorized(string className, string methodName, ClaimsPrincipal user);
        bool IsAuthorized(ClaimsPrincipal user, List<ActionType> actions);
        
        List<ActionType> GetUserAuthorizedActions(ClaimsPrincipal currentUsername);
        List<ActionType> GetAllAuthorizedActions(List<User> users);

        List<ActionType> GetUserAuthorizedActions(string userName);

        void AddUpdateUser(ClaimsPrincipal incomingPrincipal);
        User GetLogonUser();
        EmployeeUser GetCurrentEmployeeUser();
        List<string> GetPermittedWorkListUserNameFor(string username);
    }
}
