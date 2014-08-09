using MITD.Presentation;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class RuleExcuteTimeDTO
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { this.SetField (p => p.Id, ref id, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

    }

}
