using System.Collections.Generic;
using MITD.PMS.Integration.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public interface IEmployeeConverter:IConverter
    {
        void ConvertEmployees(Period period, List<JobPositionDTO> jobPositionList);
    }
}
