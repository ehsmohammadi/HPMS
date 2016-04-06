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
    public class JobIndexDataProvider:IJobIndexDataProvider
    {

        private PersonnelSoft2005Entities db;


        public List<GeneralJobIndexDto> GetGeneralIndexes()
        {
            db = new PersonnelSoft2005Entities();
            IList<long> ids = GetGeneralJobIndexIds();
            List<GeneralJobIndexDto> GeneralIndexesList = new List<GeneralJobIndexDto>();

            foreach (var item in ids)
            {
                try
                {
                    var TempGeneralIndex = (from c in db.PMS_GeneralIndex where c.ID == item select c).FirstOrDefault();

                    GeneralJobIndexDto GeneralIndex = new GeneralJobIndexDto();
                    GeneralIndex.IndexTitle = TempGeneralIndex.Title;
                    GeneralIndex.IndexTypeID = TempGeneralIndex.ID_IndexType;
                    GeneralIndex.Description = TempGeneralIndex.Description;
                    GeneralIndex.Coefficient = TempGeneralIndex.coefficient;

                    GeneralIndexesList.Add(GeneralIndex);

                }
                catch (Exception e)
                {                    
                    throw e;
                }
            }

            return GeneralIndexesList;

        }

        private IList<long> GetGeneralJobIndexIds()
        {
            db = new PersonnelSoft2005Entities();
            try
            {
                var ids = (from c in db.PMS_GeneralIndex where c.IsActive == true select c.ID).ToList();
                return ids;
            }
            catch (Exception e)
            {

                throw e;
            }          
            
        }



        
        public List<ExclusiveJobIndexDto> GetExclusiveJobIndexes()
        {
            db = new PersonnelSoft2005Entities();
            IJobDataProvider JobService = new JobDataProvider();

            var JobIds = JobService.GetJobIds();

            IList<long> ids = GetExclusiveJobIndexIds();
            List<ExclusiveJobIndexDto> ExclusiveIndexesList = new List<ExclusiveJobIndexDto>();

            foreach (var item in ids)
            {
                try
                {
                    var TempExclusiveIndex = (from c in db.PMS_JobIndex where c.ID == item && c.IsActive == true select c).FirstOrDefault();

                    ExclusiveJobIndexDto ExclusiveIndex = new ExclusiveJobIndexDto();
                    ExclusiveIndex.IndexTitle = TempExclusiveIndex.Title;
                    ExclusiveIndex.IndexTypeID = TempExclusiveIndex.ID_IndexType;
                    ExclusiveIndex.Description = TempExclusiveIndex.Discription;
                    ExclusiveIndex.JobID = TempExclusiveIndex.ID_Job;
                    ExclusiveIndex.Coefficient = TempExclusiveIndex.coefficient;



                    ExclusiveIndexesList.Add(ExclusiveIndex);

                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return ExclusiveIndexesList;
        }

        public List<JobIndexIdListItem> GetJobIndexListId()
        {
            db = new PersonnelSoft2005Entities();

            List<JobIndexIdListItem> Result = new List<JobIndexIdListItem>();

            Result = (from c in db.PMS_GeneralIndex
                      select new JobIndexIdListItem
                          {
                              //todo: Remove JobId From this damn query!
                              JobId = 0, //c.JobID,
                              TransferId = c.TransferId.Value
                          }).ToList();

            var tempJobIndexList = (from c in db.PMS_JobIndex
                                    select new JobIndexIdListItem
                                    {
                                        //todo: Remove JobId From this damn query!
                                        JobId = 0, //c.JobID,
                                        TransferId = c.TransferId.Value
                                    }).ToList();

            foreach (var item in tempJobIndexList)
            {
                Result.Add(item);
            }

            return Result;

            //return (from c in db.PMS_JobIndexList
            //        where c.ItemState == true
            //        select
            //            new JobIndexIdListItem
            //            {
            //                //todo: Remove JobId From this damn query!
            //                JobId = 0, //c.JobID,
            //                TransferId = c.TransferId.Value
            //            }).Distinct().ToList();
        }

        public JobIndexIntegrationDTO GetBy(JobIndexIdListItem id)
        {
            db = new PersonnelSoft2005Entities();
            return (from c in db.PMS_JobIndexList
                    where c.TransferId == id.TransferId //&& c.JobID == id.JobId
                    select new JobIndexIntegrationDTO
                           {
                               Id = c.IndexId,
                               TransferId = c.TransferId.Value,
                               Title = c.IndexTitle,
                               JobIndexId = c.IndexId,
                               //JobId = c.JobID,
                               IndexType = c.IndexTypeID,
                               Coefficient = c.coefficient
                           }).First();
        }

        private IList<long> GetExclusiveJobIndexIds()
        {
            db = new PersonnelSoft2005Entities();
            try
            {
                var ids = (from c in db.PMS_JobIndex where c.IsActive == true select c.ID).ToList();
                return ids;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
