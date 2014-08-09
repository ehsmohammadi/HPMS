using MITD.Presentation;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PolicyDescriptionDTO
    { 
        
        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField (p => p.Id, ref id, value); }
        }

        private string name;
        [Required(AllowEmptyStrings = false, ErrorMessage = "فیلد نام الزامی می باشد")]
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }



    }

}
