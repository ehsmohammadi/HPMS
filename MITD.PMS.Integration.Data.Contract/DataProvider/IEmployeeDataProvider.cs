using System.Collections.Generic;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.Data.Contract.DTO;

namespace MITD.PMS.Integration.Data.Contract.DataProvider
{
    public interface IEmployeeDataProvider:IDataProvider
    {

        IList<long> GetIds();
        EmployeeIntegrationDTO GetEmployeeDetails(long id);


    }
}
