using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Common;
using MITD.PMSAdmin.Domain.Model.Jobs;
using MITD.PMSAdmin.Exceptions;
using NHibernate.Linq;

namespace MITD.PMSAdmin.Persistence.NH
{
    public class JobRepository : NHRepository, IJobRepository
    {
        private NHRepository<Job> rep;
        public JobRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public JobRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep = new NHRepository<Job>(unitOfWork);
        }




        public void GetAllJob(ListFetchStrategy<Job> fs)
        {
            rep.GetAll(fs);
        }

        public IList<Job> GetAllJob()
        {
            return rep.GetAll();
        }


        public void AddJob(Job job)
        {
            rep.Add(job);
        }

        public void UpdateJob(Job job)
        {
            rep.Update(job);
        }

        public Job GetById(JobId jobId)
        {
            return rep.FindByKey(jobId);
            
        }

        public void DeleteJob(Job job)
        {
            rep.Delete(job);
        }

        public JobId GetNextId()
        {
            using (var ctx = Session.SessionFactory.OpenStatelessSession())
            {
                ctx.BeginTransaction();
                var res = ctx.CreateSQLQuery("Select next value for dbo.JobSeq").UniqueResult<long>();
                return new JobId(res);
            }
        }

        public JobException ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new JobDuplicateException("Job","DictionaryName");
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new JobDeleteException("Job", "Id");
            throw new Exception();
        }

        public JobException TryConvertException(Exception exp)
        {
            JobException res = null;
            try
            {
                res=ConvertException(exp);
            }
            catch (Exception e)
            {

            }
            return res;
        }
    }
}
