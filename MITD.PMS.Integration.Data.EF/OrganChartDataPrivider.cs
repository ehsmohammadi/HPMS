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
    public class OrganChartDataPrivider : IOrganChartDataProvider
    {

        private PersonnelSoft2005Entities DB = new PersonnelSoft2005Entities();


        #region GetUnitIds
        public IList<long> GetOrganChartIds()
        {

            List<long> Result = new List<long>();

            try
            {

                Result = (from C in DB.VW_OrganTree select Convert.ToInt64(C.ID)).ToList();

            }

            catch (Exception e)
            {

                throw e;

            }
            return Result;

        }

        #endregion


        #region GetUnitDetails

        public OrganChartNodeDto GetOrganChartDetails(long id)
        {
            {
                OrganChartNodeDto Result = new OrganChartNodeDto();
                try
                {

                    var Temp = (from c in DB.VW_OrganTree
                                where c.ID_F == id
                                select new OrganChartNodeDto()
                                {
                                    ID=c.ID,
                                    NodeName=c.NodeName,
                                    NodeTypeID=c.NodeType,
                                    PersonID=c.ID_F,
                                    PID=c.PID,
                                    TitleID=c.NCODE_TITLE,
                                    UnitID=c.Company_F
                                }).First();
                    Result = Temp;
                }
                catch (Exception e)
                {
                    throw e;
                }

                return Result;
            }
        }

        #endregion


        
        
        
        
        #region Get Unit Detail

        public UnitDto GetUnitDetail(long id)
        {

            UnitDto Result = new UnitDto();

            try
            {
                Result = (from c in DB.affiliateCompanies
                          where c.ID == id
                          select new UnitDto
                          {
                              UnitID = c.ID,
                              UnitName = c.Name
                          }).First();
            }
            catch (Exception e)
            {
                throw e;
            }

            return Result;
        }

        #endregion


        #region GetUnitIds

        public IList<long> GetUnitIds()
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






        #region GetJobTitleIds

        public IList<long> GetJobTitleIds()
        {

            List<long> Result = new List<long>();

            try
            {

                Result = (from C in DB.JOBSTitles  select Convert.ToInt64(C.ID)).ToList();

            }

            catch (Exception e)
            {

                throw e;

            }
            return Result;

        }

        #endregion


        #region GetJobTitleDetail

        public JobTitleDto GetJobTitleDetail(long id)
        {

            JobTitleDto Result = new JobTitleDto();

            try
            {
                Result = (from c in DB.JOBSTitles
                          where c.ID == id
                          select new JobTitleDto
                          {
                              JobTitleID = c.ID,
                              JobTitle = c.Name
                          }).First();
            }
            catch (Exception e)
            {
                throw e;
            }

            return Result;
        }

        #endregion





        #region GetNodeTypeIds

        public IList<long> GetNodeTypeIds()
        {

            List<long> Result = new List<long>();

            try
            {

                Result = (from C in DB.OrganTreeNodeTypes select Convert.ToInt64(C.ID)).ToList();

            }

            catch (Exception e)
            {

                throw e;

            }
            return Result;

        }

        #endregion


        #region GetNodeTypeDetail

        public NodeTypeDto GetNodeTypeDetail(long id)
        {

            NodeTypeDto Result = new NodeTypeDto();

            try
            {
                Result = (from c in DB.JOBSTitles
                          where c.ID == id
                          select new NodeTypeDto
                          {
                              ID = c.ID,
                              TypeTitle = c.Name
                          }).First();
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
