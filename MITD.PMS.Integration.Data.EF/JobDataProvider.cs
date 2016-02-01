using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        JobTitleDto IJobDataProvider.GetJobDetails(long id)
        {
            JobTitleDto Result = new JobTitleDto();
            try
            {

                var Temp = (from c in DB.PMS_JobTitle
                            where c.ID == id
                            select new JobTitleDto()
                            {
                                JobTitleID=c.ID, 
                                JobTitle = c.Title,
                                JobDecscription =c.Description                                
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
