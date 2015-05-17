using MITD.Presentation;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobDescriptionDTO
    { 
        private string name;
        private long id;
       

        public long Id
        {
            get { return id; }
            set { this.SetField (p => p.Id, ref id, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "نام شغل الزامی است")]
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

       

    }

}
