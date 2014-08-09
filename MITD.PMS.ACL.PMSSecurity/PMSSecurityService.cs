using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using MITD.PMS.Interface;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.ACL.PMSSecurity
{
    public class PMSSecurityService : IPMSSecurityService
    {
        private readonly ISecurityServiceFacade securityService;
        private readonly IEmployeeService empService;

        public PMSSecurityService(ISecurityServiceFacade securityService, IEmployeeService empService)
        {
            this.securityService = securityService;
            this.empService = empService;
        }

        public Employee GetCurrentLoginEmployee(Period period)
        {
            var user=securityService.GetCurrentEmployeeUser();
            if (user == null)
                return null;
            return empService.GetBy(new EmployeeId(user.EmployeeNo, period.Id));
        }
    }
}
