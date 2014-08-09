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
        bool IsAuthorize(string className, string methodName, ClaimsPrincipal user);

        List<ActionType> GetUserAuthorizedActions(ClaimsPrincipal currentUsername);
        void AddUpdateUser(ClaimsPrincipal incomingPrincipal);
        User GetLogonUser();
        EmployeeUser GetCurrentEmployeeUser();
        List<string> GetPermittedWorkListUserNameFor(string username);
    }
}
