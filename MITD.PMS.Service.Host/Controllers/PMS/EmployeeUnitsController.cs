using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class EmployeeUnitsController : ApiController
    {
        private readonly IEmployeeServiceFacade employeeService;


        public EmployeeUnitsController(IEmployeeServiceFacade employeeService)
        {
            this.employeeService = employeeService;
        }
        public EmployeeUnitsDTO GetEmployeeUnits( string employeeNo, long periodId)
        {
           // return employeeService.GetEmployeeJobPositions(employeeNo, periodId);
            return new EmployeeUnitsDTO();
        }

        public EmployeeUnitsDTO PutUnitsToEmployee(long periodId, string employeeNo,
            EmployeeUnitsDTO employeeJobPositions)
        {
           // return employeeService.AssignJobPositionsToEmployee(periodId,employeeNo,employeeJobPositions);
            return new EmployeeUnitsDTO();
        }
      
    }
}