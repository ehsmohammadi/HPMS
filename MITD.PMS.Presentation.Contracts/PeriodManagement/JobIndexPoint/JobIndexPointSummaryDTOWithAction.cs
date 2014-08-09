
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobIndexPointSummaryDTOWithAction :IActionDTO
    {

        private string employeeNo;
        public string EmployeeNo
        {
            get { return employeeNo; }
            set { this.SetField(p => p.EmployeeNo, ref employeeNo, value); }
        }

        private string employeeName;
        public string EmployeeName
        {
            get { return employeeName; }
            set { this.SetField(p => p.EmployeeName, ref employeeName, value); }
        }

        private decimal totalScore;
        public decimal TotalScore
        {
            get { return totalScore; }
            set { this.SetField(p => p.TotalScore, ref totalScore, value); }
        }

        private int employeeRankInPeriod;
        public int EmployeeRankInPeriod
        {
            get { return employeeRankInPeriod; }
            set { this.SetField(p => p.EmployeeRankInPeriod, ref employeeRankInPeriod, value); }
        }

        private List<JobPositionValueDTO> jobPositionValues;
        public List<JobPositionValueDTO> JobPositionValues
        {
            get { return jobPositionValues; }
            set { this.SetField(p => p.JobPositionValues, ref jobPositionValues, value); }
        }


        public List<int> ActionCodes { get; set; }
    }
}
