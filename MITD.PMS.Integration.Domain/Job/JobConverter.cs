
using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class JobConverter : IJobConverter
    {
        #region Fields
        private readonly IJobDataProvider jobDataProvider;
        private readonly IJobServiceWrapper jobService;
        private readonly IJobInPeriodServiceWrapper jobInPeriodServiceWrapper;
        private List<JobIndexInPeriodDTO> jobIndexInperiodList;
        private JobIntegrationDto root;
        private List<JobDTO> jobList = new List<JobDTO>();
        private int totalJobsCount;
        private readonly IEventPublisher publisher;
        #endregion

        #region Constructors
        public JobConverter(IJobDataProvider jobDataProvider, IJobServiceWrapper jobService, IJobInPeriodServiceWrapper jobInPeriodServiceWrapper, IEventPublisher publisher)
        {
            this.jobDataProvider = jobDataProvider;
            this.jobService = jobService;
            this.jobInPeriodServiceWrapper = jobInPeriodServiceWrapper;
            this.publisher = publisher;
        }

        #endregion

        #region public methods
        public void ConvertJobs(Period period, List<JobIndexInPeriodDTO> jobIndexInperiodListParam)
        {
            Console.WriteLine("Starting jobs convert progress...");
            jobIndexInperiodList = jobIndexInperiodListParam;
            var jobIdList = jobDataProvider.GetJobIds();
            totalJobsCount = jobIdList.Count;
            foreach (var jobId in jobIdList)
            {
                var sourceJobDTO = jobDataProvider.GetJobDetails(jobId);
                var desJobDTO = createDestinationJob(sourceJobDTO);
                var job = jobService.AddJob(desJobDTO);
                var jobInPriodAssignment = createDestinationJobInPeriod(job);
                var res = jobInPeriodServiceWrapper.AddJobInPeriod(period.Id, jobInPriodAssignment);
                jobList.Add(job);
                Console.WriteLine("Jobs Convert progress state: " + jobList.Count + " From " + totalJobsCount.ToString());
                
            }
            publisher.Publish(new JobConverted(jobList));
        }

      
        #endregion

        #region Private methods


        private void convertJob_Rec(JobIntegrationDto sourceJobDTO, long periodId, long? jobParentIdParam)
        {
            //var jobInPeriodList = new List<JobInPeriodDTO>();
            //var desJobDTO = createDestinationJob(sourceJobDTO);
            //jobService.AddJob((job, addJobExp) =>
            //{
            //    if (addJobExp != null)
            //        handleException(addJobExp);

            //    var jobInPriodAssignment = createDestinationJobInPeriod(job, jobParentIdParam);

            //    jobInPeriodServiceWrapper.AddJobInPeriod((res, exp) =>
            //    {
            //        if (exp != null)
            //            handleException(exp);
            //        jobInPeriodList.Add(res);
            //        var jobDataChildIdList = jobDataProvider.GetChildIDs(sourceJobDTO.Id);
            //        foreach (var jobDataChildId in jobDataChildIdList)
            //        {
            //            var jobdataChid = jobDataProvider.GetJobDetail(jobDataChildId);
            //            convertJob_Rec(jobdataChid, periodId, res.JobId);
            //        }
            //        if (jobInPeriodList.Count==totalJobsCount)
            //        {
            //            publisher.Publish(new JobConverted(jobInPeriodList));
            //        }
            //    }, periodId, jobInPriodAssignment);
            //}, desJobDTO);

        }

        private void handleException(Exception exception)
        {
            throw new Exception("Error In Add Job", exception);
        }


        private JobDTO createDestinationJob(JobIntegrationDto sourceJob)
        {
            var res = new JobDTO
            {
                Name = sourceJob.Title,

                CustomFields = new List<CustomFieldDTO>(),
                DictionaryName = sourceJob.Id.ToString(),
                TransferId = sourceJob.TransferId
                
            };
            return res;
        }


        private JobInPeriodDTO createDestinationJobInPeriod(JobDTO job)
        {
            var res = new JobInPeriodDTO
            {
                Name = job.Name,
                CustomFields = new List<CustomFieldDTO>(),
                JobId = job.Id,
                //ParentId = parentId,
                JobIndices = jobIndexInperiodList.Select(c => new JobInPeriodJobIndexDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsInquireable = c.IsInquireable,
                    ShowforLowLevel =true,
                    ShowforSameLevel = true,
                    ShowforTopLevel = true
                }).ToList()
            };
            return res;
        }


        #endregion
    }


}