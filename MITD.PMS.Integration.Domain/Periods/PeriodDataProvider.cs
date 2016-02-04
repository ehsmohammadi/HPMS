using System;
using System.Collections.Generic;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Integration.Data.Contract.DTO;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.Domain
{
    public class PeriodDataProvider
    {
        private readonly IPeriodServiceWrapper periodService;


        public PeriodDataProvider(IPeriodServiceWrapper periodService)
        {
            this.periodService = periodService;
        }

        public Period GetPeriodInformationMain(long id)
        {
            var Result = new Period();
            var TemResult=new PeriodDTO();
            periodService.GetPeriod((res, exp) =>
            {
                if (exp != null)
                {
                    throw new Exception("Error In Get Period Info From HPMS!");
                }
                TemResult = res;
            }, id);

            Result.Id=TemResult.Id;
            Result.State = new PeriodState { Name = TemResult.StateName }; ;
            Result.Name = TemResult.Name;

            return Result;
        }


        public Period GetPeriodInformation(long id)
        {
            var Result = new Period();
            //Result.ID = id;
            //Result.StateName = id.ToString();
            //Result.PeriodName = "Period " + id.ToString();

            var PeriodList = GetPeriodsList();

            foreach (var item in PeriodList)
            {
                if (item.Id==id)
                {
                    return item;
                }
            }

            return Result;
        }


        #region Get Periods

        //private async Task<List<Period>> GetPeriodsSync()
        //{
        //    Task<List<Period>> GetPeriods = GetPeriodsAsync();

        //    List<Period> PeriodList = await GetPeriods;
        //    return PeriodList;
        //}

        //private async Task<List<Period>> GetPeriodsAsync()
        //{
        //    PageResultDTO<PeriodDTOWithAction> PeriodList = new PageResultDTO<PeriodDTOWithAction>();

        //    PeriodList = GetAllPeriods();
        //    //System.Threading.Thread.Sleep(300);
        //    List<Period> Result = new List<Period>();

        //    if (PeriodList.Result != null)
        //    {
        //        foreach (var item in PeriodList.Result)
        //        {
        //            Period tempPeriod = new Period();
        //            tempPeriod.ID = item.Id;
        //            tempPeriod.PeriodName = item.Name;
        //            tempPeriod.StateName = item.StateName;
        //            Result.Add(tempPeriod);
        //        }
        //    }

        //    return Result;
        //}


        public List<Period> GetPeriodsList()
        {

            //return GetPeriodsSync().Result;
            PageResultDTO<PeriodDTOWithAction> PeriodList = new PageResultDTO<PeriodDTOWithAction>();

            PeriodList = GetAllPeriods();
            //System.Threading.Thread.Sleep(300);
            List<Period> Result = new List<Period>();

            if (PeriodList.Result != null)
            {
                foreach (var item in PeriodList.Result)
                {
                    Period tempPeriod = new Period();
                    tempPeriod.Id = item.Id;
                    tempPeriod.Name = item.Name;
                    tempPeriod.State = new PeriodState { Name = item.StateName };
                    Result.Add(tempPeriod);
                }
            }
            else
                throw new Exception("jlkjlkjlkjlkjl");

            return Result;

        }

        #endregion


        #region Get All Periods
        private async Task<PageResultDTO<PeriodDTOWithAction>> GetAllPeriodsSync()
        {
            Task<PageResultDTO<PeriodDTOWithAction>> GetAllPeriods = GetAllPeriodsAsync();

            return await GetAllPeriods;
            //return AllPeriodList;
        }

        private async Task<PageResultDTO<PeriodDTOWithAction>> GetAllPeriodsAsync()
        {
            PageResultDTO<PeriodDTOWithAction> Result = new PageResultDTO<PeriodDTOWithAction>();

            periodService.GetAllPeriods((res, exp) =>
            {
                if (exp == null)
                {
                    throw new Exception("Error In Get All Jobs From HPMS!");
                }
                Result = res;
            }, 10, 1);
            //System.Threading.Thread.Sleep(2000);
            return Result;
        }

        public PageResultDTO<PeriodDTOWithAction> GetAllPeriods()
        {
            return GetAllPeriodsSync().Result;
            //PageResultDTO<PeriodDTOWithAction> Result = new PageResultDTO<PeriodDTOWithAction>();

            //periodService.GetAllPeriods((res, exp) =>
            //    {
            //        if (exp != null)
            //        {
            //            throw new Exception("Error In Get All Jobs From HPMS!");
            //        }
            //        Result = res;
            //    }, 10, 1);
            //System.Threading.Thread.Sleep(10000);
            //return Result;
        }

        #endregion

    }
}
