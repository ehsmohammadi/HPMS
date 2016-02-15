using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Data.Contract;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.Data.EF.DBModel;

namespace MITD.PMS.Integration.Data.EF
{
    public class UnitIndexDataProvider:IUnitIndexDataProvider
    {
        private PersonnelSoft2005Entities db = new PersonnelSoft2005Entities();
        public List<UnitIndexIntegrationDTO> GetUnitIndexList()
        {
            throw new NotImplementedException();
        }

        public List<long> GetUnitIndexListId()
        {
            return (from c in db.PMS_GeneralIndex
                where c.ID_IndexType == 1
                select c.ID).ToList();
        }

        public UnitIndexIntegrationDTO GetBy(long id)
        {
            return (from c in db.PMS_GeneralIndex
                where c.ID == id
                select new UnitIndexIntegrationDTO
                {
                    Title = c.Title
                    ,
                    Description = c.Description
                    ,
                    ID = c.ID
                    //TranferID
                }).Single();
        }
    }
}
