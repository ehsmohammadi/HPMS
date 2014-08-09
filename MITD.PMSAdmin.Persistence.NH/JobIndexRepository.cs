using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Common;
using MITD.PMSAdmin.Domain.Model.JobIndices;
using NHibernate.Linq;

namespace MITD.PMSAdmin.Persistence.NH
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

        public IList<JobIndex> GetAllJobIndex()
        {
            return rep.GetAll<JobIndex>();
        }

        public IList<JobIndex> GetAllJobIndex(ListFetchStrategy<JobIndex> fs)
        {
            return rep.GetAll(fs);
        }


        public IList<JobIndexCategory> GetAllJobIndexCategory()
        {
            return rep.GetAll<JobIndexCategory>();
        }

        public IList<JobIndexCategory> GetAllJobIndexCategory(ListFetchStrategy<JobIndexCategory> fs)
        {
            return rep.GetAll<JobIndexCategory>(fs);
        }

        public IList<AbstractJobIndex> GetAll()
        {
            return rep.GetAll();
        }

        public AbstractJobIndex GetById(AbstractJobIndexId jobIndexId)
        {
            return  rep.FindByKey(jobIndexId);
        }

        public void Add(AbstractJobIndex jobIndex)
        {
            rep.Add(jobIndex);
        }

        public void Update(AbstractJobIndex jobIndex)
        {
            rep.Update(jobIndex);
        }

        public void Delete(AbstractJobIndexId jobIndexId)
        {
            var abstractJobIndex = GetById(jobIndexId);
            rep.Delete(abstractJobIndex);
        }

        public AbstractJobIndexId GetNextId()
        {
            using (var ctx = Session.SessionFactory.OpenStatelessSession())
            {
                ctx.BeginTransaction();
                var res = ctx.CreateSQLQuery("Select next value for dbo.AbstractJobIndexSeq").UniqueResult<long>();
                return new AbstractJobIndexId(res);
            }
        }

        public JobIndex GetJobIndex(AbstractJobIndexId id)
        {
            return rep.Single<JobIndex>(p => p.Id == id);
        }


        public JobIndexCategory GetJobIndexCategory(AbstractJobIndexId id)
        {
            return rep.Single<JobIndexCategory>(p => p.Id == id);
        }

        public JobIndexException ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new JobIndexDuplicateException("JobIndex", "DictionaryName");
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new JobIndexDeleteException("JobIndex", "Id");
            throw new Exception();
        }

        public JobIndexException TryConvertException(Exception exp)
        {
            JobIndexException res = null;
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
