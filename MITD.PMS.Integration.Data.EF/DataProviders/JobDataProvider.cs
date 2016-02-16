using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.Data.EF.DBModel;

namespace MITD.PMS.Integration.Data.EF
{
    public class JobDataProvider: IJobDataProvider
    {

        private PersonnelSoft2005Entities db;


        IList<long> IJobDataProvider.GetJobIds()
        {
            db = new PersonnelSoft2005Entities();
            List<long> Result = new List<long>();

            try
            {
                //todo: Must change for get all jobs
                var parentId = 4334;
                var fullPath = (from c in db.VW_OrganTree where c.ID == parentId select c.FullPath).Single();
                var idList =
                    (from c in db.VW_OrganTree
                        where c.FullPath.StartsWith(fullPath) && c.NodeType == 1 && c.ID_PMS_JobTitle != null
                        select c.ID_PMS_JobTitle).Distinct().ToList();

                var temp = (from C in db.PMS_JobTitle select C.ID).ToList();
                foreach (var item in idList)
                {
                    Result.Add(item.Value);
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
            db = new PersonnelSoft2005Entities();
            JobIntegrationDto Result = new JobIntegrationDto();
            try
            {

                var Temp = (from c in db.PMS_JobTitle
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

        public List<JobIndexIntegrationDTO> GetJobIndecesByJobId(long id)
        {
            db = new PersonnelSoft2005Entities();

            return (from c in db.PMS_IndexList
                where c.ID_Job == id || c.ID_Job == null
                    select new JobIndexIntegrationDTO
                       {
                           TransferId = c.IndexId.Value,
                           Title = c.Title
                       }).ToList();
        }
    }
}
