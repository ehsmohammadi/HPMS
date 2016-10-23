using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;
namespace MITD.PMS.Presentation.Contracts
{
    public partial class SubordinatesResultDTO 
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

        private string totalUnitPoint;
        public string TotalUnitPoint
        {
            get { return totalUnitPoint; }
            set { this.SetField(p => p.TotalUnitPoint, ref totalUnitPoint, value); }

        }

        private string employeeUnitRootName;
        public string EmployeeUnitRootName
        {
            get { return employeeUnitRootName; }
            set { this.SetField(p => p.EmployeeUnitRootName, ref employeeUnitRootName, value); }

        }

        private  string employeeUnitName;
        public string EmployeeUnitName
        {
            get { return employeeUnitName; }
            set { this.SetField(p => p.EmployeeUnitName, ref employeeUnitName, value); }

        }
        
        private List<EmployeeResultDTO> subordinates;
        public List<EmployeeResultDTO> Subordinates
        {
            get { return subordinates; }
            set { this.SetField(p => p.Subordinates, ref subordinates, value); }

        }
    }
}
