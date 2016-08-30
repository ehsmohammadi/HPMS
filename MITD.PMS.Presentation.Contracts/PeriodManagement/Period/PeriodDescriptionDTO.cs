using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PeriodDescriptionDTO
    {

        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }

        private string name;
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام دوره الزامی می باشد")]
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }



    }
}
