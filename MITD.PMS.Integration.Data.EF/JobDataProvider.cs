using System;
using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.Data.EF.DBModel;

namespace MITD.PMS.Integration.Data.EF
{
    public class JobDataProvider: IJobDataProvider
    {

        private PersonnelSoft2005Entities DB = new PersonnelSoft2005Entities();


        IList<long> IJobDataProvider.GetJobIds()
        {
            List<long> Result = new List<long>();

            try
            {

                var temp = (from C in DB.PMS_JobTitle select C.ID).ToList();
                foreach (var item in temp)
                {
                    Result.Add(item);
                }

            }

            catch (Exception e)
            {

                throw e;

            }
            return Result;
        }

        JobIntegrationDto IJobDataProvider.GetJobDetails(long id)
        {
            JobIntegrationDto Result = new JobIntegrationDto();
            try
            {

                var Temp = (from c in DB.PMS_JobTitle
                    where c.ID == id
                    select new JobIntegrationDto()
                           {
                               Id = c.ID,
                               Title = c.Title,
                               Decscription = c.Description,
                               TransferId = c.TransferId.Value
                           }).FirstOrDefault();
                Result = Temp;
            }
            catch (Exception e)
            {
                throw e;
            }

            return Result;
        }
    }
}
