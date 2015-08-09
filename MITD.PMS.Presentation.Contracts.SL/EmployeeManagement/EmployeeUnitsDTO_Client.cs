using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeUnitsDTO : ViewModelBase
    {
        private ObservableCollection<EmployeeUnitAssignmentDTO> _employeeUnitAssignmentDtos;
        public ObservableCollection<EmployeeUnitAssignmentDTO> EmployeeUnitAssignmentDtos
        {
            get { return _employeeUnitAssignmentDtos; }
            set { this.SetField(p => p.EmployeeUnitAssignmentDtos, ref _employeeUnitAssignmentDtos, value); }
        }
    }
}
