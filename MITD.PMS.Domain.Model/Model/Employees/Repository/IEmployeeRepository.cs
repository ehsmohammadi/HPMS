using MITD.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMSReport.Domain.Model;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Employees
{
    public interface IEmployeeRepository:IRepository
    {
        IList<Employee> Find(Expression<Func<Employee, bool>> predicate);
        void Find(Expression<Func<Employee, bool>> predicate,ListFetchStrategy<Employee> fs);
        void Add(Employee employee);
        long GetNextId();
        Employee GetBy(EmployeeId employeeId);
        void Delete(Employee employee);
        Dictionary<int, IList<Employee>> FindInListWithPath(IList<string> enList, PeriodId periodId, int pathNo);
        IList<Employee> FindInList(IList<string> enList, PeriodId periodId);
        Employee First();
        Dictionary<int, IList<Employee>> FindRemainingEmployeesOfCalculation(List<string> employeesInCalc, PeriodId periodId, CalculationId calculationId, int pathNo);
        List<InquirySubjectWithJobPosition> GetEmployeeByWithJobPosition(IEnumerable<JobPositionInquiryConfigurationItemId> configurationIds, PeriodId inquirerPeriodId);
        CalculationData ProvideDataForRule(Employee emp, CalculationId calculationId, bool withCalculationPoints = false);
        EmployeePointData GetPoints(Employee emp, CalculationId calcId);
        void Attach(Employee employee);
        List<string> GetAllEmployeeNo(Expression<Func<Employee, bool>> predicate);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
    }
}
