using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Service;
using MITD.PMSReport.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Persistence.NH
{
    public class CalculationRepository : NHRepository, ICalculationRepository
    {
        private NHRepository<Calculation> rep;

        public CalculationRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public CalculationRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep = new NHRepository<Calculation>(unitOfWork);
        }

        public void Add(Calculation calculation)
        {
            rep.Add(calculation);
        }

        public Calculation GetById(CalculationId calculationId)
        {
            return rep.FindByKey(calculationId);
        }

        public CalculationId GetNextId()
        {
            var id = session.CreateSQLQuery("select next value for [dbo].[CalculationSeq]").UniqueResult<long>();
            return new CalculationId(id);
        }


        public IList<Calculation> FindBy(Domain.Model.Periods.PeriodId periodId, ListFetchStrategy<Calculation> fs)
        {
            return rep.Find(c => c.PeriodId == periodId, fs);
        }


        public IList<CalculationWithPolicyAndPeriod> FindByWithPolicy(Domain.Model.Periods.PeriodId periodId,
            IListFetchStrategy<CalculationWithPolicyAndPeriod> fs)
        {
            var q = from c in session.Query<Calculation>()
                    where c.PeriodId == periodId
                    join p in session.Query<Policy>() on c.PolicyId equals p.Id
                    join pr in session.Query<Period>() on c.PeriodId equals pr.Id
                    select new CalculationWithPolicyAndPeriod { Calculation = c, Policy = p, Period = pr };
            var res = FetchStrategyHelper.ExecuteQuery(q2 => q2.ToList(), q, fs);
            fs.PageCriteria.PageResult.Result = res;
            return res;
        }


        public void Delete(Calculation calculation)
        {
            session.CreateQuery("DELETE FROM JobIndexPoint o WHERE o.CalculationId = :id")
                .SetParameter("id", calculation.Id)
                .ExecuteUpdate();
            session.CreateQuery("DELETE FROM SummaryJobPositionPoint o WHERE o.CalculationId = :id")
                .SetParameter("id", calculation.Id)
                .ExecuteUpdate();
            session.CreateQuery("DELETE FROM SummaryEmployeePoint o WHERE o.CalculationId = :id")
                .SetParameter("id", calculation.Id)
                .ExecuteUpdate();
            session.CreateQuery("DELETE FROM SummaryCalculationPoint o WHERE o.CalculationId = :id")
            .SetParameter("id", calculation.Id)
            .ExecuteUpdate();
            session.CreateQuery("DELETE FROM EmployeeCalculationException o WHERE o.CalculationId = :id")
                .SetParameter("id", calculation.Id)
                .ExecuteUpdate();
            rep.Delete(calculation);
        }

        public void DeleteAllCalculation(Period period)
        {
            var allCalcs = rep.Find(c => c.PeriodId == period.Id);
            foreach (Calculation calc in allCalcs)
                Delete(calc);
        }

        public bool HasDeterministicCalculation(Period period)
        {
            return rep.GetQuery().Any(p => p.PeriodId == period.Id && p.IsDeterministic);
        }

        public IList<Calculation> GetAll(PeriodId periodId)
        {
            return rep.Find(c => c.PeriodId == periodId);
        }

        public Calculation GetDeterministicCalculation(Period period)
        {
            return rep.Find(c => c.PeriodId == period.Id && c.IsDeterministic).SingleOrDefault();
        }

        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new CalculationDuplicateException("Calculation", "Name");
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new CalculationDeleteException("Calculation", "Id");
            throw new Exception();
        }

        public Exception TryConvertException(Exception exp)
        {
            Exception res = null;
            try
            {
                res = ConvertException(exp);
            }
            catch (Exception e)
            {

            }
            return res;
        }
    }
}
