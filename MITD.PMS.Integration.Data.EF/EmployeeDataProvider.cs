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
    public class EmployeeDataProvider : IEmployeeDataProvider
    {
        private PersonnelSoft2005Entities DB = new PersonnelSoft2005Entities();

        #region GetEmployeeListIdentifier

        public IList<long> GetEmployeeIds()
        {

            List<long> Result = new List<long>();

            try
            {

                var temp = (from C in DB.VW_OrganTree where C.ID_F > 0 select C.ID_F).ToList();
                foreach (var item in temp)
                {
                    Result.Add(Convert.ToInt64(item));
                }

            }

            catch (Exception e)
            {

                throw e;

            }
            return Result;

        }

        #endregion


        #region GetEmployeeDetails


        public EmployeeDTO GetEmployeeDetails(long id)
        {
            EmployeeDTO Result = new EmployeeDTO();
            try
            {

                var Temp = (from c in DB.VW_OrganTree
                            where c.ID_F == id
                            select new EmployeeDTO()
                            {
                                Name = c.Name,
                                Family = c.FamilyName,
                                OrganID = c.ID,
                                PersonnelCode = c.PersonnelCode.ToString()
                            }).FirstOrDefault();
                Result = Temp;
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
