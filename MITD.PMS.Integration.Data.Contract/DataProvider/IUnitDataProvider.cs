using System.Collections.Generic;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.Data.Contract.DTO;

namespace MITD.PMS.Integration.Data.Contract.DataProvider
{
    public interface IUnitDataProvider : IDataProvider
    {

        UnitIntegrationDTO GetRoot();

        List<int> GetChildIDs(int ParentID);

        //UnitNodeIntegrationDTO UnitDetail(long id);

        List<JobPositionIntegrationDTO> GetUnitJobPositions(int UnitID);

        UnitIntegrationDTO GetUnitDetail(int id);

    }
}
