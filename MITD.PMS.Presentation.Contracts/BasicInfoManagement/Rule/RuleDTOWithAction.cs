using System.ComponentModel.DataAnnotations;
using MITD.Presentation;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class RuleDTOWithAction:IActionDTO
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

        public List<int> ActionCodes { get; set; }
    }

}
