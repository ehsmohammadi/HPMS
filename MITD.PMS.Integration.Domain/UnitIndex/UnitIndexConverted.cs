using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class UnitIndexConverted : IDomainEvent<UnitIndexConverted>
    {
        private readonly List<UnitIndexInPeriodDTO> unitIndexInperiodList;

        public List<UnitIndexInPeriodDTO> UnitIndexInperiodList
        {
            get { return unitIndexInperiodList; }
        }

        public UnitIndexConverted(List<UnitIndexInPeriodDTO> unitIndexInperiodList)
        {
            this.unitIndexInperiodList = unitIndexInperiodList;
        }

        public bool SameEventAs(UnitIndexConverted other)
        {
            return true;
        }
    }
}
