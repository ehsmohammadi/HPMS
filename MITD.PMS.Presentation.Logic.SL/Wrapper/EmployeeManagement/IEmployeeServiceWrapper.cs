using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IEmployeeServiceWrapper:IServiceWrapper
    {
        void GetEmployee(Action<EmployeeDTO, Exception> action, long periodId, string employeeNo);
        void AddEmployee(Action<EmployeeDTO, Exception> action, EmployeeDTO employee);
        void UpdateEmployee(Action<EmployeeDTO, Exception> action, EmployeeDTO employee);
        void GetAllEmployees(Action<PageResultDTO<EmployeeDTOWithActions>, Exception> action, long periodId, int pageSize, int pageIndex);
        void DeleteEmployee(Action<string, Exception> action,long periodId,string personnelNo);
        void GetAllEmployees(Action<List<EmployeeDTO>, Exception> action,long periodId);


        void GetEmployeeJobPositionsInPeriod(Action<EmployeeJobPositionsDTO, Exception> action, string employeeNo, long periodId);
        void GetEmployeeUnitsInPeriod(Action<EmployeeUnitsDTO, Exception> action, string employeeNo, long periodId);



        void AssignJobPositionsToEmployee(Action<EmployeeJobPositionsDTO, Exception> action, long periodId, string employeeNo, EmployeeJobPositionsDTO employeeJobPositions);
        void GetAllEmployees(Action<PageResultDTO<EmployeeDTOWithActions>, Exception> action, long periodId, EmployeeCriteria employeeCriteria, int pageSize, int pageIndex);
        void GetAllEmployeeNo(Action<List<String>, Exception> action, long periodId, EmployeeCriteria employeeCriteria);
        void ConfirmEmployeeAboveMaxPoint(Action<Exception> action, string personnelNo, long periodId);
        void ChangeEmployeePoint(Action< Exception> action, EmployeeDTO employee);
    }
}
