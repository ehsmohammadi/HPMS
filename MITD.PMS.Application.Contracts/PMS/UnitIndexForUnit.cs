using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.UnitIndices;
using System;
using MITD.PMS.Domain.Model.Units;

namespace MITD.PMS.Application.Contracts
{
    public class UnitIndexForUnit:IUnitUnitIndex
    {
        public UnitIndexForUnit(AbstractUnitIndexId jobIndexId, bool showforTopLevel, bool showforSameLevel, bool showforLowLevel)
        {
            UnitIndexId = jobIndexId;
            ShowforLowLevel = showforLowLevel;
            ShowforTopLevel = showforTopLevel;
            ShowforSameLevel = showforSameLevel;

        }
        public AbstractUnitIndexId UnitIndexId { get; private set; }
        public bool ShowforTopLevel { get; private set; }
        public bool ShowforSameLevel { get; private set; }
        public bool ShowforLowLevel { get; private set; }
    }
}
