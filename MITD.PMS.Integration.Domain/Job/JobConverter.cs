
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
        private List<JobIndexDTO> jobIndexList;
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
        public void ConvertJobs(Period period, List<JobIndexInPeriodDTO> jobIndexInperiodListParam, List<JobIndexDTO> jobIndexListParam)
        {
            Console.WriteLine("Starting jobs convert progress...");
            jobIndexInperiodList = jobIndexInperiodListParam;
            jobIndexList = jobIndexListParam;
            var jobIdList = jobDataProvider.GetJobIds();
            totalJobsCount = jobIdList.Count;
            foreach (var jobId in jobIdList)
            {
                var sourceJobDTO = jobDataProvider.GetJobDetails(jobId);
                var job = jobService.GetByTransferId(sourceJobDTO.TransferId);
                if (job == null)
                {
                    var desJobDTO = createDestinationJob(sourceJobDTO);
                    job = jobService.AddJob(desJobDTO);    
                }
                var sourceJobIndicesForAssignment=jobDataProvider.GetJobIndecesByJobId(jobId);
                var jobInPriodAssignment = createDestinationJobInPeriod(job, sourceJobIndicesForAssignment);
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


        private JobInPeriodDTO createDestinationJobInPeriod(JobDTO job,List<JobIndexIntegrationDTO> selectedSourceJobindices)
        {
            //var sourceTranferIdList=selectedSourceJobindices.Select(s=>s.TransferId);
            var selectedJobIndexIdListLong = new List<long>();
            foreach (var item in selectedSourceJobindices)
            {
                var tempJobIndex=jobIndexList.First(j => j.TransferId == item.TransferId);
                selectedJobIndexIdListLong.Add(tempJobIndex.Id);

            }
            //var selectedJobIndexIdList = jobIndexList.Where(j => sourceTranferIdList.Contains(j.TransferId)).Select(j => j.Id);
            var res = new JobInPeriodDTO
            {
                Name = job.Name,
                CustomFields = new List<CustomFieldDTO>(),
                JobId = job.Id,
                //ParentId = parentId,
                JobIndices = jobIndexInperiodList.Where(j => selectedJobIndexIdListLong.Contains(j.JobIndexId)).Select(c => new JobInPeriodJobIndexDTO
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