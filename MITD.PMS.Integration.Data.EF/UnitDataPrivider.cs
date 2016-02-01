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
    public class UnitDataPrivider : IUnitDataProvider
    {

        private PersonnelSoft2005Entities DB = new PersonnelSoft2005Entities();


        #region GetRoot

        public UnitNodeIntegrationDTO GetRoot()
        {
            return (from c in DB.VW_OrganTree
                    where c.ID == c.PID
                    select new UnitNodeIntegrationDTO
                    {
                        ID = c.ID,
                        UnitName = c.NodeName,
                    }).FirstOrDefault();
        }

        #endregion

        #region GetChildsIDs

        public List<int> GetChildIDs(int ParentID)
        {
            try
            {
                return (from c in DB.VW_OrganTree
                        where c.PID == ParentID && c.ID != ParentID
                            && c.NodeType != 1
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

        public UnitNodeIntegrationDTO GetUnitDetail(int id)
        {
            {
                UnitNodeIntegrationDTO Result = new UnitNodeIntegrationDTO();
                try
                {

                    return (from c in DB.VW_OrganTree
                                where c.ID == id
                                select new UnitNodeIntegrationDTO()
                                {
                                    ID = c.ID,
                                    UnitName = c.NodeName,
                                    ParentID = c.PID
                                }).FirstOrDefault();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        #endregion

        #region GetUnitJobPositions

        public List<JobPositionIntegrationDTO> GetUnitJobPositions(int UnitID)
        {
            return (from c in DB.VW_OrganTree
                    where c.NodeType == 1 && c.PID == UnitID
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

            List<long> Result = new List<long>();

            try
            {

                Result = (from C in DB.affiliateCompanies select Convert.ToInt64(C.ID)).ToList();

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
