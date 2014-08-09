using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMSReport.Domain.Model;
using System.Collections;
using System.Collections.Generic;

namespace MITD.PMS.Domain.Model.CalculationExceptions
{
    public interface ICalculationExceptionRepository :IRepository
    {
        EmployeeCalculationExceptionId GetNextId();
        void Add(EmployeeCalculationException calculationException);
        EmployeeCalculationException GetById(EmployeeCalculationExceptionId exceptionCalculationId);
        IList<EmployeeCalculationException> GetAllBy(CalculationId calculationId);
        void DeleteAll(CalculationId calculationId);
        IList<EmployeeCalculationException> FindBy(CalculationId calculationId, ListFetchStrategy<EmployeeCalculationException> fs);
    }
}
