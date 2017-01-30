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
    public interface IPeriodController
    {
        #region Work list commands

        [RequiredPermission(ActionType.ShowUnitInPeriodInquiry)]
        void ShowUnitsInquiryListView(string employeeNo, long periodId);

        [RequiredPermission(ActionType.ShowEmployeeInquiry)]
        void ShowEmployeesInquiryListView(string employeeNo, long id, bool showInNewTab = false);

        [RequiredPermission(ActionType.ShowEmployeeInquiry)]
        void ShowJobIndexInquiryListView(string employeeNo, long id, bool isShiftPressed);

        [RequiredPermission(ActionType.ShowCalculationResult)]
        void ShowPeriodCalculationResultView(PeriodDTO period, string employeeNo, bool isShiftPressed = false);
        void ShowCalculationResultForManagerView(PeriodDTO period, string employeeNo, bool isShiftPressed = false);
        void ShowCalculationResultForTrainingUnitView(PeriodDTO period, string employeeNo, bool isShiftPressed = false);

        #endregion

        void ShowInquiryUnitFormView(InquiryUnitFormDTO inquiryForm, ActionType action);
        void ShowInquiryFormView(InquiryFormDTO inquiryForm, ActionType action);
        void ShowJobIndexInquiryFormView(InquiryFormByIndexDTO inquiryForm, ActionType fillInquiryForm);


        void ShowPeriodView(PeriodDTO period, ActionType actionType);

        [RequiredPermission(ActionType.ShowPeriod)]
        void ShowPeriodList(bool showInNewTab);

        [RequiredPermission(ActionType.ShowJobInPeriod)]
        void ShowJobInPeriodListView(PeriodDTO period, bool showInNewTab = false);
        void ShowJobInPeriodView(long periodId, long? jobId, ActionType actionType);

        //  void ShowUnitInPeriodView(UnitInPeriodAssignmentDTO unitInPeriod, ActionType action);
        void ShowUnitInPeriodView(long periodId, long? unitId, long? parentId, ActionType actionType);

        [RequiredPermission(ActionType.ShowUnitInPeriod)]
        void ShowUnitInPeriodTreeView(PeriodDTOWithAction period, bool showInNewTab = false);
        void ShowUnitIndexInPeriodView(UnitIndexInPeriodDTO unitIndexInPeriodDto, ActionType action);

        [RequiredPermission(ActionType.ShowUnitIndexInPeriod)]
        void ShowUnitIndexInPeriodTreeView(PeriodDTOWithAction period, bool showInNewTab = false);
        void ShowUnitIndexGroupInPeriodView(UnitIndexGroupInPeriodDTO unitIndexGroupInPeriodDto, ActionType action);


        void ShowUnitInPeriodCustomFieldManageView(long periodId, UnitInPeriodDTO unitInPeriodDto, ActionType modifyUnitInPrdField);
        void ShowUnitInPeriodUnitIndicesManageView(long periodId, UnitInPeriodDTO unitInPeriodDto, ActionType modifyUnitInPrdField);

        void ShowJobPositionInPeriodView(JobPositionInPeriodAssignmentDTO jobPositionInPeriod, ActionType action);

        [RequiredPermission(ActionType.ShowJobPositionInPeriod)]
        void ShowJobPositionInPeriodTreeView(PeriodDTOWithAction period, bool showInNewTab = false);
        void ShowPeriodBasicDataCopyView(PeriodDTO period);
        void ShowPeriodBasicDataCopyStatusView(PeriodDTO period);

        void ShowJobIndexInPeriodView(JobIndexInPeriodDTO jobIndexInPeriod, ActionType action);

        [RequiredPermission(ActionType.ShowJobIndexInPeriod)]
        void ShowJobIndexInPeriodTreeView(PeriodDTOWithAction period, bool showInNewTab = false);
        void ShowJobIndexGroupInPeriodView(JobIndexGroupInPeriodDTO jobIndexGroupInPeriod, ActionType action);
        void ShowJobPositionInPeriodInquiryView(PeriodDTO period, JobPositionInPeriodDTO jobPositionInPeriod, ActionType action);

        void ShowPrepareToExcuteInquiryView(long id);




        [RequiredPermission(ActionType.ManageCalculations)]
        void ShowCalculationListView(PeriodDTOWithAction periodId, bool showInNewTab = false);
        void ShowCalculationExceptionView(CalculationExceptionDTO calculationExceptionDto);
        void ShowCalculationResultListView(long calcId, long periodId, bool showInNewTab = false);
        //void AddPeriodCalculationView(CalculationDTO calculation);

        void ShowPeriodCalculationExecView(CalculationDTO calculation, ActionType action);
        void ShowPeriodCalculationStateView(long calculationId);
        void ShowEmployeeCalculationResultHistoryView(long employeeId, bool showInNewTab = false);
        void ShowJobInPeriodCustomFieldManageView(long periodId, JobInPeriodDTO jobInPeriod, ActionType modifyJobInPrdField);
        void ShowJobInPeriodJobIndicesManageView(long periodId, JobInPeriodDTO jobInPeriod, ActionType modifyJobInPrdField);

        void ShowEmployeeClaimListView(PeriodDTO period, string employeeNo, bool showInNewTab = false);
        void ShowClaimView(ClaimDTO res, ActionType action);
        void ShowClaimView(ClaimDTO claim);
        void ShowPermittedUserListToMyTasksView(UserStateDTO employeeNo);
        void ShowPermittedUserToMyTasksView(UserStateDTO employeeNo);
        void ShowPeriodStatusView(PeriodDTO periodDTO, ActionType actionType);
        void ShowManagerClaimListView(PeriodDTO periodDTO, bool showInNewTab = false);
        void ShowCalculationExceptionListView(CalculationDTO calculation);


        void ShowUnitInPeriodVerifierView(PeriodDTO period, UnitInPeriodDTO unitInPeriodDto, ActionType action);
        void ShowUnitInPeriodInquiryView(PeriodDTO period, UnitInPeriodDTO unitInPeriodDto, ActionType action);
    }
}
