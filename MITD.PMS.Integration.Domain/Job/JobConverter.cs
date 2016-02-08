
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.PMS.API;
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
        private List<JobInPeriodDTO> jobInPeriodList = new List<JobInPeriodDTO>();
        //private JobInPeriodServiceWrapper jobAssignmentService;
        //private JobServiceWrapper jobService;
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
            //root = jobDataProvider.GetRoot();
            //convertJob_Rec(root, period.Id, null);
            var JobIdList = jobDataProvider.GetJobIds();
            totalJobsCount = JobIdList.Count;
            foreach (var JobId in JobIdList)
            {
                var sourceJobDTO = jobDataProvider.GetJobDetails(JobId);
                var desJobDTO = createDestinationJob(sourceJobDTO);
                jobService.AddJob((job, addJobExp) =>
                {
                    if (addJobExp != null)
                        handleException(addJobExp);

                    var jobInPriodAssignment = createDestinationJobInPeriod(job);

                    jobInPeriodServiceWrapper.AddJobInPeriod((res, exp) =>
                    {
                        if (exp != null)
                            handleException(exp);
                        jobInPeriodList.Add(res);
                        //var jobDataChildIdList = jobDataProvider.GetChildIDs(sourceJobDTO.Id);
                        //foreach (var jobDataChildId in jobDataChildIdList)
                        //{
                            //var jobdataChid = jobDataProvider.GetJobDetails(jobDataChildId);
                            //convertJob_Rec(jobdataChid, periodId, res.JobId);
                        //}
                        Console.WriteLine("Jobs Convert progress state: " + jobInPeriodList.Count + " From " + totalJobsCount.ToString());
                        if (jobInPeriodList.Count == totalJobsCount)
                        {
                            publisher.Publish(new JobConverted(jobInPeriodList));
                        }
                    },period.Id, jobInPriodAssignment);
                }, desJobDTO);
            }
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


