using System.Collections.Generic;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.Data.Contract.DTO;

namespace MITD.PMS.Integration.Data.Contract
{
    public interface IUnitIndexDataProvider:IDataProvider
    {
        List<UnitIndexIntegrationDTO> GetUnitIndexList();
        List<long> GetUnitIndexListId();
        UnitIndexIntegrationDTO GetBy(long id);
    }
}
