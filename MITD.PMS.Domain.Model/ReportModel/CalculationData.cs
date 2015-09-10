using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.JobPositions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.RuleContracts;
using Employee = MITD.PMS.Domain.Model.Employees.Employee;
using Job = MITD.PMS.Domain.Model.Jobs.Job;
using JobIndex = MITD.PMS.Domain.Model.JobIndices.JobIndex;
using JobPosition = MITD.PMS.Domain.Model.JobPositions.JobPosition;
using UnitIndex = MITD.PMS.Domain.Model.UnitIndices.UnitIndex;

namespace MITD.PMSReport.Domain.Model
{
    public class InquiryData
    {

        public InquiryJobIndexPoint Point { get; set; }
        public SharedJobPosition JobPosition { get; set; }
    }
    public class JobPositionData
    {
        public Unit  Unit { get; set; }
        public Job Job { get; set; }
        public Dictionary<JobIndex, Dictionary<Employee, List<InquiryData>>> Indices { get; set; }
        public Dictionary<UnitIndex, Tuple<Employee, string>> UnitIndices { get; set; }
        public IReadOnlyList<EmployeeJobCustomFieldValue> CustomFields { get; set; }
        public int WorkTimePercent { get; set; }
        public int Weight { get; set; }
    }

   

    public class CalculationData
    {
        public Employee employee { get; set; }
        public Dictionary<JobPosition, JobPositionData> JobPositions { get; set; }
        public List<CalculationPoint> CalculationPoints { get; set; }
    }

}
