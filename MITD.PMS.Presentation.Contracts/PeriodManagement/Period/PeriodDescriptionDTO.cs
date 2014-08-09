using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PeriodDescriptionDTO
    {

        private long id;
        private string name;

        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "نام دوره الزامی می باشد")]
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

    }
}
