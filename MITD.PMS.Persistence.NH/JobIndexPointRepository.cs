using System.Linq.Expressions;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.JobIndexPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Domain.Model.Periods;
using NHibernate.Linq;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMSReport.Domain.Model;

namespace MITD.PMS.Persistence.NH
{
    public class JobIndexPointRepository : NHRepository, IJobIndexPointRepository
    {
        private NHRepository<CalculationPoint> rep;
        
        private void init()
        {
            rep = new NHRepository<CalculationPoint>(unitOfWork);
        }
        public JobIndexPointRepository(NHUnitOfWork unitOfWork):base(unitOfWork)
        {
            init();
        }

        public JobIndexPointRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        public void Add(CalculationPoint point)
        {
            rep.Add(point);
        }

        public IList<CalculationPoint> Find(System.Linq.Expressions.Expression<Func<CalculationPoint, bool>> predicate, 
            IListFetchStrategy<CalculationPoint> fetchStrategy)
        {
            return rep.Find(predicate, fetchStrategy);
        }

        public IList<JobIndexPointWithEmployee> Find<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate,
            IListFetchStrategy<JobIndexPointWithEmployee> fetchStrategy) where T:EmployeePoint
        {
            var q = from j in session.Query<T>().Where(predicate)
                    join e in session.Query<Employee>() on j.EmployeeId equals e.Id
                    select new JobIndexPointWithEmployee{ JobIndexPoint =j, Employee= e };
            var res = FetchStrategyHelper.ExecuteQuery(q2 => q2.ToList(), q, fetchStrategy);
            fetchStrategy.PageCriteria.PageResult.Result = res;
            return res;
        }

        

        public CalculationPointId GetNextId()
        {
            var id = session.CreateSQLQuery("select next value for [dbo].[JobIndexPointSeq]").UniqueResult<long>();
            return new CalculationPointId(id);
        }

        public CalculationPoint GetById(CalculationPointId id)
        {
            return rep.Single(ep => ep.Id == id);
        }

        public List<SummaryCalculationPoint> GetCalculationPointBy(CalculationId calcId)
        {
            return rep.Find<SummaryCalculationPoint>(s=>s.CalculationId==calcId).ToList();
        }

        public void ResetAllInquiryPoints(Period period)
        {
            session.CreateSQLQuery("Update Inquiry_JobIndexPoints  set JobIndexValue = '' where PeriodId= :id ")
            .SetParameter("id", period.Id.Id)
                .ExecuteUpdate();

        }

        

        
    }
}
