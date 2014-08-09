using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UnitDTO
    {
        private long id;
        private string name;
        private string dictionaryName;

        public long Id
        {
            get { return id; }
            set { this.SetField (p => p.Id, ref id, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "نام واحد را وارد نمایید")]
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "نام واحد در لغت نامه را وارد نمایید")]
        public string DictionaryName
        {
            get { return dictionaryName; }
            set { this.SetField(p => p.DictionaryName, ref dictionaryName, value); }
        }


    }

}
