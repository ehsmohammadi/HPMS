using MITD.PMS.Integration.Data.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.Data.Contract.DataProvider
{
    public interface IUnitDataProvider
    {

        UnitNodeIntegrationDTO GetRoot();

        List<int> GetChildIDs(int ParentID);

        //UnitNodeIntegrationDTO UnitDetail(long id);

        List<JobPositionIntegrationDTO> GetUnitJobPositions(int UnitID);

        UnitNodeIntegrationDTO GetUnitDetail(int id);

    }
}
