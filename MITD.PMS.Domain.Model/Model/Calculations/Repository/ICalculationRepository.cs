using System;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMSReport.Domain.Model;
using System.Collections;
using System.Collections.Generic;

namespace MITD.PMS.Domain.Model.Calculations
{
    public interface ICalculationRepository :IRepository
    {
        CalculationId GetNextId();
        void Add(Calculation calculation);
        Calculation GetById(CalculationId calculationId);

        IList<Calculation> FindBy(Periods.PeriodId periodId, ListFetchStrategy<Calculation> fs);

        IList<CalculationWithPolicyAndPeriod> FindByWithPolicy(Domain.Model.Periods.PeriodId periodId, 
            IListFetchStrategy<CalculationWithPolicyAndPeriod> fs);

        void Delete(Calculation calculation);
        void DeleteAllCalculation(Period period);
        bool HasDeterministicCalculation(Period period);
        IList<Calculation> GetAll(PeriodId periodId);
        Calculation GetDeterministicCalculation(Period period);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
    }
}
