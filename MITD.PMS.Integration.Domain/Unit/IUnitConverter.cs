using System.Collections.Generic;
using MITD.PMS.Integration.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public interface IUnitConverter:IConverter
    {
        void ConvertUnits(Period PeriodId, List<UnitIndexInPeriodDTO> unitIndexInperiodList);
    }
}
