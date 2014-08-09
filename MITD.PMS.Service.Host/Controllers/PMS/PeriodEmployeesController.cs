using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class PeriodEmployeesController : ApiController
    {
        private readonly IEmployeeServiceFacade employeeService;

        public PeriodEmployeesController(IEmployeeServiceFacade employeeService)
        {
            this.employeeService = employeeService;
        }
       
        public PageResultDTO<EmployeeDTOWithActions> GetAllEmployees(long periodId, int pageSize, int pageIndex)
        {
            return employeeService.GetAllEmployees(periodId, pageSize, pageIndex);
        }

        public PageResultDTO<EmployeeDTOWithActions> GetAllEmployees(long periodId, int pageSize, int pageIndex,
                                                                     string filter)
        {
            return employeeService.GetAllEmployees(periodId, pageSize, pageIndex,filter);
        }

        public List<string> GetAllEmployeeNo(long periodId,string filter)
        {
            return employeeService.GetAllEmployeeNo(periodId, filter);
        }

        public List<EmployeeDTO> GetAllEmployees(long periodId)
        {
            return employeeService.GetAllEmployees(periodId);
        }

        public string DeleteEmployee(long periodId, string personnelNo)
        {
            return employeeService.DeleteEmployee(periodId, personnelNo);
        }

        public EmployeeDTO GetEmployee(long periodId, string employeeNo)
        {
            return employeeService.GetEmployee(periodId, employeeNo);
        }

        public EmployeeDTO PostEmployee(long periodId, EmployeeDTO employee)
        {
            return employeeService.AddEmployee(periodId, employee);
        }

        public EmployeeDTO PutEmployee(long periodId, EmployeeDTO employee)
        {
            return employeeService.UpdateEmployee(periodId, employee);
        }
    }
}