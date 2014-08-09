using System;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class RuleTrailDTOWithAction:IActionDTO
    {
        
        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }

        private string name;
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام قانون الزامی است")]
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        private DateTime effectiveDate;
        public DateTime EffectiveDate
        {
            get { return effectiveDate; }
            set { this.SetField(p => p.EffectiveDate, ref effectiveDate, value); }
        }

        private long policyId;
        public long PolicyId
        {
            get { return policyId; }
            set { this.SetField(p => p.PolicyId, ref policyId, value); }
        }

        private long ruleId;
        public long RuleId
        {
            get { return ruleId; }
            set { this.SetField(p => p.RuleId, ref ruleId, value); }
        }

        public List<int> ActionCodes { get; set; }
    }

}
