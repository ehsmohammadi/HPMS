using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;
namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeResultDTO 
    {
        private string periodName;
        public string PeriodName
        {
            get { return periodName; }
            set { this.SetField(p => p.PeriodName, ref periodName, value); }

        }

        private string periodTimeLine;
        public string PeriodTimeLine
        {
            get { return periodTimeLine; }
            set { this.SetField(p => p.PeriodTimeLine, ref periodTimeLine, value); }

        }

        private  string employeeFullName;
        public string EmployeeFullName
        {
            get { return employeeFullName; }
            set { this.SetField(p => p.EmployeeFullName, ref employeeFullName, value); }

        }

        private string employeeUnitRootName;
        public string EmployeeUnitRootName
        {
            get { return employeeUnitRootName; }
            set { this.SetField(p => p.EmployeeUnitRootName, ref employeeUnitRootName, value); }

        }

        private string employeeNo;
        public string EmployeeNo
        {
            get { return employeeNo; }
            set { this.SetField(p => p.EmployeeNo, ref employeeNo, value); }

        }

        private  string employeeUnitName;
        public string EmployeeUnitName
        {
            get { return employeeUnitName; }
            set { this.SetField(p => p.EmployeeUnitName, ref employeeUnitName, value); }

        }

        private  string employeeJobPositionName;
        public string EmployeeJobPositionName
        {
            get { return employeeJobPositionName; }
            set { this.SetField(p => p.EmployeeJobPositionName, ref employeeJobPositionName, value); }

        }

        private  string totalPoint;
        public string TotalPoint
        {
            get { return totalPoint; }
            set { this.SetField(p => p.TotalPoint, ref totalPoint, value); }

        }

        private string levelTotalPoint;
        public string LevelTotalPoint
        {
            get { return levelTotalPoint; }
            set { this.SetField(p => p.LevelTotalPoint, ref levelTotalPoint, value); }

        }

        private  string totalUnitPoint;
        public string TotalUnitPoint
        {
            get { return totalUnitPoint; }
            set { this.SetField(p => p.TotalUnitPoint, ref totalUnitPoint, value); }

        }

        private List<JobIndexValueDTO> jobIndexValues;
        public List<JobIndexValueDTO> JobIndexValues
        {
            get { return jobIndexValues; }
            set { this.SetField(p => p.JobIndexValues, ref jobIndexValues, value); }

        }
    }
}
