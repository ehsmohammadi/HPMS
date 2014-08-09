using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class CustomFieldDTOWithActions : CustomFieldDTO,IActionDTO 
    { 
        private string entityName;
        private string typeName;


        public string EntityName
        {
            get { return entityName; }
            set { this.SetField(p => p.EntityName, ref entityName, value); }
        }

        public string TypeName
        {
            get { return typeName; }
            set { this.SetField(p => p.TypeName, ref typeName, value); }
        }

       
       public List<int> ActionCodes
        {
            get;
            set;
        }
 
    }
}
