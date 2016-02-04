using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class UnitConverted : IDomainEvent<UnitConverted>
    {
        private readonly List<UnitInPeriodDTO> unitInperiodList;

        public List<UnitInPeriodDTO> UnitInperiodList
        {
            get { return unitInperiodList; }
        }

        public UnitConverted(List<UnitInPeriodDTO> unitInperiodList)
        {
            this.unitInperiodList = unitInperiodList;
        }

        public bool SameEventAs(UnitConverted other)
        {
            return true;
        }
    }
}
