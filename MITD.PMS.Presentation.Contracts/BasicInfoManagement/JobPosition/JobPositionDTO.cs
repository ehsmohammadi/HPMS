using System;
using MITD.Presentation;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{ 
    public partial class JobPositionDTO
    {
        private long id;
        private string name;
        private string dictionaryName;

        public long Id
        {
            get { return id; }
            set { this.SetField (p => p.Id, ref id, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "نام پست سازمانی را وارد نمایید")]
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "نام پست سازمانی در لغت نامه را وارد نمایید")]
        public string DictionaryName
        {
            get { return dictionaryName; }
            set { this.SetField(p => p.DictionaryName, ref dictionaryName, value); }
        }

        private Guid transferId;
        public Guid TransferId
        {
            get { return transferId; }
            set { this.SetField(p => p.TransferId, ref transferId, value); }
        }
    }

}
