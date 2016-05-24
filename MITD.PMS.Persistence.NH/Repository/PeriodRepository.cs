using System.Linq.Expressions;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using MITD.PMS.Common;

namespace MITD.PMS.Persistence.NH
{
    public class PeriodRepository : NHRepository, IPeriodRepository
    {
        private NHRepository<Period> rep;

        public PeriodRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public PeriodRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep = new NHRepository<Period>(unitOfWork);
        }


        public void Add(Period period)
        {
            rep.Add(period);
        }

        public Period GetById(PeriodId periodId)
        {
            return rep.FindByKey(periodId);
        }

        public void GetAll(ListFetchStrategy<Period> fs)
        {
            rep.GetAll(fs);
        }

        public List<Period> GetAll()
        {
            return rep.GetAll().ToList();
        }

        public void Delete(Period period)
        {
            rep.Delete(period);
        }

        public Period GetBy(Expression<Func<Period, bool>> precidate)
        {
            return rep.Single(precidate);
        }

        public void DeleteBasicData(PeriodId periodId)
        {
           // session.Connection.Close();

            session.CreateSQLQuery("DELETE FROM Employees_JobCustomField_Values  WHERE PeriodId = :id")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();


            session.CreateSQLQuery("DELETE FROM Employees_JobPositions WHERE  PeriodId = :id")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();

            session.CreateSQLQuery(
                "DELETE FROM Periods_Jobs_JobIndices WHERE  PeriodJobId in (select id from Periods_Jobs where PeriodId = :id)")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();

            session.CreateSQLQuery(
                "DELETE FROM Periods_JobIndices_CustomFields WHERE  PeriodJobIndexId in (select id from Periods_AbstractJobIndices where PeriodId = :id)")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();

            session.CreateSQLQuery(
                "DELETE FROM Periods_JobIndices WHERE  Id in (select id from Periods_AbstractJobIndices where PeriodId = :id)")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();

            session.CreateSQLQuery(
                "DELETE FROM Periods_JobIndexGroups WHERE  Id in (select id from Periods_AbstractJobIndices where PeriodId = :id)")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();

            session.CreateSQLQuery("DELETE FROM Periods_AbstractJobIndices WHERE  PeriodId = :id")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();

            session.CreateSQLQuery("DELETE FROM Periods_Jobs_CustomFields WHERE  PeriodId = :id")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();

            session.CreateSQLQuery("DELETE FROM Periods_JobPositions WHERE  PeriodId = :id")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();

            session.CreateSQLQuery("DELETE FROM Periods_Jobs WHERE  PeriodId = :id")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();


            session.CreateSQLQuery(
                "DELETE FROM Periods_Units_UnitIndices WHERE  PeriodUnitId in (select id from Periods_Units where PeriodId = :id)")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();

            session.CreateSQLQuery(
                "DELETE FROM Periods_UnitIndices_CustomFields WHERE  PeriodUnitIndexId in (select id from Periods_AbstractUnitIndices where PeriodId = :id)")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();


            session.CreateSQLQuery("DELETE FROM Periods_Units WHERE  PeriodId = :id")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();

            session.CreateSQLQuery("DELETE FROM Employees WHERE  PeriodId = :id")
                .SetParameter("id", periodId.Id)
                .ExecuteUpdate();

        }

        public List<Period> GetPeriodsWithDeterministicCalculation()
        {
            return session.Query<Period>()
                .Join(session.Query<Calculation>(), p => p.Id, c => c.PeriodId, (p, c) => new {c, p})
                .Where(r => r.c.IsDeterministic)
                .Select(r => r.p)
                .ToList();

        }

        public long GetNextId()
        {
            return session.CreateSQLQuery("select next value for [dbo].[PeriodSeq]").UniqueResult<long>();
        }


        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new PeriodDuplicateException(typeName, keyName);
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new PeriodDeleteException(typeName, keyName);
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
