using System.Collections.Generic;
using MITD.PMS.Integration.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public interface IJobPositionConverter:IConverter
    {
        void ConvertJobPositions(Period period, List<UnitDTO> unitList, List<JobDTO> jobList);
    }
}
