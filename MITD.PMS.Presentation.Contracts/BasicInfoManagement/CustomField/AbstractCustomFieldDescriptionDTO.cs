using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class AbstractCustomFieldDescriptionDTO
    { 
        private long id;
        private string name;
     
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }


       
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
