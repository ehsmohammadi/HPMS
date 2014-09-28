using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.Periods;
using System.Linq.Expressions;
using MITD.PMS.Common;


namespace MITD.PMS.Persistence.NH
{
    public class JobIndexRepository : NHRepository, IJobIndexRepository
    {
        private NHRepository<AbstractJobIndex> rep;

        public JobIndexRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public JobIndexRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep = new NHRepository<AbstractJobIndex>(unitOfWork);
        }



        public List<JobIndex> GetAllJobIndex(PeriodId periodId)
        {
            return rep.Find<JobIndex>(u => u.PeriodId == periodId).ToList();
        }

        public List<JobIndexGroup> GetAllJobIndexGroup(PeriodId periodId)
        {
            return rep.Find<JobIndexGroup>(u => u.PeriodId == periodId).ToList();
        }

        public AbstractJobIndex GetById(AbstractJobIndexId jobIndexId)
        {
            return rep.FindByKey(jobIndexId);

        }

        public void Add(AbstractJobIndex jobIndex)
        {
            rep.Add(jobIndex);
        }

        public void Delete(AbstractJobIndexId jobIndexId)
        {
            var abstractIndex = GetById(jobIndexId);
            rep.Delete(abstractIndex);
        }

        public JobIndex GetJobIndexById(AbstractJobIndexId id)
        {
            return rep.Single<JobIndex>(u => u.Id == id);
        }

        public JobIndexGroup GetJobIndexGroupById(AbstractJobIndexId id)
        {
            return rep.Single<JobIndexGroup>(u => u.Id == id);
        }

        public IEnumerable<AbstractJobIndex> GetAll(PeriodId periodId)
        {
            return rep.Find(i => i.PeriodId == periodId).ToList();
        }

        public AbstractJobIndexId GetNextId()
        {
            using (var ctx = Session.SessionFactory.OpenStatelessSession())
            {
                ctx.BeginTransaction();
                var res = ctx.CreateSQLQuery("Select next value for dbo.Periods_AbstractJobIndexSeq").UniqueResult<long>();
                return new AbstractJobIndexId(res);
            }
        }

        public void Update(AbstractJobIndex jobIndex)
        {
            rep.Update(jobIndex);
        }

        public IEnumerable<JobIndex> FindJobIndices(Expression<Func<JobIndex, bool>> predicate)
        {
            return rep.Find(predicate).ToList();
        }

        public List<AbstractJobIndex> GetAllAbstractJobIndexByParentId(AbstractJobIndexId id)
        {
            var jobIndexChilds = rep.Find<JobIndex>(u => u.Group.Id == id).ToList();
            var jobIndexGroupChilds = rep.Find<JobIndexGroup>(u => u.Parent.Id == id).ToList();

            List<AbstractJobIndex> res = new List<AbstractJobIndex>(jobIndexChilds);
            res.AddRange(jobIndexGroupChilds);
            return res;
        }

        public List<JobIndexGroup> GetAllParentJobIndexGroup(Period period)
        {
            return rep.Find<JobIndexGroup>(u => u.Parent == null && u.PeriodId == period.Id).ToList();
        }

        public SharedJobIndexId GetSharedJobIndexIdBy(AbstractJobIndexId abstractJobIndexId)
        {
            //return rep.Find<JobIndex>(i => abstractJobIndexIds.Contains(i.Id)).Select(i => i.SharedJobIndexId).ToList();
            return rep.Single<JobIndex>(j => j.Id == abstractJobIndexId).SharedJobIndexId;
        }

        public AbstractJobIndexId GetJobIndexIdBy(Period period, SharedJobIndexId sharedJobIndexId)
        {
            return rep.Single<JobIndex>(j => j.PeriodId == period.Id && j.SharedJobIndexId.Id == sharedJobIndexId.Id).Id;
        }

        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new JobIndexDuplicateException(typeName, keyName);
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new JobIndexDeleteException(typeName, keyName);
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
