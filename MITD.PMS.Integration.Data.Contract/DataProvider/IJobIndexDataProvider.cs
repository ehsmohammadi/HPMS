using System;
using System.Collections.Generic;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.Data.Contract.DTO;

namespace MITD.PMS.Integration.Data.Contract.DataProvider
{
    public interface IJobIndexDataProvider:IDataProvider
    {

        List<GeneralJobIndexDto> GetGeneralIndexes();

        List<ExclusiveJobIndexDto> GetExclusiveJobIndexes();
        List<Nullable<Guid>> GetJobIndexListId();
        JobIndexIntegrationDTO GetBy(Guid? id);
    }
}
