using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PolicyDTO : PolicyDescriptionDTO
    {
        private string dictionaryName;
        [Required(AllowEmptyStrings = false, ErrorMessage = "فیلد نام در لغت نامه الزامی می باشد")]
        public string DictionaryName
        {
            get { return dictionaryName; }
            set { this.SetField(p => p.DictionaryName, ref dictionaryName, value); }
        }

    }

}
