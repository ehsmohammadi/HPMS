using System;
using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.Data.EF.DBModel;

namespace MITD.PMS.Integration.Data.EF
{
    public class EmployeeDataProvider : IEmployeeDataProvider
    {
        private PersonnelSoft2005Entities db;

        #region GetEmployeeListIdentifier

        public IList<long> GetIds()
        {
            db = new PersonnelSoft2005Entities();

            var idList = (from c in db.VW_OrganTree where c.ID_F != null && c.Company_F == 25 select c.ID_F).ToList();

            return idList.Select(c => Convert.ToInt64(c.Value)).ToList();


        }

        #endregion


        #region GetEmployeeDetails


        public EmployeeIntegrationDTO GetEmployeeDetails(long id)
        {
            db = new PersonnelSoft2005Entities();
            EmployeeIntegrationDTO result;
            var temp = (from c in db.VW_OrganTree
                where c.ID_F == id
                select new EmployeeIntegrationDTO()
                       {
                           Name = c.Name,
                           Family = c.FamilyName,
                           JobPositionTransferId = c.TranferId.Value,
                           PersonnelCode = c.PersonnelCode.ToString()
                       }).FirstOrDefault();
            result = temp;

            return result;
        }


        #endregion

                
    }
}
