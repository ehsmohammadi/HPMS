using System;
using System.Collections.Generic;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Presentation.Contracts;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.Domain
{
    public class PeriodDataProvider
    {
        private readonly IPeriodServiceWrapper periodService;



        #region Public Methods



        public PageResultDTO<PeriodDTOWithAction> GetAllPeriods()
        {
            return GetAllPeriodsSync().Result;

        }


        public PeriodDataProvider(IPeriodServiceWrapper periodService)
        {
            this.periodService = periodService;
        }

        public Period GetPeriodInformationMain(long id)
        {
            var result = new Period();
            var temResult=new PeriodDTO();
            periodService.GetPeriod((res, exp) =>
            {
                if (exp != null)
                {
                    throw new Exception("Error In Get Period Info From HPMS!");
                }
                temResult = res;
            }, id);

            result.Id=temResult.Id;
            result.State = new PeriodState { Name = temResult.StateName };
            result.Name = temResult.Name;

            return result;
        }


        public Period GetPeriodInformation(long id)
        {
            var result = new Period();
            //Result.ID = id;
            //Result.StateName = id.ToString();
            //Result.PeriodName = "Period " + id.ToString();

            var periodList = GetPeriodsList();

            foreach (var item in periodList)
            {
                if (item.Id==id)
                {
                    return item;
                }
            }

            return result;
        }


        #region Get Periods


        public List<Period> GetPeriodsList()
        {

            var periodList = GetAllPeriods();

            var result = new List<Period>();

            if (periodList.Result != null)
            {
                foreach (var item in periodList.Result)
                {
                    Period tempPeriod = new Period();
                    tempPeriod.Id = item.Id;
                    tempPeriod.Name = item.Name;
                    tempPeriod.State = new PeriodState { Name = item.StateName };
                    result.Add(tempPeriod);
                }
            }
            else
                throw new Exception("Error in Get Period List!");

            return result;

        }

        #endregion

        #endregion


        #region Private Methods

        #region Get All Periods
        private async Task<PageResultDTO<PeriodDTOWithAction>> GetAllPeriodsSync()
        {
            var getAllPeriods = GetAllPeriodsAsync();

            return await getAllPeriods;

        }

        private async Task<PageResultDTO<PeriodDTOWithAction>> GetAllPeriodsAsync()
        {
            PageResultDTO<PeriodDTOWithAction> result;
            result = new PageResultDTO<PeriodDTOWithAction>();

            periodService.GetAllPeriods((res, exp) =>
            {
                if (exp == null)
                {
                    throw new Exception("Error In Get All Jobs From HPMS!");
                }
                result = res;
            }, 10, 1);

            return result;
        }


        #endregion

        #endregion

    }
}
