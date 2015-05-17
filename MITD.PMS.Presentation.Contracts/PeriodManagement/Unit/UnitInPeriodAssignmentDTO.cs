using System;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UnitInPeriodAssignmentDTO
    {
        private long periodId;
        private long unitId;

        [Range(1,Int64.MaxValue,ErrorMessage="انتخاب واحد الزامی است")]
        public long UnitId
        {
            get { return unitId; }
            set { this.SetField(p => p.UnitId, ref unitId, value); }
        }

        [Range(1, Int64.MaxValue, ErrorMessage = "انتخاب دوره الزامی است")]
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }
        private long? parentUnitId;
        public long? ParentUnitId
        {
            get { return parentUnitId; }
            set { this.SetField(p => p.ParentUnitId, ref parentUnitId, value); }
        }

    }
}
