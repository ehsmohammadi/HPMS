using MITD.Presentation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobPositionValueDTO
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        private List<PointValueDTO> jobPoints;
        public List<PointValueDTO> JobPoints
        {
            get { return jobPoints; }
            set { this.SetField(p => p.JobPoints, ref jobPoints, value); }
        }

        private List<JobIndexResultValueDTO> jobIdexPoints = new List<JobIndexResultValueDTO>();
        public List<JobIndexResultValueDTO> JobIdexPoints
        {
            get { return jobIdexPoints; }
            set { this.SetField(p => p.JobIdexPoints, ref jobIdexPoints, value); }
        }
        
    }

}
