using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IEmployeeServiceFacade : IFacadeService
    {
        PageResultDTO<EmployeeDTOWithActions>  GetAllEmployees(long periodId, int pageSize, int pageIndex);

        List<EmployeeDTO>  GetAllEmployees(long periodId);

        string DeleteEmployee(long periodId, string personnelNo);

        EmployeeDTO GetEmployee(long periodId, string employeeNo);

        EmployeeDTO AddEmployee(long periodId, EmployeeDTO employee);

        EmployeeDTO UpdateEmployee(long periodId, EmployeeDTO employee);

        EmployeeJobPositionsDTO GetEmployeeJobPositions(string employeeNo, long periodId);
        EmployeeJobPositionsDTO AssignJobPositionsToEmployee(long periodId, string employeeNo, EmployeeJobPositionsDTO employeeJobPositions);
        PageResultDTO<EmployeeDTOWithActions> GetAllEmployees(long periodId, int pageSize, int pageIndex, string filter);
        List<string> GetAllEmployeeNo(long periodId, string filter);
        void ConfirmAboveMaxEmployeePoint(long periodId, string employeeNo);
        void ChangeEmployeePoint(long periodId, string employeeNo, decimal point);
    }
}
