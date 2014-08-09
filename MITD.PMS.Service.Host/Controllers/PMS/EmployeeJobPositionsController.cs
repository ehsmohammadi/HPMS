using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class EmployeeJobPositionsController : ApiController
    {
        private readonly IEmployeeServiceFacade employeeService;


        public EmployeeJobPositionsController(IEmployeeServiceFacade employeeService)
        {
            this.employeeService = employeeService;
        }
        public EmployeeJobPositionsDTO GetEmployeeJobPositions( string employeeNo, long periodId)
        {
            return employeeService.GetEmployeeJobPositions(employeeNo, periodId);
        }

        public EmployeeJobPositionsDTO PutJobPositionsToEmployee(long periodId, string employeeNo,
            EmployeeJobPositionsDTO employeeJobPositions)
        {
            return employeeService.AssignJobPositionsToEmployee(periodId,employeeNo,employeeJobPositions);
        }
      
    }
}