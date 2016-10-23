using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.PMS.Presentation.Logic.Views;
using MITD.PMS.Presentation.Logic.Views.PeriodManagement.UnitIndex;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class PeriodController : IPeriodController
    {
        private readonly IViewManager viewManager;
        private IEventPublisher eventPublisher;
        private IDeploymentManagement deploymentManagement;
        public PeriodController(IViewManager viewManager,
                                     IEventPublisher eventPublisher,
                                     IDeploymentManagement deploymentManagement)
        {
            this.viewManager = viewManager;
            this.eventPublisher = eventPublisher;
            this.deploymentManagement = deploymentManagement;
        }

        #region Period

        public void ShowUnitInPeriodInquiryView(PeriodDTO period, UnitInPeriodDTO unitInPeriodDto, ActionType action)
        {
            var view = viewManager.ShowInTabControl<IUnitInPeriodInquiryView>(v => ((UnitInPeriodInquiryVM)v).Period.Id == period.Id
                && ((UnitInPeriodInquiryVM)v).UnitInPeriod.Id == unitInPeriodDto.Id);
            ((UnitInPeriodInquiryVM)view.ViewModel).Load(period, unitInPeriodDto);
        }

        public void ShowPeriodView(PeriodDTO period, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IPeriodView>();
            ((PeriodVM)view.ViewModel).Load(period, actionType);
            viewManager.ShowInDialog(view);

        }

        public void ShowPeriodStatusView(PeriodDTO period, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IPeriodInitializeInquiryStatusView>();
            ((PeriodInitializeInquiryStatusVM)view.ViewModel).Load(period, actionType);
            viewManager.ShowInDialog(view);
        }

        public void ShowPeriodList(bool showInNewTab = false)
        {
            var view = viewManager.ShowInTabControl<IPeriodListView>(showInNewTab);
            ((PeriodListVM)view.ViewModel).Load();
        }
        #endregion

        #region Unit

        //public void ShowUnitInPeriodView(UnitInPeriodAssignmentDTO unitInPeriod, ActionType action)
        //{
        //    var view = ServiceLocator.Current.GetInstance<IUnitInPeriodView>();
        //    ((UnitInPeriodVM)view.ViewModel).Load(unitInPeriod.PeriodId, unitInPeriod.UnitId, action);
        //    viewManager.ShowInDialog(view);
        //}
        public void ShowUnitInPeriodView(long periodId, long? unitId, long? parentId, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IUnitInPeriodView>();
            ((UnitInPeriodVM)view.ViewModel).Load(periodId, unitId, parentId, actionType);
            viewManager.ShowInDialog(view);
        }

        public void ShowUnitInPeriodTreeView(PeriodDTOWithAction period, bool showInNewTab = false)
        {
            var view = viewManager.ShowInTabControl<IUnitInPeriodTreeView>(showInNewTab);
            ((UnitInPeriodTreeVM)view.ViewModel).Load(period);
        }

        public void ShowUnitInPeriodCustomFieldManageView(long periodId, UnitInPeriodDTO unitInPeriodDto, ActionType modifyUnitInPrdField)
        {
            var view = ServiceLocator.Current.GetInstance<IUnitInPrdFieldView>();
            ((UnitInPrdFieldsVM)view.ViewModel).Load(periodId, unitInPeriodDto, modifyUnitInPrdField);
            viewManager.ShowInDialog(view);
        }

        public void ShowUnitInPeriodUnitIndicesManageView(long periodId, UnitInPeriodDTO unitInPeriodDto, ActionType modifyUnitInPrdField)
        {
            var view = ServiceLocator.Current.GetInstance<IUnitInPeriodUnitIndicesView>();
            ((UnitInPeriodUnitIndicesVM)view.ViewModel).Load(periodId, unitInPeriodDto, modifyUnitInPrdField);
            viewManager.ShowInDialog(view);
        }


        #endregion

        #region UnitIndex
        public void ShowUnitIndexInPeriodView(UnitIndexInPeriodDTO unitIndexInPeriodDto, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IUnitIndexInPeriodView>();
            ((UnitIndexInPeriodVM)view.ViewModel).Load(unitIndexInPeriodDto, action);
            viewManager.ShowInDialog(view);
        }
        public void ShowUnitIndexInPeriodTreeView(PeriodDTOWithAction period, bool showInNewTab = false)
        {
            var view = ServiceLocator.Current.GetInstance<IUnitIndexInPeriodTreeView>();
            ((UnitIndexInPeriodTreeVM)view.ViewModel).Load(period);
            viewManager.ShowInTabControl(view);

        }
        public void ShowUnitIndexGroupInPeriodView(UnitIndexGroupInPeriodDTO unitIndexGroupInPeriodDto, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IUnitIndexGroupInPeriodView>();
            ((UnitIndexGroupInPeriodVM)view.ViewModel).Load(unitIndexGroupInPeriodDto, action);
            viewManager.ShowInDialog(view);
        }

        #endregion



        #region JobPosition
        public void ShowJobPositionInPeriodView(JobPositionInPeriodAssignmentDTO jobPositionInPeriod, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IJobPositionInPeriodView>();
            ((JobPositionInPeriodVM)view.ViewModel).Load(jobPositionInPeriod, action);
            viewManager.ShowInDialog(view);
        }

        public void ShowJobPositionInPeriodTreeView(PeriodDTOWithAction period, bool showInNewTab = false)
        {
            var view = viewManager.ShowInTabControl<IJobPositionInPeriodTreeView>(showInNewTab);
            ((JobPositionInPeriodTreeVM)view.ViewModel).Load(period);
        }

        public void ShowPeriodBasicDataCopyView(PeriodDTO period)
        {
            var view = ServiceLocator.Current.GetInstance<IPeriodBasicDataCopyView>();
            ((PeriodBasicDataCopyVM)view.ViewModel).Load(period);
            viewManager.ShowInDialog(view);
        }

        public void ShowPeriodBasicDataCopyStatusView(PeriodDTO period)
        {
            var view = ServiceLocator.Current.GetInstance<IPeriodBasicDataCopyStatusView>();
            ((PeriodBasicDataCopyStatusVM)view.ViewModel).Load(period);
            viewManager.ShowInDialog(view);
        }

        public void ShowJobPositionInPeriodInquiryView(PeriodDTO period, JobPositionInPeriodDTO jobPositionInPeriod, ActionType action)
        {
            var view = viewManager.ShowInTabControl<IJobPositionInPeriodInquiryView>(v => ((JobPositionInPeriodInquiryVM)v).Period.Id == period.Id
                && ((JobPositionInPeriodInquiryVM)v).JobPositionInPeriod.Id == jobPositionInPeriod.Id);
            ((JobPositionInPeriodInquiryVM)view.ViewModel).Load(period, jobPositionInPeriod);
        }



        #endregion

        #region Job
        public void ShowJobInPeriodListView(PeriodDTO period, bool showInNewTab = false)
        {
            var view = viewManager.ShowInTabControl<IJobInPeriodListView>(showInNewTab);
            ((JobInPeriodListVM)view.ViewModel).Load(period);
        }

        public void ShowJobInPeriodView(long periodId, long? jobId, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IJobInPeriodView>();
            ((JobInPeriodVM)view.ViewModel).Load(periodId, jobId, actionType);
            viewManager.ShowInDialog(view);
        }

        public void ShowJobInPeriodCustomFieldManageView(long periodId, JobInPeriodDTO jobInPeriod, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IJobInPrdFieldView>();
            ((JobInPrdFieldsVM)view.ViewModel).Load(periodId, jobInPeriod, actionType);
            viewManager.ShowInDialog(view);
        }

        public void ShowJobInPeriodJobIndicesManageView(long periodId, JobInPeriodDTO jobInPeriod, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IJobInPeriodJobIndicesView>();
            ((JobInPeriodJobIndicesVM)view.ViewModel).Load(periodId, jobInPeriod, actionType);
            viewManager.ShowInDialog(view);
        }

        public void ShowClaimView(ClaimDTO claim, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IClaimView>();
            ((ClaimVM)view.ViewModel).Load(claim, action);
            viewManager.ShowInDialog(view);
        }

        public void ShowClaimView(ClaimDTO claim)
        {
            var view = ServiceLocator.Current.GetInstance<IShowClaimView>();
            ((ShowClaimVM)view.ViewModel).Load(claim);
            viewManager.ShowInDialog(view);
        }

        public void ShowEmployeeClaimListView(PeriodDTO period, string employeeNo, bool showInNewTab = false)
        {
            var view = viewManager.ShowInTabControl<IEmployeeClaimListView>(showInNewTab);
            ((EmployeeClaimListVM)view.ViewModel).Load(period, employeeNo);
        }

        public void ShowManagerClaimListView(PeriodDTO period, bool showInNewTab = false)
        {
            var view = viewManager.ShowInTabControl<IManagerClaimListView>(showInNewTab);
            ((ManagerClaimListVM)view.ViewModel).Load(period);
        }

        public void ShowCalculationExceptionListView(CalculationDTO calculation)
        {
            var view = ServiceLocator.Current.GetInstance<ICalculationExceptionListView>();
            ((CalculationExceptionListVM)view.ViewModel).Load(calculation);
            viewManager.ShowInDialog(view);
        }

        public void ShowCalculationExceptionView(CalculationExceptionDTO calculationException)
        {
            var view = ServiceLocator.Current.GetInstance<ICalculationExceptionView>();
            ((CalculationExceptionVM)view.ViewModel).Load(calculationException);
            viewManager.ShowInDialog(view);
        }

        public void ShowPermittedUserListToMyTasksView(UserStateDTO employee)
        {
            var view = viewManager.ShowInTabControl<IPermittedUserListToMyTasksView>();
            ((PermittedUserListToMyTasksVM)view.ViewModel).Load(employee);
        }

        public void ShowPermittedUserToMyTasksView(UserStateDTO employee)
        {
            var view = ServiceLocator.Current.GetInstance<IPermittedUserToMyTasksView>();
            ((PermittedUserToMyTasksVM)view.ViewModel).Load(employee);
            viewManager.ShowInDialog(view);
        }



        #endregion

        #region JobIndex

        public void ShowJobIndexInPeriodView(JobIndexInPeriodDTO jobIndexInPeriod, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IJobIndexInPeriodView>();
            ((JobIndexInPeriodVM)view.ViewModel).Load(jobIndexInPeriod, action);
            viewManager.ShowInDialog(view);
        }

        public void ShowJobIndexInPeriodTreeView(PeriodDTOWithAction period, bool showInNewTab = false)
        {
            var view = viewManager.ShowInTabControl<IJobIndexInPeriodTreeView>(showInNewTab);
            ((JobIndexInPeriodTreeVM)view.ViewModel).Load(period);
        }

        public void ShowJobIndexGroupInPeriodView(JobIndexGroupInPeriodDTO jobIndexGroupInPeriod, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IJobIndexGroupInPeriodView>();
            ((JobIndexGroupInPeriodVM)view.ViewModel).Load(jobIndexGroupInPeriod, action);
            viewManager.ShowInDialog(view);
        }

        #endregion

        #region Inquiry

        public void ShowInquiryUnitFormView(InquiryUnitFormDTO inquiryForm, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IUnitInquiryFormView>();
            ((InquiryUnitFormVM)view.ViewModel).Load(inquiryForm, action);
            viewManager.ShowInDialog(view);
        }
        public void ShowUnitsInquiryListView(string employeeNo, long periodId)
        {
            var view = viewManager.ShowInTabControl<IUnitsInquiryListView>();
            ((InquirerInquiryUnitListVM)view.ViewModel).Load(employeeNo, periodId);
        }

        public void ShowPrepareToExcuteInquiryView(long periodId)
        {
            var view = ServiceLocator.Current.GetInstance<IPrepareToExecInquiryView>();
            ((PrepareToExecInquiryVM)view.ViewModel).Load(periodId);
            viewManager.ShowInDialog(view);
        }

        public void ShowEmployeesInquiryListView(string employeeNo, long periodId, bool showInNewTab = false)
        {
            var view = viewManager.ShowInTabControl<IEmployeesInquiryListView>(showInNewTab);
            ((InquirerInquirySubjectListVM)view.ViewModel).Load(employeeNo, periodId);
        }

        public void ShowJobIndexInquiryListView(string employeeNo, long periodId, bool isShiftPressed = false)
        {
            var view = viewManager.ShowInTabControl<IJobIndexInquiryListView>(isShiftPressed);
            ((JobIndexInquiryListVM)view.ViewModel).Load(employeeNo, periodId);
        }

        public void ShowInquiryFormView(InquiryFormDTO inquiryForm, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IInquiryFormView>();
            ((InquiryFormVM)view.ViewModel).Load(inquiryForm, action);
            viewManager.ShowInDialog(view);
        }

        public void ShowJobIndexInquiryFormView(InquiryFormByIndexDTO inquiryForm, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IJobIndexInquiryFormView>();
            ((JobIndexInquiryFormVM)view.ViewModel).Load(inquiryForm, action);
            viewManager.ShowInDialog(view);
        }

        #endregion

        public void ShowCalculationListView(PeriodDTOWithAction period, bool showInNewTab = false)
        {
            var view = viewManager.ShowInTabControl<ICalculationListView>(showInNewTab);
            ((CalculationListVM)view.ViewModel).Load(period);
        }

        public void ShowCalculationResultListView(long calculationId, long periodId, bool showInNewTab = false)
        {
            var view = viewManager.ShowInTabControl<ICalculationResultListView>(showInNewTab);
            ((CalculationResultListVM)view.ViewModel).Load(calculationId, periodId);
        }

        public void ShowPeriodCalculationExecView(CalculationDTO calculation, ActionType action)
        {
            var view = viewManager.ShowInTabControl<ICalculationView>();
            ((CalculationVM)view.ViewModel).Load(calculation, action);
        }

        public void ShowPeriodCalculationResultView(PeriodDTO currentPeriod, string employeeNo, bool isShiftPressed = false)
        {
            var view = viewManager.ShowInTabControl<ICalculationResultView>(isShiftPressed);
            ((CalculationResultVM)view.ViewModel).Load(employeeNo);
        }

        public void ShowCalculationResultForManagerView(PeriodDTO periodDTO, string employeeNo, bool isShiftPressed = false)
        {
            var view = viewManager.ShowInTabControl<ICalculationResultForManagerView>(isShiftPressed);
            ((CalculationResultForManagerVM)view.ViewModel).Load(employeeNo);
        }

        public void ShowPeriodCalculationStateView(long calculationId)
        {
            var view = viewManager.ShowInTabControl<ICalculationStatusView>();
            ((CalculationStatusVM)view.ViewModel).Load(calculationId);

        }

        public void ShowEmployeeCalculationResultHistoryView(long employeeId, bool showInNewTab = false)
        {
            var view = viewManager.ShowInTabControl<IEmployeeCalculationResultHistoryView>(showInNewTab);
            ((EmployeeCalculationResultHistoryVM)view.ViewModel).Load(employeeId);
        }




    }
}
