using System.Collections.Generic;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.Data.Contract.DTO;

namespace MITD.PMS.Integration.Data.Contract.DataProvider
{
    public interface IJobDataProvider : IDataProvider

    {

        IList<long> GetJobIds();

        JobIntegrationDto GetJobDetails(long id);

    }
}
