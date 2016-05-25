using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Application.Contracts
{
    public interface IEmployeeService:IService
    {
        void Delete(EmployeeId employeeId);
        Employee GetBy(EmployeeId employeeId);
        Employee Add(PeriodId periodId, string personnelNo, string firstName, string lastName, Dictionary<SharedEmployeeCustomFieldId, string> customFields);
        Employee Update(EmployeeId employeeId, string firstName, string lastName, Dictionary<SharedEmployeeCustomFieldId, string> customFieldIdValueList);
        Employee AssignJobPositions(EmployeeId employeeId, IEnumerable<JobPositionDuration> jonPositionDurations);
        Employee UpdateFinalPoint(EmployeeId employeeId);
        IEnumerable<string> GetAllEmployeeNo(PeriodId periodId);
        void DeleteFinalPoint(EmployeeId employeeId);
    }
}
