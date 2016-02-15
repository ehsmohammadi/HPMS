using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.Data.EF.DBModel;

namespace MITD.PMS.Integration.Data.EF
{
    public class JobPositionDataProvider: IJobPositionDataProvider
    {

        private PersonnelSoft2005Entities db;

        public JobPositionIntegrationDTO GetRoot()
        {
            db = new PersonnelSoft2005Entities();

            var parentId = 4334;
            return (from c in db.VW_OrganTree
                    where c.PID == parentId && c.IsManager == true
                                      select new JobPositionIntegrationDTO
                                             {
                                                 ID = c.ID,
                                                 JobPositionName = c.NodeName,
                                                 TransferId = c.TranferId.Value,
                                                 JobIntegrationDto = (from d in db.PMS_JobTitle
                                                                      where d.ID == c.ID_PMS_JobTitle.Value
                                                                      select new JobIntegrationDto
                                                                             {
                                                                                 Title = d.Title,
                                                                                 TransferId = d.TransferId.Value
                                                                             }).FirstOrDefault(),
                                                 UnitIntegrationDTO = (from e in db.VW_OrganTree
                                                                       where e.ID == c.PID
                                                                       select new UnitIntegrationDTO
                                                                              {
                                                                                  TransferId = e.TranferId.Value,
                                                                                  UnitName = e.NodeName,
                                                                                  ID = e.ID
                                                                              }).FirstOrDefault()

                                             }).Single();


            var res = db.VW_OrganTree.Where(vm => vm.PID == parentId && vm.IsManager == true)
                .Select(s => new JobPositionIntegrationDTO
                                                                                                      {
                                                                                                         JobPositionName = s.NodeName,
                                                                                                         TransferId = s.TranferId.Value,
                                                                                                         JobIntegrationDto = db.PMS_JobTitle.Where(j=>j.ID==s.ID_PMS_JobTitle.Value).Select(x=>new JobIntegrationDto
                                                                                                                                                                                               {
                                                                                                                                                                                                   TransferId = x.TransferId.Value,
                                                                                                                                                                                                   Title = x.Title
                                                                                                                                                                                               }).FirstOrDefault()

                                                                                                      }).ToList();
            return new JobPositionIntegrationDTO();
        }

        public int GetCount()
        {
            db = new PersonnelSoft2005Entities();
            //todo: Predicate
            var RootFullPath = (from c in db.VW_OrganTree
                                where c.ID == 4334
                                select c.FullPath
                    ).FirstOrDefault();
            try
            {
                return (from c in db.VW_OrganTree
                        where c.NodeType == 1
                              && c.NodeType != 6
                              && c.FullPath.StartsWith(RootFullPath)
                        select c.ID).Count();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public IEnumerable<int> GetChildIDs(int id)
        {
            db = new PersonnelSoft2005Entities();
            List<int> brotherJobPositionsIds = new List<int>();
            List<int> childJobPositionsIds = new List<int>();
            try
            {
                Boolean isManager;
                try
                {
                    isManager = (from c in db.VW_OrganTree where c.ID == id select c.IsManager.Value).Single();
                }
                catch (Exception)
                {
                    isManager = false;
                }

                if (!isManager )
                {
                    return new List<int>();
                }
                var parentId = (from c in db.VW_OrganTree where c.ID == id select c.PID).Single();
                brotherJobPositionsIds = (from c in db.VW_OrganTree
                    where c.PID == parentId && c.ID != id
                          && c.NodeType == 1
                          && c.NodeType != 6
                    orderby c.ID
                    select c.ID).ToList();
                var parentFullPath = (from c in db.VW_OrganTree where c.ID == parentId select c.FullPath).Single();
                childJobPositionsIds =
                    (from c in db.VW_OrganTree where c.FullPath.StartsWith(parentFullPath) && c.PID!=parentId && c.IsManager == true select c.ID)
                        .ToList();

                var finalIds = brotherJobPositionsIds;
                foreach (var childJobPositionsId in childJobPositionsIds)
                {
                    finalIds.Add(childJobPositionsId);
                }
                return finalIds;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public JobPositionIntegrationDTO GetJobPositionDetail(int id)
        {
            db = new PersonnelSoft2005Entities();
            try
            {

                return (from c in db.VW_OrganTree
                        where c.ID == id
                        select new JobPositionIntegrationDTO
                        {
                            ID = c.ID,
                            JobPositionName = c.NodeName,
                            TransferId = c.TranferId.Value,
                            JobIntegrationDto = (from d in db.PMS_JobTitle
                                                 where d.ID == c.ID_PMS_JobTitle.Value
                                                 select new JobIntegrationDto
                                                 {
                                                     Title = d.Title,
                                                     TransferId = d.TransferId.Value
                                                 }).FirstOrDefault(),
                            UnitIntegrationDTO = (from e in db.VW_OrganTree
                                                  where e.ID == c.PID
                                                  select new UnitIntegrationDTO
                                                  {
                                                      TransferId = e.TranferId.Value,
                                                      UnitName = e.NodeName,
                                                      ID = e.ID
                                                  }).FirstOrDefault()
                        }).Single();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
