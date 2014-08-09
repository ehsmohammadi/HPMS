using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Application.Contracts
{
    public interface IUserManagementService
    {
        IList<string> GetRolesForUser(string userName);
    }
}
