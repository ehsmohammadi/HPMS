using MITD.Presentation;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobDTO : JobDescriptionDTO
    { 
        private string dictionaryName;  
         

        [Required(AllowEmptyStrings = false, ErrorMessage = "نام در لغت نامه الزامی است")]
        public string DictionaryName
        {
            get { return dictionaryName; }
            set { this.SetField(p => p.DictionaryName, ref dictionaryName, value); }
        }

    }

}
