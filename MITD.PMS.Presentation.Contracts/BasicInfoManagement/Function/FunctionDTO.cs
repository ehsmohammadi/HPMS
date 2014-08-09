using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class FunctionDTO
    {
        private string name;
        private long id;
        private string dictionaryName;
        private string content;
        private long policyId;

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

        [Required(AllowEmptyStrings = false, ErrorMessage = "نام تابع الزامی است")]
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "متن تابع الزامی است")]
        public string Content
        {
            get { return content; }
            set { this.SetField(p => p.Content, ref content, value); }
        }

    }

}
