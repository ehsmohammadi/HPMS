using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMSSecurity.Domain.Service
{
    public interface ISecurityCheckerService
    {
        List<ActionType> GetAllAuthorizedActions(List<User> pmsUsers);
        bool IsAuthorized(List<ActionType> pmsUsers, List<ActionType> actions);
    }
}
