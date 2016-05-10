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
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IMainAppLocalizedResources : ILocalizedResources
    {
        string PeriodManagementMainMenu { get; }
        string EmployeeManagementMainMenu { get; }
        string EmployeeListSubMenu { get; }
        string PeriodListSubMenu { get; }
        string BasicInfoManagement { get; }
        string BasicInfoManagementShowCustomFieldsTitle { get; }
        string BasicInfoManagementShowJobsTitle { get; }
        string BasicInfoManagementShowJobIndexsTitle { get; }
        string BasicInfoManagementShowUnitsTitle { get; }
        string BasicInfoManagementShowJobPositionsTitle { get; }
        string BasicInfoManagementShowPliciesTitle { get; }
        string PMSTitle { get; }
        string WorkListManagement { get; }
        string EmployeesInquiryListSubMenu { get; }
        string EmployeesMyInquiryHistorySubMenu { get; }
        string EmployeesMyInquiryResultSubMenu { get; }
        string ClaimSubMenu { get; }
        string MyTasksAccessibilitySettingSubMenu { get; }
        string BasicInfoManagementShowUsersTitle { get; }
        string BasicInfoManagementShowUserGroupsTitle { get; }
        string BasicInfoManagementShowLogsTitle { get; }
        string ReportsMainMenu { get; }

        string JobIndex { get; }
        string Job { get; }
        string Employee { get; }
        string CustomFieldType { get; }
        string JobPosition { get; }
        string Unit { get; }
        string Rule { get; }
        string Function { get; }
        string UserGroup { get; }
        string JobIndexGroup { get; }
        string JobIndexCategory { get; }
        string Policy { get; }
        string Period { get; }
        string Claim { get; }
        string ClaimType { get; }
        string Calculation { get; }
        string User { get; }
        string DictionaryName { get; }
        string LastName { get; }
        string Name { get; }
        string StartDate { get; }
        string EndDate { get; }


        string DuplicateException { get; }
        string DeleteException { get; }
        string InvalidArgumentException { get; }
        string CompareException { get; }
        string ModifyForbiddenException { get; }
        string InvalidStateOperationException { get; }
        string CouldNotActivatePeriodWhileExistsAnotherActivePeriod { get; }
        string CouldNotClosePeriodWithOpenClaims { get; }

        string CouldNotClosePeriodWithoutAnyDeterministicCalculation
        {
            get;
        }

        string CouldNotCompleteInquiryWithNotFilledInquiryForms
        {
            get;
        }

        string CouldNotDeleteClaimByAnotherUser
        {
            get;
        }

        string CouldNotDeleteDeterministicCalculation
        {
            get;
        }

        string CouldNotInitializeInquiryForInactivePeriod
        {
            get;
        }

        string CouldNotModifyJobIndicesInInquiryStartedState
        {
            get;
        }

        string CouldNotStartConfirmationWithoutAnyDeterministicCalculation
        {
            get;
        }

        string DoesNotExistAnyActivePeriod
        {
            get;
        }

        string DoesNotExistAnyDeterministicCalculationInPeriod
        {
            get;
        }

        string DoesNotExistEvaluationForEmployee
        {
            get;
        }

        string ExceedViolationInDeterministicCalculationInPeriod
        {
            get;
        }

        string InvalidSumEmployeeWorkTimePercents
        {
            get;
        }

        string InvalidUsernameOrPassword
        {
            get;
        }

        string UnauthorizedAccessToOperation
        {
            get;
        }

        string UnExpectedException { get; }


        string PeriodInitState { get; }
        string PeriodBasicDataCopying { get; }
        string PeriodInitializingForInquiryState { get; }
        string PeriodInitializeInquiryCompletedState { get; }
        string PeriodInquiryStartedState { get; }
        string PeriodInquiryCompletedState { get; }
        string PeriodClaimingStartedState { get; }
        string PeriodClaimingFinishedState { get; }
        string PeriodClosedState { get; }


        string CalculationInitState{ get; }
        string CalculationRunningState{ get; }
        string CalculationPausedState{ get; }
        string CalculationStoppedState{ get; }
        string CalculationCompletedState{ get; }
        string CalculationNotCompletedState{ get; }

        string InitializeInquiry
        {
            get;
        }

        string CompleteIntializingForInquiry
        {
            get;
        }

        string CompleteCopyingBasicData
        {
            get;
        }


        string CopyPeriodBasicData
        {
            get;
        }

        string StartInquiry
        {
            get;
        }

        string CompleteInquiry
        {
            get;
        }

        string StartClaiming
        {
            get;
        }

        string FinishClaiming
        {
            get;
        }

        string Close
        {
            get;
        }

        string InitializeInquiryProgress
        {
            get;
        }

        string CopyingStateProgress
        {
            get;
        }

        string RollBack
        {
            get;
        }

        string ChangeActiveStatus
        {
            get;
        }

        string CheckAssigningUnit
        {
            get;
        }

        string CheckRemovingUnit
        {
            get;
        }

        string CheckAssigningJobIndex
        {
            get;
        }

        string CheckModifingJobIndex
        {
            get;
        }

        string CheckAssigningJob
        {
            get;
        }

        string CheckModifyingJobCustomFields
        {
            get;
        }

        string CheckModifyingJobIndices
        {
            get;
        }



        string CheckAssigningJobPosition
        {
            get;
        }

        string CheckModifyingJobPositionInquirers
        {
            get;
        }

        string CheckCreatingEmployee
        {
            get;
        }

        string CheckModifyingEmployeeCustomFieldsAndValues
        {
            get;
        }

        string CheckModifyingEmployeeJobPositions
        {
            get;
        }

        string CheckCreatingCalculation
        {
            get;
        }

        string CheckShowingInquirySubject
        {
            get;
        }

        string CheckSettingInquiryJobIndexPointValue
        {
            get;
        }

        string CheckAddClaim
        {
            get;
        }

        string CheckReplyClaim
        {
            get;
        }

        string CheckCancelClaim
        {
            get;
        }

        string CheckChangeCalculationDeterministicStatus
        {
            get;
        }

        string BasicInfoManagementShowUnitIndexsTitle { get; }
    }
}
