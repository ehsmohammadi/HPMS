using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class RuleDTO
    {
        private long id;
        private long policyId;
        private string name;



        public long Id
        {
            get { return id; }
            set { this.SetField (p => p.Id, ref id, value); }
        }

        public long PolicyId
        {
            get { return policyId; }
            set { this.SetField(p => p.PolicyId, ref policyId, value); }
        }

        private int excuteOrder;
        [Range(1, Int32.MaxValue, ErrorMessage = "ترتیب اجرا باید عدد مثبت صحیح باشد")]
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

        [Required(AllowEmptyStrings = false, ErrorMessage = "نام قانون الزامی است")]
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

 
    }

}
