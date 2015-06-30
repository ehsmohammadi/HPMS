using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Data.Contract;
using MITD.PMS.Integration.Data.EF.DBModel;


namespace MITD.PMS.Integration.Data.EF
{

    /*
    public class PersonnelDataPrivider : IPersonnelDataPrivider
    {
        private  PersonnelSoft2005Entities DB = new PersonnelSoft2005Entities();

        //Personnel Data
        private static List<ErrorListDto> PersonnelErrorIDs = new List<ErrorListDto>();


        //Organ Data
        private static List<ErrorListDto> OrganErrorIDs = new List<ErrorListDto>();


        #region GetJobTitleList  *****************

        public List<JobTitleDto> GetJobTitleList()
        {

            List<JobTitleDto> Result = new List<JobTitleDto>();

            try
            {
                Result = (from c in DB.JOBSTitles
                          select new JobTitleDto
                          {
                              JobTitle = c.Name,
                              JobTitleID = c.ID
                          }).ToList();
            }
            catch (Exception)
            {

            }
            return Result;

        }

        #endregion



        #region GetNodeTypeList *****************
        
        public List<NodeTypeDto> GetNodeTypeList()
        {

            List<NodeTypeDto> Result = new List<NodeTypeDto>();

            try
            {
                Result = (from c in DB.OrganTreeNodeTypes
                          select new NodeTypeDto
                          {
                              ID = c.ID,
                              TypeTitle = c.Name
                          }).ToList();
            }
            catch (Exception)
            {
                Result = null;                
            }
            return Result;

        }

        #endregion



        #region GetListIdentifier ************************

        public List<long> GetListIdentifier(RequestType R_Type)
        {

            if (R_Type == RequestType.Personnel)
            {
                return GetEmployeeListIdentifier();
            }
            else if (R_Type == RequestType.OrganTree)
            {
                return GetOrganListIdentifier();
            }
            else
                return null;

        }

        #endregion



        #region GetEmployeeListIdentifier  ****************************

        private List<long> GetEmployeeListIdentifier()
        {

            List<long> Result = new List<long>();

            try
            {

                Result = (from C in DB.VW_OrganTree where C.ID_F > 0 select Convert.ToInt64(C.ID_F)).ToList();
                
            }

            catch (Exception e)
            {
                
            }
            return Result;

        }

        #endregion



        #region GetPersonnelDetail **************************

        public List<PersonnelInfoDto> GetPersonnelDetail(List<long> EmployeeListIdentifier)
        {
            List<PersonnelInfoDto> Result = new List<PersonnelInfoDto>();
            try
            {
                PersonnelErrorIDs.Clear();
                foreach (var PersonID in EmployeeListIdentifier)
                {
                    try
                    {

                        var Temp = (from c in DB.VW_OrganTree
                                    where c.ID_F == PersonID
                                    select new PersonnelInfoDto()
                                    {
                                        Name = c.Name,
                                        Family = c.FamilyName,
                                        OrganID = c.ID,
                                        PersonnelCode = c.PersonnelCode.ToString()
                                    }).First();
                        Result.Add(Temp);

                    }
                    catch (Exception e)
                    {
                        ErrorListDto Err = new ErrorListDto();
                        Err.ItemID = PersonID;
                        Err.ErrorMessage = e.Message;
                        PersonnelErrorIDs.Add(Err);
                    }


                }
            }
            catch (Exception)
            {
                return null;
            }
            return Result;
        }

        #endregion
        


        #region GetEmployeeListIdentifier **********************

        private List<long> GetOrganListIdentifier()
        {

            List<long> Result = new List<long>();

            try
            {

                Result = (from C in DB.VW_OrganTree select Convert.ToInt64(C.ID)).ToList();

            }
            catch (Exception e)
            {

                return null;

            }

            return Result;

        }

        #endregion



        #region Get Companies List *************************

        public List<CompanyDto> GetGompanyList()
        {

            List<CompanyDto> Result = new List<CompanyDto>();

            try
            {
                Result = (from c in DB.affiliateCompanies
                          select new CompanyDto
                          {
                              CompanyID = c.ID,
                              CompanyName = c.Name
                          }).ToList();
            }
            catch (Exception e)
            {
                Result = null;
            }

            return Result;
        }

        #endregion



        #region GetOrganTreeListIdentifier ***************************

        public List<int> GetOrganTreeListIdentifier()
        {
            
            try
            {

                return (from c in DB.VW_OrganTree select c.ID).ToList();

            }
            catch (Exception)
            {

                return null;
            
            }

        }

        #endregion



        #region GetOrganTreeDetail ********************


        public List<OrganTreeNodeDto> GetOrganTreeDetail(List<long> OrganTreeListIdentifier)
        {

            List<OrganTreeNodeDto> Result = new List<OrganTreeNodeDto>();

            try
            {

                OrganErrorIDs.Clear();

                foreach (var item in OrganTreeListIdentifier)
                {

                    try
                    {

                        OrganTreeNodeDto Temp = (from c in DB.VW_OrganTree
                                                 where c.ID == item
                                                 select new OrganTreeNodeDto
                                                 {
                                                     ID=c.ID,
                                                     NodeName=c.NodeName,
                                                     NodeTypeID=c.NodeType,
                                                     PersonID=c.ID_F,
                                                     PID=c.PID,
                                                     TitleID=c.NCODE_TITLE,
                                                     CompanyID=c.Company_F
                                                 }).First();

                    }
                    catch (Exception e)
                    {
                        ErrorListDto Err = new ErrorListDto();
                        Err.ItemID = item;
                        Err.ErrorMessage = e.Message;

                        OrganErrorIDs.Add(Err);

                    }

                }

            }
            catch (Exception)
            {

                return null;
            
            }

            return Result;

        }


        #endregion



        #region Return Error Lists


        public List<ErrorListDto> GetErrorList(RequestType R_Type)
        {

            if (R_Type == RequestType.Personnel)
            {
                return PersonnelErrorIDs;
            }
            else if (R_Type == RequestType.OrganTree)
            {
                return OrganErrorIDs;
            }
            else
                return null;
        }

               
        #endregion
    }
     * */
}
