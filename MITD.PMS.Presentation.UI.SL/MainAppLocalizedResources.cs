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
using MITD.PMS.Presentation.Logic;
using MITD.PMS.Presentation.UI.SL.Assets.Resources;

namespace MITD.PMS.Presentation.UI
{
    public class MainAppLocalizedResources : IMainAppLocalizedResources
    {

        public string BasicInfoManagement
        {
            get
            {
                return MainAppStrings.BasicInfoManagement;
            }
        }

        public string PeriodManagementMainMenu
        {
            get
            {
                return MainAppStrings.PeriodManagementMainMenu;
            }
        }

        public string EmployeeManagementMainMenu
        {
            get
            {
                return MainAppStrings.EmployeeManagementMainMenu;
            }
        }

        public string EmployeeListSubMenu
        {
            get
            {
                return MainAppStrings.EmployeeListSubMenu;
            }
        }

        public string PeriodListSubMenu
        {
            get
            {
                return MainAppStrings.PeriodListSubMenu;
            }
        }

        public string BasicInfoManagementShowCustomFieldsTitle
        {
            get { return MainAppStrings.BasicInfoManagementShowCustomFieldsTitle; }
        }

        public string BasicInfoManagementShowJobsTitle
        {
            get { return MainAppStrings.BasicInfoManagementShowJobsTitle; }
        }

        public string BasicInfoManagementShowJobIndexsTitle
        {
            get { return MainAppStrings.BasicInfoManagementShowJobIndexsTitle; }
        }

        public string BasicInfoManagementShowUnitsTitle
        {
            get { return MainAppStrings.BasicInfoManagementShowUnitsTitle; }
        }

        public string BasicInfoManagementShowJobPositionsTitle
        {
            get { return MainAppStrings.BasicInfoManagementShowJobPositionsTitle; }
        }

        public string BasicInfoManagementShowPliciesTitle
        {
            get { return MainAppStrings.BasicInfoManagementShowPliciesTitle; }
        }

        public string PMSTitle
        {
            get { return MainAppStrings.PMSTitle; }
        }

        public string WorkListManagement
        {
            get { return MainAppStrings.WorkListManagement; }
        }

        public string EmployeesInquiryListSubMenu
        {
            get { return MainAppStrings.EmployeesInquiryListSubMenu; }
        }

        public string EmployeesMyInquiryHistorySubMenu
        {
            get { return MainAppStrings.EmployeesMyInquiryHistorySubMenu; }
        }

        public string EmployeesMyInquiryResultSubMenu
        {
            get { return MainAppStrings.EmployeesMyInquiryResultSubMenu; }
        }

        public string ClaimSubMenu
        {
            get { return MainAppStrings.ClaimSubMenu; }
        }

        public string MyTasksAccessibilitySettingSubMenu
        {
            get { return MainAppStrings.MyTasksAccessibilitySettingSubMenu; }
        }

        public string BasicInfoManagementShowUsersTitle
        {
            get { return MainAppStrings.BasicInfoManagementShowUsersTitle; }
        }


        public string BasicInfoManagementShowUserGroupsTitle
        {
            get { return MainAppStrings.BasicInfoManagementShowUserGroupsTitle; }
        }

        public string BasicInfoManagementShowLogsTitle
        {
            get { return MainAppStrings.BasicInfoManagementShowLogsTitle; }
        }

        public string ReportsMainMenu
        {
            get { return MainAppStrings.ReportsMainMenu; }
        }




        public string JobIndex
        {
            get { return MainAppStrings.JobIndex; }
        }

        public string Job
        {
            get { return MainAppStrings.Job; }
        }

        public string Employee
        {
            get { return MainAppStrings.Employee; }
        }

        public string CustomFieldType
        {
            get { return MainAppStrings.CustomFieldType; }
        }

        public string JobPosition
        {
            get { return MainAppStrings.JobPosition; }
        }

        public string Unit
        {
            get { return MainAppStrings.Unit; }
        }

        public string Rule
        {
            get { return MainAppStrings.Rule; }
        }

        public string Function
        {
            get { return MainAppStrings.Function; }
        }

        public string UserGroup
        {
            get { return MainAppStrings.UserGroup; }
        }

        public string JobIndexGroup
        {
            get { return MainAppStrings.JobIndexGroup; }
        }

        public string JobIndexCategory
        {
            get { return MainAppStrings.JobIndexCategory; }
        }

        public string Policy
        {
            get { return MainAppStrings.Policy; }
        }

        public string Period
        {
            get { return MainAppStrings.Period; }
        }

        public string Claim
        {
            get { return MainAppStrings.Claim; }
        }

        public string ClaimType
        {
            get { return MainAppStrings.ClaimType; }
        }

        public string Calculation
        {
            get { return MainAppStrings.Calculation; }
        }

        public string User
        {
            get { return MainAppStrings.User; }
        }




        public string DictionaryName
        {
            get { return MainAppStrings.DictionaryName; }
        }

        public string LastName
        {
            get { return MainAppStrings.LastName; }
        }

        public string Name
        {
            get { return MainAppStrings.Name; }
        }

        public string StartDate
        {
            get { return MainAppStrings.StartDate; }
        }

        public string EndDate
        {
            get { return MainAppStrings.EndDate; }
        }





        public string DuplicateException
        {
            get { return MainAppStrings.DuplicateException; }
        }

        public string DeleteException
        {
            get { return MainAppStrings.DeleteException; }
        }

        public string InvalidArgumentException
        {
            get { return MainAppStrings.InvalidArgumentException; }
        }

        public string CompareException
        {
            get { return MainAppStrings.CompareException; }
        }

        public string ModifyForbiddenException
        {
            get { return MainAppStrings.ModifyForbiddenException; }
        }

        public string InvalidStateOperationException
        {
            get { return MainAppStrings.InvalidStateOperationException; }
        }


        public string CouldNotActivatePeriodWhileExistsAnotherActivePeriod
        {
            get
            {
                return MainAppStrings.CouldNotActivatePeriodWhileExistsAnotherActivePeriod;
            }
        }

        public string CouldNotClosePeriodWithOpenClaims
        {
            get
            {
                return MainAppStrings.CouldNotClosePeriodWithOpenClaims;
            }
        }

        public string CouldNotClosePeriodWithoutAnyDeterministicCalculation
        {
            get
            {
                return MainAppStrings.CouldNotClosePeriodWithoutAnyDeterministicCalculation ;
            }
        }

        public string CouldNotCompleteInquiryWithNotFilledInquiryForms
        {
            get
            {
                return MainAppStrings.CouldNotCompleteInquiryWithNotFilledInquiryForms;
            }
        }

        public string CouldNotDeleteClaimByAnotherUser
        {
            get
            {
                return MainAppStrings.CouldNotDeleteClaimByAnotherUser;
            }
        }

        public string CouldNotDeleteDeterministicCalculation
        {
            get
            {
                return MainAppStrings.CouldNotDeleteDeterministicCalculation;
            }
        }

        public string CouldNotInitializeInquiryForInactivePeriod
        {
            get
            {
                return MainAppStrings.CouldNotInitializeInquiryForInactivePeriod;
            }
        }

        public string CouldNotModifyJobIndicesInInquiryStartedState
        {
            get
            {
                return MainAppStrings.CouldNotModifyJobIndicesInInquiryStartedState ;
            }
        }

        public string CouldNotStartClaimingWithoutAnyDeterministicCalculation
        {
            get
            {
                return MainAppStrings.CouldNotStartClaimingWithoutAnyDeterministicCalculation ;
            }
        }

        public string DoesNotExistAnyActivePeriod
        {
            get
            {
                return MainAppStrings.DoesNotExistAnyActivePeriod;
            }
        }

        public string DoesNotExistAnyDeterministicCalculationInPeriod
        {
            get
            {
                return MainAppStrings.DoesNotExistAnyDeterministicCalculationInPeriod ;
            }
        }

        public string DoesNotExistEvaluationForEmployee
        {
            get
            {
                return MainAppStrings.DoesNotExistEvaluationForEmployee ;
            }
        }

        public string ExceedViolationInDeterministicCalculationInPeriod
        {
            get
            {
                return MainAppStrings.ExceedViolationInDeterministicCalculationInPeriod ;
            }
        }

        public string InvalidSumEmployeeWorkTimePercents
        {
            get
            {
                return MainAppStrings.InvalidSumEmployeeWorkTimePercents ;
            }
        }

        public string InvalidUsernameOrPassword
        {
            get
            {
                return MainAppStrings.InvalidUsernameOrPassword;
            }
        }

        public string UnauthorizedAccessToOperation
        {
            get
            {
                return MainAppStrings.UnauthorizedAccessToOperation;
            }
        }

        public string UnExpectedException
        {
            get
            {
                return MainAppStrings.UnExpectedException;
            }
        }

        public string PeriodInitState
        {
            get { return MainAppStrings.PeriodInitState ; }
        }

        public string PeriodBasicDataCopying
        {
            get { return MainAppStrings.PeriodBasicDataCopying; }
        }

        public string PeriodInitializingForInquiryState
        {
            get { return MainAppStrings.PeriodInitializingForInquiryState ; }
        }

        public string PeriodInitializeInquiryCompletedState
        {
            get { return MainAppStrings.PeriodInitializeInquiryCompletedState; }
        }

        public string PeriodInquiryStartedState
        {
            get { return MainAppStrings.PeriodInquiryStartedState ; }
        }

        public string PeriodInquiryCompletedState
        {
            get { return MainAppStrings.PeriodInquiryCompletedState ; }
        }

        public string PeriodClaimingStartedState
        {
            get { return MainAppStrings.PeriodClaimingStartedState; }
        }

        public string PeriodClaimingFinishedState
        {
            get { return MainAppStrings.PeriodClaimingFinishedState ; }
        }

        public string PeriodClosedState
        {
            get { return MainAppStrings.PeriodClosedState; }
        }

        public string CalculationInitState
        {
            get { return MainAppStrings.CalculationInitState ; }
        }

        public string CalculationRunningState
        {
            get { return MainAppStrings.CalculationRunningState ; }
        }

        public string CalculationPausedState
        {
            get { return MainAppStrings.CalculationPausedState ; }
        }

        public string CalculationStoppedState
        {
            get { return MainAppStrings.CalculationStoppedState; }
        }

        public string CalculationCompletedState
        {
            get { return MainAppStrings.CalculationCompletedState; }
        }

        public string CalculationNotCompletedState
        {
            get { return MainAppStrings.CalculationNotCompletedState ; }
        }

        public string InitializeInquiry
        {
            get { return MainAppStrings.InitializeInquiry ; }
        }

        public string CompleteIntializingForInquiry
        {
            get { return MainAppStrings.CompleteIntializingForInquiry ; }
        }

        public string CompleteCopyingBasicData
        {
            get { return MainAppStrings.CompleteCopyingBasicData; }
        }


        public string CopyPeriodBasicData
        {
            get { return MainAppStrings.CopyPeriodBasicData; }
        }

        public string StartInquiry
        {
            get { return MainAppStrings.StartInquiry; }
        }

        public string CompleteInquiry
        {
            get { return MainAppStrings.CompleteInquiry; }
        }

        public string StartClaiming
        {
            get { return MainAppStrings.StartClaiming; }
        }

        public string FinishClaiming
        {
            get { return MainAppStrings.FinishClaiming ; }
        }

        public string Close
        {
            get { return MainAppStrings.Close; }
        }

        public string  InitializeInquiryProgress
        {
            get { return MainAppStrings.InitializeInquiryProgress ; }
        }

        public string CopyingStateProgress
        {
            get { return MainAppStrings.CopyingStateProgress ; }
        }

        public string RollBack
        {
            get { return MainAppStrings.RollBack; }
        }

        public string ChangeActiveStatus
        {
            get { return MainAppStrings.ChangeActiveStatus ; }
        }

        public string CheckAssigningUnit 
        {
            get { return MainAppStrings.CheckAssigningUnit ; }
        }

        public string CheckRemovingUnit 
        {
            get { return MainAppStrings.CheckRemovingUnit ; }
        }

        public string CheckAssigningJobIndex 
         {
            get { return MainAppStrings.CheckRemovingUnit ; }
        }

        public string CheckModifingJobIndex 
         {
            get { return MainAppStrings.CheckModifingJobIndex ; }
        }

        public string CheckAssigningJob
        {
            get { return MainAppStrings.CheckAssigningJob ; }
        }

        public string CheckModifyingJobCustomFields
        {
            get { return MainAppStrings.CheckModifyingJobCustomFields ; }
        }

        public string CheckModifyingJobIndices
        {
            get { return MainAppStrings.CheckModifyingJobIndices ; }
        }



        public string CheckAssigningJobPosition
         {
             get { return MainAppStrings.CheckAssigningJobPosition ; }
        }

        public string CheckModifyingJobPositionInquirers
         {
            get { return MainAppStrings.CheckModifyingJobPositionInquirers ; }
        }

        public string CheckCreatingEmployee
         {
            get { return MainAppStrings.CheckCreatingEmployee ; }
        }

        public string CheckModifyingEmployeeCustomFieldsAndValues
         {
            get { return MainAppStrings.CheckModifyingEmployeeCustomFieldsAndValues; }
        }

        public string CheckModifyingEmployeeJobPositions
         {
            get { return MainAppStrings.CheckModifyingEmployeeJobPositions ; }
        }

        public string CheckCreatingCalculation
         {
            get { return MainAppStrings.CheckCreatingCalculation ; }
        }

        public string CheckShowingInquirySubject
         {
            get { return MainAppStrings.CheckShowingInquirySubject ; }
        }

        public string CheckSettingInquiryJobIndexPointValue
         {
            get { return MainAppStrings.CheckSettingInquiryJobIndexPointValue ; }
        }
        
        public string CheckAddClaim
         {
            get { return MainAppStrings.CheckAddClaim ; }
        }

        public string CheckReplyClaim
         {
            get { return MainAppStrings.CheckReplyClaim ; }
        }

        public string CheckCancelClaim
         {
            get { return MainAppStrings.CheckCancelClaim ; }
        }

        public string CheckChangeCalculationDeterministicStatus
        {
            get { return MainAppStrings.CheckChangeCalculationDeterministicStatus; }
        }

        public string BasicInfoManagementShowUnitIndexsTitle { get { return "شاخص های سازمانی"; } }
    }
}
