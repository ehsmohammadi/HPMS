using System;
using System.Threading;
using System.Collections.Generic;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Presentation.Contracts;
using System.Threading.Tasks;


namespace MITD.PMS.Integration.Domain
{
    public class JobIndexConverter_OLD
    {
        private IJobIndexDataProvider jobIndexDataProvider;
        private readonly IJobIndexServiceWrapper jobIndexService;
        public int ProgressCount;

        public JobIndexConverter_OLD(IJobIndexDataProvider jobIndexDataProvider, IJobIndexServiceWrapper jobIndexService, IJobIndexInPeriodServiceWrapper JobIndexInPeriodService, IPeriodServiceWrapper PeriodService)
        {

            this.jobIndexDataProvider = jobIndexDataProvider;
            this.jobIndexService = jobIndexService;
            ProgressCount = 0;

        }


        #region Convert General Jobexes

        public void ConvertGeneralJobexes(PeriodDTO periodDto)
        {
            GetGeneralJobIndexessSync(periodDto);
        }


        private async Task<int> GetGeneralJobIndexessSync(PeriodDTO periodDto)
        {
            Task<int> GetGeneralJobIndexes = GetGeneralJobIndexesAsync(periodDto);

            int GeneralJobIndexesList = await GetGeneralJobIndexes;
            return GeneralJobIndexesList;
        }

        private async Task<int> GetGeneralJobIndexesAsync(PeriodDTO periodDto)
        {
            var GeneralIndexesList = jobIndexDataProvider.GetGeneralIndexes();

            foreach (var TempGeneralIndex in GeneralIndexesList)
            {


                //var personDetail = employeeDataProvider.GetEmployeeDetails(id);
                var desGeneralJobIndex = new JobIndexDTO();
                desGeneralJobIndex.Name = TempGeneralIndex.IndexTitle;

                desGeneralJobIndex.CustomFields = new List<CustomFieldDTO>();

                jobIndexService.AddJobIndex(
                    (r, e) =>
                    {
                        if (e != null)
                            throw new Exception("Error In General Job Indexes Converting");
                        ProgressCount++;

                    }, desGeneralJobIndex);
                //System.Threading.Thread.Sleep(500);
            }
            return ProgressCount;
        }

        #endregion


        #region ConvertExclusiveJobexes

        public void ConvertExclusiveJobexes(PeriodDTO periodDto)
        {
            ProgressCount = 0;
            GetExclusiveJobIndexessSync(periodDto);
            System.Threading.Thread.Sleep(500);
        }


        private async Task<int> GetExclusiveJobIndexessSync(PeriodDTO periodDto)
        {
            Task<int> GetExclusiveJobIndexes = GetExclusiveJobIndexesAsync(periodDto);

            int ExclusiveJobIndexesList = await GetExclusiveJobIndexes;
            return ExclusiveJobIndexesList;
        }

        private async Task<int> GetExclusiveJobIndexesAsync(PeriodDTO periodDto)
        {
            var ExclusiveIndexesList = jobIndexDataProvider.GetExclusiveJobIndexes();

            foreach (var TempExclusiveIndex in ExclusiveIndexesList)
            {


                //var personDetail = employeeDataProvider.GetEmployeeDetails(id);
                var desExclusiveJobIndex = new JobIndexDTO();
                desExclusiveJobIndex.Name = TempExclusiveIndex.IndexTitle;

                desExclusiveJobIndex.CustomFields = new List<CustomFieldDTO>();

                jobIndexService.AddJobIndex(
                    (r, e) =>
                    {
                        if (e != null)
                            throw new Exception("Error In Exclusive Job Indexes Converting");
                        ProgressCount++;

                    }, desExclusiveJobIndex);
                //System.Threading.Thread.Sleep(500);
            }
            return ProgressCount;
        }

        #endregion

    }
}
