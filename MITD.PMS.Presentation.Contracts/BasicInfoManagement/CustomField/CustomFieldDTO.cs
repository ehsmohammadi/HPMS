using System;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class CustomFieldDTO
    {
        private long id;
        private string name;
        private string dictionaryName;
        private long minValue;
        private long maxValue;
        private int entityId;
        private string typeId;

        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }


        [Required(AllowEmptyStrings = false, ErrorMessage = "نام الزامی می باشد")]
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        [Range(1,100,ErrorMessage = "موجودیت الزامی می باشد")]
        public int EntityId
        {
            get { return entityId; }
            set { this.SetField(p => p.EntityId, ref entityId, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "نوع فیلد الزامی می باشد")]
        public string TypeId
        {
            get { return typeId; }
            set { this.SetField(p => p.TypeId, ref typeId, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "نام در لغت نامه الزامی می باشد")]
        public string DictionaryName
        {
            get { return dictionaryName; }
            set { this.SetField(p => p.DictionaryName, ref dictionaryName, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "حد بالا الزامی می باشد")]
        public long MaxValue
        {
            get { return maxValue; }
            set { this.SetField(p => p.MaxValue, ref maxValue, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "حد پایین الزامی می باشد")]
        public long MinValue
        {
            get { return minValue; }
            set { this.SetField(p => p.MinValue, ref minValue, value); }
        }

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set { this.SetField(p => p.IsReadOnly, ref isReadOnly, value); }
        }
 
    }
}
