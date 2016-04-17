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


        #region GetRoot
        public JobPositionIntegrationDTO GetRoot()
        {
            db = new PersonnelSoft2005Entities();

            var parentId = DataEFConfig.RootUnitId;
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

        #endregion


        #region GetCount
        public int GetCount()
        {
            db = new PersonnelSoft2005Entities();
            //todo: Predicate
            var RootFullPath = (from c in db.VW_OrganTree
                                where c.ID == DataEFConfig.RootUnitId
                                select c.FullPath
                    ).FirstOrDefault();
            try
            {
                return (from c in db.VW_OrganTree
                        where c.NodeType == 1
                              && c.NodeType != 6
                              && (c.ID_F != null || c.IsManager == true)
                              && c.FullPath.StartsWith(RootFullPath)
                        select c.ID).Count();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        #endregion


        #region GetChildIds
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

                if (!isManager)
                {
                    return new List<int>();
                }


                var parentId = (from c in db.VW_OrganTree where c.ID == id select c.PID).Single();

                brotherJobPositionsIds = (from c in db.VW_OrganTree
                    where c.PID == parentId && c.ID != id
                          && c.NodeType == 1
                          && c.ID_F != null
                    orderby c.ID
                    select c.ID).ToList();

                var subSectionIds = (from c in db.VW_OrganTree 
                                    where 
                                        c.PID == parentId 
                                        && c.NodeType != 6   //بلا استفاده
                                        && c.NodeType == 2   // بخش
                                 select c.ID).ToList();

                var subSectionJobPositionIds = (from c in db.VW_OrganTree
                                                where subSectionIds.Contains(c.PID.Value)
                                                      && c.NodeType == 1
                                                      && c.ID_F != null
                                                orderby c.ID
                                                select c.ID).ToList();




                var parentFullPath = (from c in db.VW_OrganTree where c.ID == parentId select c.FullPath).Single();
                childJobPositionsIds =
                    (from c in db.VW_OrganTree
                     where
                         c.FullPath.StartsWith(parentFullPath) && c.PID != parentId && c.IsManager == true
                         // && c.ID_F != null
                     select c.ID)
                        .ToList();
                var subManagerIds = new List<int>();

                foreach (var childJobPositionsId in childJobPositionsIds)
                {

                    var parentNodeId = (from c in db.VW_OrganTree where c.ID == childJobPositionsId select c.PID)
                            .Single();
                    var
                    IdSubManager =
                        (from c in db.VW_OrganTree where c.ID == parentNodeId && c.PID == parentId select c.ID)
                            .Any();
                    if (IdSubManager)
                    {
                        subManagerIds.Add(childJobPositionsId);
                    }

                }




                    //(from c in db.VW_OrganTree
                    //    where
                    //        c.FullPath.StartsWith(parentFullPath) && c.PID != parentId && c.IsManager == true &&
                    //        c.ID_F != null
                    //    select c.ID)
                    //    .ToList();

                var finalIds = brotherJobPositionsIds;
                foreach (var childJobPositionsId in subManagerIds)
                {
                    finalIds.Add(childJobPositionsId);
                }


                foreach (var subSectionJobPositionId in subSectionJobPositionIds)
                {
                    finalIds.Add(subSectionJobPositionId);
                }
                return finalIds;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        #endregion


        #region GetJobPositionDetail
        public JobPositionIntegrationDTO GetJobPositionDetail(int id)
        {
            db = new PersonnelSoft2005Entities();
            try
            {
                var jobPosition=db.VW_OrganTree.Single(c=>c.ID==id);
                var job=(from c in db.PMS_JobTitle where c.ID==jobPosition.ID_PMS_JobTitle.Value select c).Single();

                var interMediateUnit=(from c in db.VW_OrganTree where c.ID==jobPosition.PID.Value select c).Single();
                while (interMediateUnit.NodeType==2)
                {
                    interMediateUnit=(from c in db.VW_OrganTree where c.ID==interMediateUnit.PID.Value select c).Single();
                }

                var jobPositionIntegrationDTO=new JobPositionIntegrationDTO{
                    ID=jobPosition.ID,
                    JobPositionName=jobPosition.NodeName,
                    TransferId=jobPosition.TranferId.Value,
                    JobIntegrationDto=new JobIntegrationDto{
                        Title=job.Title,
                        TransferId=job.TransferId.Value
                    },
                    UnitIntegrationDTO=new UnitIntegrationDTO{
                        TransferId=interMediateUnit.TranferId.Value,
                        ID=interMediateUnit.ID,
                        UnitName=interMediateUnit.NodeName
                    }
                };

                return jobPositionIntegrationDTO;

                //return (from c in db.VW_OrganTree
                //        where c.ID == id
                //        select new JobPositionIntegrationDTO
                //        {
                //            ID = c.ID,
                //            JobPositionName = c.NodeName,
                //            TransferId = c.TranferId.Value,
                //            JobIntegrationDto = (from d in db.PMS_JobTitle
                //                                 where d.ID == c.ID_PMS_JobTitle.Value
                //                                 select new JobIntegrationDto
                //                                 {
                //                                     Title = d.Title,
                //                                     TransferId = d.TransferId.Value
                //                                 }).FirstOrDefault(),
                //            UnitIntegrationDTO = (var temp=(from e in db.VW_OrganTree
                //                                  where e.ID == c.PID
                //                                  ).FirstOrDefault()
                                                  
                //                                  select new UnitIntegrationDTO
                //                                  {
                //                                      TransferId = e.TranferId.Value,
                //                                      UnitName = e.NodeName,
                //                                      ID = e.ID
                //                                  }).FirstOrDefault()
                //        }).Single();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion 
    }
}
