
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobIndexResultValueDTO 
    {

        private string jobIndexName;
        public string JobIndexName
        {
            get { return jobIndexName; }
            set { this.SetField(p => p.JobIndexName, ref jobIndexName, value); }
        }

        private List<PointValueDTO> indexPoints= new List<PointValueDTO>();
        public List<PointValueDTO> IndexPoints
        {
            get { return indexPoints; }
            set { this.SetField(p => p.IndexPoints, ref indexPoints, value); }
        }

    }
}
