
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
                jobDataProvider.GetJobIndecesByJobId(jobId);
                var jobInPriodAssignment = createDestinationJobInPeriod(job);
                jobInPeriodServiceWrapper.AddJobInPeriod(period.Id, jobInPriodAssignment);
                jobList.Add(job);
                Console.WriteLine("Jobs Convert progress state: " + jobList.Count + " From " + totalJobsCount.ToString());
                
            }
            publisher.Publish(new JobConverted(jobList));
        }

      
        #endregion

        #region Private methods


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