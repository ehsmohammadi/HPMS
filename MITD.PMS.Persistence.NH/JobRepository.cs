using System.Collections.Generic;
using System.Linq;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Units;
using System;
using MITD.PMS.Common;

namespace MITD.PMS.Persistence.NH
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

        public List<Job> GetAllJob(PeriodId periodId)
        {
            return rep.Find(u => u.Id.PeriodId == periodId).ToList();
        }

        public void Add(Job job)
        {
            rep.Add(job);
        }

        public void DeleteJob(Job job)
        {
            rep.Delete(job);
        }

        public List<JobId> GetAllJobIdList(PeriodId periodId)
        {
            return rep.Find(u => u.Id.PeriodId == periodId).Select(j=>j.Id).ToList();
        }

        public JobId GetJobIdBy(Period currentPeriod, SharedJobId sharedJobId)
        {
            return rep.Find(j => j.Id.PeriodId == currentPeriod.Id && j.Id.SharedJobId == sharedJobId).Single().Id;
        }

        public Job GetById(JobId jobId)
        {
            return rep.Find(u => u.Id.PeriodId == jobId.PeriodId && u.Id.SharedJobId == jobId.SharedJobId).SingleOrDefault();
        }


        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new JobDuplicateException(typeName, keyName);
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new JobDeleteException(typeName, keyName);
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
