using System;
using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.Data.EF.DBModel;

namespace MITD.PMS.Integration.Data.EF
{
    public class UnitDataPrivider : IUnitDataProvider
    {

        private PersonnelSoft2005Entities db;


        #region GetRoot

        public UnitIntegrationDTO GetRoot()
        {
            db = new PersonnelSoft2005Entities();

            return (from c in db.VW_OrganTree
                    where c.ID == DataEFConfig.RootUnitId
                select new UnitIntegrationDTO
                       {
                           TransferId = c.TranferId.Value,
                           ID = c.ID,
                           UnitName = c.NodeName,
                       }).SingleOrDefault();

            return (from c in db.VW_OrganTree
                    where c.ID == c.PID
                    select new UnitIntegrationDTO
                    {
                        ID = c.ID,
                        UnitName = c.NodeName,
                    }).FirstOrDefault();
        }

        #endregion

        #region GetChildsIDs

        public List<int> GetChildIDs(int ParentID)
        {
            db = new PersonnelSoft2005Entities();
            try
            {
                var tempVar = (from c in db.VW_OrganTree
                               where c.PID == ParentID && c.ID != ParentID
                                   && c.NodeType != DataEFConfig.NodeType_Post // پست
                                 && c.NodeType != DataEFConfig.NodeType_Idle //بلا استفاده
                                 && c.NodeType != DataEFConfig.NodeType_Section // بخش
                               orderby c.ID
                               select c.ID).ToList();
                return (from c in db.VW_OrganTree
                        where c.PID == ParentID && c.ID != ParentID
                            && c.NodeType != DataEFConfig.NodeType_Post // پست
                          && c.NodeType != DataEFConfig.NodeType_Idle //بلا استفاده
                          && c.NodeType != DataEFConfig.NodeType_Section // بخش
                        orderby c.ID
                        select c.ID).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        #endregion

        #region GetUnitDetail

        public UnitIntegrationDTO GetUnitDetail(int id)
        {
            db = new PersonnelSoft2005Entities();
                try
                {

                    return (from c in db.VW_OrganTree
                                where c.ID == id
                                select new UnitIntegrationDTO()
                                {
                                    ID = c.ID,
                                    UnitName = c.NodeName,
                                    ParentID = c.PID,
                                    TransferId = c.TranferId.Value
                                }).FirstOrDefault();
                }
                catch (Exception e)
                {
                    throw e;
                }
            
        }

        #endregion


        #region GetCount

        public int GetCount()
        {
            db = new PersonnelSoft2005Entities();
            //todo: Predicate
            var RootFullPath= (from c in db.VW_OrganTree
                    where c.ID == DataEFConfig.RootUnitId
                    select c.FullPath
                    ).FirstOrDefault();
            try
            {
                return (from c in db.VW_OrganTree
                    where c.NodeType != DataEFConfig.NodeType_Post // پست
                          && c.NodeType != DataEFConfig.NodeType_Idle //بلا استفاده
                          && c.NodeType != DataEFConfig.NodeType_Section // بخش
                          && c.FullPath.StartsWith(RootFullPath)
                    select c.ID).Count();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        #endregion

        #region GetUnitJobPositions

        public List<JobPositionIntegrationDTO> GetUnitJobPositions(int UnitID)
        {
            db = new PersonnelSoft2005Entities();
            return (from c in db.VW_OrganTree
                    where c.NodeType == DataEFConfig.NodeType_Post && c.PID == UnitID
                    //todo: Add Not Used Job Position Condition
                    orderby c.ID
                    select new JobPositionIntegrationDTO
                    {
                        ID = c.ID,
                        JobPositionName = c.NodeName
                    }).ToList();

        }

        #endregion













        #region Comment

        //public UnitNodeIntegrationDTO GetUnitDetail(long id)
        //{

        //    UnitNodeIntegrationDTO Result = new UnitNodeIntegrationDTO();

        //    try
        //    {
        //        Result = (from c in DB.affiliateCompanies
        //                  where c.ID == id
        //                  select new UnitNodeIntegrationDTO
        //                  {
        //                      ID = c.ID,
        //                      UnitName = c.Name
        //                  }).FirstOrDefault();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }

        //    return Result;
        //}

        #endregion

        #region GetUnitIds

        public IList<long> GetIdList()
        {
            db = new PersonnelSoft2005Entities();
            List<long> Result = new List<long>();

            try
            {

                //Result = (from C in db.affiliateCompanies select Convert.ToInt64(C.ID)).ToList();

            }

            catch (Exception e)
            {

                throw e;

            }
            return Result;

        }

        #endregion




    }
}
