using System;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UnitIndexInPeriodDTO : AbstractUnitIndexInPeriodDTO
    {

        private bool isInquireable;
        public bool IsInquireable
        {
            get { return isInquireable; }
            set
            {
                this.SetField(p => p.IsInquireable, ref isInquireable, value);
            }
        }

        private long unitIndexId;
        [Range(1, Int64.MaxValue, ErrorMessage = "انتخاب شاخص الزامی است")]
        public long UnitIndexId
        {
            get { return unitIndexId; }
            set
            {
                this.SetField(p => p.UnitIndexId, ref unitIndexId, value);
            }
        }

        private List<AbstractCustomFieldDescriptionDTO> customFields = new List<AbstractCustomFieldDescriptionDTO>();
        public List<AbstractCustomFieldDescriptionDTO> CustomFields
        {
            get { return customFields; }
            set { this.SetField(p => p.CustomFields, ref customFields, value); }
        }

        private long calculationLevel;
        [Range(1, Int64.MaxValue, ErrorMessage = "انتخاب مرحله محاسبه الزامی است")]
        public long CalculationLevel
        {
            get { return calculationLevel; }
            set { this.SetField(p => p.CalculationLevel, ref calculationLevel, value); }
        }

        private int calculationOrder;
        [Range(1, Int64.MaxValue, ErrorMessage = "الويت  محاسبه در هر مرحله الزامی است")]
        public int CalculationOrder
        {
            get { return calculationOrder; }
            set { this.SetField(p => p.CalculationOrder, ref calculationOrder, value); }
        }
    }
}
