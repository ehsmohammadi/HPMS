using System;
using System.Threading;
using System.Collections.Generic;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Presentation.Contracts;
using System.Threading.Tasks;


namespace MITD.PMS.Integration.Domain
{
    public class JobConverter_Old
    {
        private IJobDataProvider jobDataProvider;
        private readonly IJobServiceWrapper jobService;
        public int ProgressCount;

        public JobConverter_Old(IJobDataProvider jobDataProvider, IJobServiceWrapper jobService, IJobInPeriodServiceWrapper JobInPeriodService, IPeriodServiceWrapper PeriodService)
        {

            this.jobDataProvider = jobDataProvider;
            this.jobService = jobService;
            ProgressCount = 0;

        }


        private async Task GetJobsSync(PeriodDTO periodDto)
        {
            Task<int> GetJobs = GetJobsAsync(periodDto);

            int result = await GetJobs;

            
        }

        private async Task<int> GetJobsAsync(PeriodDTO periodDto) 
        {
            var idList = jobDataProvider.GetJobIds();

            foreach (var id in idList)
            {
                Random rndNumber = new Random();

                var jobDetail = jobDataProvider.GetJobDetails(id);
                var desJob = new JobDTO();
                desJob.Name = jobDetail.Title;
                desJob.DictionaryName = "DicName" + Guid.NewGuid();
                desJob.CustomFields = new List<CustomFieldDTO>();

                JobDTO CurrentJob = new JobDTO();
                CurrentJob = GetJob(jobDetail.Id);

                if (CurrentJob.Id != null && CurrentJob.Id != 0)
                {
                    jobService.UpdateJob((res, exp) =>
                    {
                        if (exp != null)
                        {
                            throw new Exception("Error In Update Job!");
                        }
                        //System.Threading.Thread.Sleep(100);
                        //ProgressCount++;

                    }, CurrentJob);
                }
                else
                {
                    jobService.AddJob(
                        (r, e) =>
                        {
                            if (e != null)
                                throw new Exception("Error In AddJob!");
                            //System.Threading.Thread.Sleep(100);
                            //ProgressCount++;

                        }, desJob);
                }

                ProgressCount++;

            }
            return ProgressCount;
        }

        public void ConvertJob(PeriodDTO periodDto)
        {
            GetJobsSync(periodDto);

        }

        private JobDTO GetJob(long ID)
        {
            JobDTO Result = new JobDTO();

            jobService.GetJob((res, exp) =>
                    {
                        if (exp == null)
                        {
                            //System.Threading.Thread.Sleep(100);
                            Result = res;
                        }
                        else
                            throw new Exception("Error in Get Job From Repository!");
                    }, ID);

            return Result;
        }
    }
}
