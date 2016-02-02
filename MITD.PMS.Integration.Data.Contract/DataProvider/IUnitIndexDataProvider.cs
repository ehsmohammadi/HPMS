using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Data.Contract.DTO;

namespace MITD.PMS.Integration.Data.Contract
{
    public interface IUnitIndexDataProvider
    {
        List<UnitIndexIntegrationDTO> GetUnitIndexList();
        List<long> GetUnitIndexListId();
        UnitIndexIntegrationDTO GetBy(long id);
    }
}
