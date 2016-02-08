using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;
using System.Transactions;
using MITD.PMS.Exceptions;


namespace MITD.PMS.Application
{
    public class JobService : IJobService
    {
        private readonly IPeriodRepository periodRep;
        private readonly IPMSAdminService pmsAdminService;
        private readonly IJobIndexRepository jobIndexRep;
        private readonly IPeriodManagerService periodChecker;
        private readonly IJobRepository jobRep;

        public JobService(

                        IJobRepository jobRep,
                          IPeriodRepository periodRep,
                          IPMSAdminService pmsAdminService,
                        IJobIndexRepository jobIndexRep,
                        IPeriodManagerService periodChecker
                        )
        {
            this.periodRep = periodRep;
            this.pmsAdminService = pmsAdminService;
            this.jobIndexRep = jobIndexRep;
            this.periodChecker = periodChecker;
            this.jobRep = jobRep;
        }


        public Job AssignJob(JobId jobId, List<SharedJobCustomFieldId> customFieldIdList, IList<JobIndexForJob> jobIndexList)
        {
            using (var tr = new TransactionScope())
            {
                var period = periodRep.GetById(jobId.PeriodId);
                var sharedJob = pmsAdminService.GetSharedJob(jobId.SharedJobId);

                var sharedJobCustomFields = pmsAdminService.GetSharedCutomFieldListForJob(jobId.SharedJobId, customFieldIdList);

                var jobCustomFields = new List<JobCustomField>();
                foreach (var sharedJobCustomField in sharedJobCustomFields)
                {
                    jobCustomFields.Add(new JobCustomField(new JobCustomFieldId(period.Id, sharedJobCustomField.Id, jobId.SharedJobId),
                        sharedJobCustomField
                        ));
                }
                var jobIndexIds=jobIndexList.Select(jj => jj.JobIndexId).ToList();
                var jobIndices = jobIndexRep.FindJobIndices(jobIndexIds);
                //job.UpdateJobIndices(jobIndices.ToList());
                var jobJobInddices = new List<JobJobIndex>();
                foreach (var jobIndex in jobIndices)
                {
                    var jobindexForJob = jobIndexList.Single(j => j.JobIndexId == jobIndex.Id);
                    jobJobInddices.Add(new JobJobIndex(jobIndex.Id, jobindexForJob.ShowforTopLevel,jobindexForJob.ShowforSameLevel,jobindexForJob.ShowforLowLevel));
                }

                var job = new Job(period, sharedJob, jobCustomFields, jobJobInddices);
                jobRep.Add(job);
                tr.Complete();
                return job;

            }
        }

        public void RemoveJob(JobId jobId)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var job = jobRep.GetById(jobId);
                    jobRep.DeleteJob(job);
                    tr.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = jobRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Job UpdateJob(JobId jobId, List<SharedJobCustomFieldId> customFieldIdList, IList<JobIndexForJob> jobIndexList)
        {
            using (var tr = new TransactionScope())
            {

                var job = jobRep.GetById(jobId);
                var sharedJobCustomFields = pmsAdminService.GetSharedCutomFieldListForJob(jobId.SharedJobId, customFieldIdList);

                var jobCustomFields = new List<JobCustomField>();
                foreach (var sharedJobCustomField in sharedJobCustomFields)
                {
                    jobCustomFields.Add(new JobCustomField(new JobCustomFieldId(jobId.PeriodId, sharedJobCustomField.Id, jobId.SharedJobId),
                        sharedJobCustomField
                        ));
                }
                job.UpdateCustomFields(jobCustomFields, periodChecker);
                var jobindexIdList = jobIndexList.Select(jj => jj.JobIndexId).ToList();
                var jobIndices = jobIndexRep.FindJobIndices(jobindexIdList);

                var jobJobIndices = new List<JobJobIndex>();
                foreach (var jobIndex in jobIndices)
                {
                    var jobindexForJob = jobIndexList.Single(j => j.JobIndexId == jobIndex.Id);
                    jobJobIndices.Add(new JobJobIndex(jobIndex.Id, jobindexForJob.ShowforTopLevel, jobindexForJob.ShowforSameLevel, jobindexForJob.ShowforLowLevel));
                }
                job.UpdateJobIndices(jobJobIndices, periodChecker);
                tr.Complete();
                return job;

            }
        }

        public Job GetJobById(JobId jobId)
        {
            using (var tr = new TransactionScope())
            {
                var job = jobRep.GetById(jobId);
                tr.Complete();
                return job;
            }
        }

        public List<JobId> GetAllJobIdList(PeriodId periodId)
        {
            using (var tr = new TransactionScope())
            {
                var jobId = jobRep.GetAllJobIdList(periodId);
                tr.Complete();
                return jobId;
            }
        }

        public JobId GetJobIdBy(Period currentPeriod, SharedJobId sharedJobId)
        {
            using (var tr = new TransactionScope())
            {
                var jobId = jobRep.GetJobIdBy(currentPeriod, sharedJobId);
                tr.Complete();
                return jobId;
            }
        }
    }
}
