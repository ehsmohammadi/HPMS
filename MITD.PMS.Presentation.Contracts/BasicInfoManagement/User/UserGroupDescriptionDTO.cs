using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UserGroupDescriptionDTO 
    {
        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }


        private string partyName;
        public string PartyName
        {
            get { return partyName; }
            set { this.SetField(p => p.PartyName, ref partyName, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { this.SetField(p => p.Description, ref description, value); }
        }
        
       

 
    }
}
