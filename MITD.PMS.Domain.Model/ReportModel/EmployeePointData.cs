using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMSReport.Domain.Model
{
    public class JobPositionPointData
    {
        public List<SummaryJobPositionPoint> SummaryJobPositionPoints { get; set; }
        public Dictionary<JobIndex, List<JobIndexPoint>> JobIndexPoints { get; set; }
    }
    public class EmployeePointData
    {
        public List<SummaryEmployeePoint> SummaryEmployeePoints { get; set; }
        public Dictionary<JobPosition, JobPositionPointData> SummaryJobPositionPoints { get; set; }
    }

}
