using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class UnitConverted : IDomainEvent<UnitConverted>
    {
        private readonly List<UnitDTO> unitList;

        public List<UnitDTO> UnitList
        {
            get { return unitList; }
        }

        public UnitConverted(List<UnitDTO> unitList)
        {
            this.unitList = unitList;
        }

        public bool SameEventAs(UnitConverted other)
        {
            return true;
        }
    }
}
