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
            List<GeneralJobIndexDto> generalIndexesList = new List<GeneralJobIndexDto>();

            foreach (var item in ids)
            {
                try
                {
                    var tempGeneralIndex = (from c in db.PMS_GeneralIndex where c.ID == item select c).FirstOrDefault();

                    GeneralJobIndexDto generalIndex = new GeneralJobIndexDto();
                    generalIndex.IndexTitle = tempGeneralIndex.Title;
                    generalIndex.IndexTypeID = tempGeneralIndex.ID_IndexType;
                    generalIndex.Description = tempGeneralIndex.Description;
                    generalIndex.Coefficient = tempGeneralIndex.coefficient;

                    generalIndexesList.Add(generalIndex);

                }
                catch (Exception e)
                {                    
                    throw e;
                }
            }

            return generalIndexesList;

        }

        private IList<long> GetGeneralJobIndexIds()
        {
            db = new PersonnelSoft2005Entities();
            try
            {
                var ids = (from c in db.PMS_GeneralIndex where c.IsActive == true && c.ID_IndexType != DataEFConfig.IndexType_UnitIndex select c.ID).ToList();
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
                    var TempExclusiveIndex = (from c in db.PMS_JobIndexList where c.IndexId == item && c.IsActive == true && c.IndexTypeID != DataEFConfig.IndexType_UnitIndex select c).FirstOrDefault();

                    ExclusiveJobIndexDto ExclusiveIndex = new ExclusiveJobIndexDto();
                    ExclusiveIndex.IndexTitle = TempExclusiveIndex.IndexTitle;
                    ExclusiveIndex.IndexTypeID = TempExclusiveIndex.IndexTypeID;
                    ExclusiveIndex.Description = ""; //TempExclusiveIndex.Discription;
                    ExclusiveIndex.JobID = TempExclusiveIndex.JobID;
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

            //return (from c in db.PMS_JobIndexList
            //    select new JobIndexIdListItem
            //           {
            //               JobId = c.JobID,
            //               TransferId = c.TransferId.Value
            //           }).ToList();

            //Result = (from c in db.PMS_GeneralIndex
            //          where c.ID_IndexType != DataEFConfig.IndexType_UnitIndex
            //          select new JobIndexIdListItem
            //              {
            //                  //todo: Remove JobId From this damn query!
            //                  JobId = 0, //c.JobID,
            //                  TransferId = c.TransferId.Value
            //              }).ToList();

            var tempJobIndexList = (from c in db.PMS_JobIndexList
                                    select new JobIndexIdListItem
                                    {
                                        //todo: Remove JobId From this damn query!
                                        JobId = 0, //c.JobID, //c.JobID,
                                        TransferId = c.TransferId.Value
                                    }).ToList();
            Result = db.PMS_JobIndexList.GroupBy(c => c.TransferId).Select(r => new JobIndexIdListItem
                                                                                  {
                                                                                      JobId = 0, //c.JobID, //c.JobID,
                                                                                      TransferId =  r.Key.Value
                                                                                  }).ToList();

            //foreach (JobIndexIdListItem item in tempJobIndexList)
            //{
            //    Result.Add(item);
            //}

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
