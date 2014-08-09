using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class ReportDTO
    {
        private string path;
        public string Path
        {
            get { return path; }
            set { this.SetField(p => p.Path, ref path, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { this.SetField(p => p.Description, ref description, value); }
        }

        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set { this.SetField(p => p.TypeName, ref typeName, value); }
        }


    }
}
