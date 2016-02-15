using System.Collections.Generic;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.Data.Contract.DTO;

namespace MITD.PMS.Integration.Data.Contract.DataProvider
{
    public interface IJobPositionDataProvider:IDataProvider
    {
        JobPositionIntegrationDTO GetRoot();
        int GetCount();
        IEnumerable<int> GetChildIDs(int id);
        JobPositionIntegrationDTO GetJobPositionDetail(int id);
    }
}
