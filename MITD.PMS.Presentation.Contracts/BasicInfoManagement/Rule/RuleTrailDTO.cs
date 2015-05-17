using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class RuleTrailDTO
    {

        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField (p => p.Id, ref id, value); }
        }

        private long ruleId;
        //todo: i change ref id to ruleId if any thing happen in this project , i must undo all my change in this file 
        public long RuleId
        {
            get { return id; }
            set { this.SetField(p => p.RuleId, ref ruleId, value); }
        }


        private int excuteOrder;
        public int ExcuteOrder
        {
            get { return excuteOrder; }
            set { this.SetField(p => p.ExcuteOrder, ref excuteOrder, value); }
        }

        private int excuteTime;
        [Required(AllowEmptyStrings = false, ErrorMessage = "زمان اجرا الزامی است")]
        public int ExcuteTime
        {
            get { return excuteTime; }
            set { this.SetField(p => p.ExcuteTime, ref excuteTime, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        private string ruleText;
        [Required(AllowEmptyStrings = false, ErrorMessage = "متن قانون الزامی است")]
        public string RuleText
        {
            get { return ruleText; }
            set { this.SetField(p => p.RuleText, ref ruleText, value); }
        }

        private DateTime effectiveDate;
        public DateTime EffectiveDate
        {
            get { return effectiveDate; }
            set { this.SetField(p => p.EffectiveDate, ref effectiveDate, value); }
        }


 
    }

}
