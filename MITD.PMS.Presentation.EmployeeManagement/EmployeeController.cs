using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.EmployeeManagement
{
    public class EmployeeController : IEmployeeController
    {
        private readonly IViewManager viewManager;
        public EmployeeController(IViewManager viewManager)
        {
            this.viewManager = viewManager;
        }

        public void ShowEmployeeListView(PeriodDTO period, bool isShiftPressed)
        {
            var view = viewManager.ShowInTabControl<IEmployeeListView>();
            ((EmployeeListVM)view.ViewModel).Load(period);
        }

        public void ShowEmployeeView(EmployeeDTO employee, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IEmployeeView>();
            ((EmployeeVM)view.ViewModel).Load(employee, action);
            viewManager.ShowInDialog(view);
        }

        public void ShowEmployeeJobPositionsView(EmployeeDTO employee, PeriodDTO period)
        {
            var view = viewManager.ShowInTabControl<IEmployeeJobPositionsView>(v => ((EmployeeJobPositionsVM)v).Employee.PersonnelNo == employee.PersonnelNo
                && ((EmployeeJobPositionsVM)v).Period.Id == period.Id);
            ((EmployeeJobPositionsVM)view.ViewModel).Load(employee, period);
        }

        public void ShowEmployeeJobCustomFieldsView(EmployeeDTO employee, PeriodDTO period,
                                                    EmployeeJobPositionAssignmentDTO jobPositionAssignment,
                                                    ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IEmployeeJobCustomFieldsView>();
            ((EmployeeJobCustomFieldsVM)view.ViewModel).Load(employee, period, jobPositionAssignment, action);
            viewManager.ShowInDialog(view);
        }

        public void ShowSubordinatesConfirmationView(string employeeNo, PeriodDTO period, bool isShiftPressed)
        {
            var view = viewManager.ShowInTabControl<ISubordinatesConfirmationView>();
            ((SubordinatesConfirmationVM)view.ViewModel).Load(employeeNo, period);
        }
    }
}
