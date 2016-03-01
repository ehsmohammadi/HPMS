using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IEmployeeController 
    {


        [RequiredPermission(ActionType.ManageEmployees)]
        void ShowEmployeeListView(PeriodDTO period, bool isShiftPressed);
        void ShowEmployeeView(EmployeeDTO employee, ActionType action);
        void ShowEmployeeJobPositionsView(EmployeeDTO employee, PeriodDTO period);

        void ShowEmployeeJobCustomFieldsView(EmployeeDTO employee, PeriodDTO period, EmployeeJobPositionAssignmentDTO jobPositionAssignment, ActionType addEmployeeJobCustomFields);
    }
}
