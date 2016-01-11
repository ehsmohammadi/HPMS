using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PartyDTO
    {
        private string id;
        public string Id
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


        private Dictionary<int, bool> customActions = new Dictionary<int, bool>();
        public Dictionary<int, bool> CustomActions
        {
            get { return customActions; }
            set { this.SetField(p => p.CustomActions, ref customActions, value); }
        }

      


 
    }
}
