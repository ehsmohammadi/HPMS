using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class CustomFieldValueDTO
    { 
        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }
       
        private string fieldvalue;
        public string Value
        {
            get { return fieldvalue; }
            set { this.SetField(p => p.Value, ref fieldvalue, value); }
        }

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set { this.SetField(p => p.IsReadOnly, ref isReadOnly, value); }
        }
    }
}
