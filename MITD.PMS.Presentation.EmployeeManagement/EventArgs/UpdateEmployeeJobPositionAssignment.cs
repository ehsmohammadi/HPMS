using System;
using System.Collections.Generic;
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

namespace MITD.PMS.Presentation.Logic
{
    public class UpdateEmployeeJobPositionAssignment
    {
        public UpdateEmployeeJobPositionAssignment(EmployeeJobPositionAssignmentDTO employeeJobPositionAssignment,
            PeriodDTO period, EmployeeDTO employee, ActionType action)
        {
            EmployeeJobPositionAssignment = employeeJobPositionAssignment;
            Period = period;
            Employee = employee;
            Action = action;
        }
        public EmployeeJobPositionAssignmentDTO EmployeeJobPositionAssignment
        {
            get; private set; 
        }

        public PeriodDTO Period
        {
            get;
            private set;
        }

        public EmployeeDTO Employee
        {
            get;
            private set;
        }

        public ActionType Action
        {
            get;
            private set;
        } 
    }
}
