using System.Collections.Generic;
using MITD.PMS.Integration.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public interface IJobConverter:IConverter
    {
        void ConvertJobs(Period periodId, List<JobIndexInPeriodDTO> jobIndexInperiodList, List<JobIndexDTO> jobIndexListParam);
    }
}
