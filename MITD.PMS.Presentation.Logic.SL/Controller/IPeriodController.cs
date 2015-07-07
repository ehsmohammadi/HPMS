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
        void ShowPeriodView(PeriodDTO period, ActionType actionType);
        void ShowPeriodList(bool showInNewTab);

        void ShowJobInPeriodListView(PeriodDTO period, bool showInNewTab = false);
        void ShowJobInPeriodView(long periodId, long? jobId, ActionType actionType);

        void ShowUnitInPeriodView(UnitInPeriodAssignmentDTO unitInPeriod, ActionType action);
        void ShowUnitInPeriodView(long periodId, long? unitId, ActionType actionType);


        void ShowUnitInPeriodTreeView(PeriodDTOWithAction period, bool showInNewTab = false);

        void ShowUnitInPeriodCustomFieldManageView(long periodId, UnitInPeriodDTO unitInPeriodDto, ActionType modifyUnitInPrdField);
        void ShowUnitInPeriodUnitIndicesManageView(long periodId, UnitInPeriodDTO unitInPeriodDto, ActionType modifyUnitInPrdField);

      




        void ShowJobPositionInPeriodView(JobPositionInPeriodAssignmentDTO jobPositionInPeriod, ActionType action);
        void ShowJobPositionInPeriodTreeView(PeriodDTOWithAction period, bool showInNewTab = false);
        void ShowPeriodBasicDataCopyView(PeriodDTO period);
        void ShowPeriodBasicDataCopyStatusView(PeriodDTO period);

        void ShowJobIndexInPeriodView(JobIndexInPeriodDTO jobIndexInPeriod, ActionType action);
        void ShowJobIndexInPeriodTreeView(PeriodDTOWithAction period, bool showInNewTab = false);
        void ShowJobIndexGroupInPeriodView(JobIndexGroupInPeriodDTO jobIndexGroupInPeriod, ActionType action);
        void ShowJobPositionInPeriodInquiryView(PeriodDTO period, JobPositionInPeriodDTO jobPositionInPeriod, ActionType action);

        void ShowPrepareToExcuteInquiryView(long id);
        void ShowEmployeesInquiryListView(string employeeNo, long id, bool showInNewTab = false);
        void ShowInquiryFormView(InquiryFormDTO inquiryForm, ActionType action);
        void ShowCalculationListView(PeriodDTOWithAction periodId, bool showInNewTab = false);
        void ShowCalculationExceptionView(CalculationExceptionDTO calculationExceptionDto);
        void ShowCalculationResultListView(long calcId, bool showInNewTab = false);
        //void AddPeriodCalculationView(CalculationDTO calculation);

        void ShowPeriodCalculationResultView(PeriodDTO currentPeriod, string employeeNo, bool isShiftPressed);

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
    }
}
