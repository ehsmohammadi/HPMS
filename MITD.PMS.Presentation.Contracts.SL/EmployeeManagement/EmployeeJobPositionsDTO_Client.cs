using System.Collections.ObjectModel;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeJobPositionsDTO:ViewModelBase
    {
        private ObservableCollection<EmployeeJobPositionAssignmentDTO> employeeJobPositionAssignmentList;
        public ObservableCollection<EmployeeJobPositionAssignmentDTO> EmployeeJobPositionAssignmentList
        {
            get { return employeeJobPositionAssignmentList; }
            set { this.SetField(p => p.EmployeeJobPositionAssignmentList, ref employeeJobPositionAssignmentList, value); }
        }
    }
}
