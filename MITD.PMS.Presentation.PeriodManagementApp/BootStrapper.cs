using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation.Config;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class BootStrapper : IBootStrapper
    {
        public void Execute()
        {
            var resourceDic = App.GetResourceStream(new Uri("MITD.PMS.Presentation.PeriodManagementApp;component/Assets/LocalResource.xaml", UriKind.Relative));
            ApplicationConfigHelper.ConfigureModule<IPeriodController, PeriodController>(resourceDic);
            ModuleConfigHelper.ConfigureActionModule<IActionService>(new Dictionary<int, Type>
            {
                {(int) ActionType.AddPeriod, typeof (AddPeriodService)},
                {(int) ActionType.ModifyPeriod, typeof (ModifyPeriodService)},
                {(int) ActionType.DeletePeriod, typeof (DeletePeriodService)},
                {(int) ActionType.ManageUnits, typeof (ManageUnitInPeriodService)},
                {(int) ActionType.ManageJobs, typeof (ManageJobInPeriodService)},
                {(int) ActionType.ManageJobIndices, typeof (ManageJobIndexInPeriodService)},
                {(int) ActionType.ManageUnitIndices, typeof (ManageUnitIndexInPeriodService)},
                {(int) ActionType.ManageJobPositions, typeof (ManageJobPositionInPeriodService)},
                {(int) ActionType.ManageEmployees, typeof (ManageEmployeeService)},
                {(int) ActionType.ManageCalculations, typeof (ManagePeriodCaculationsService)},
                {(int) ActionType.ActivatePeriod, typeof (ActivatePeriodService)},
                {(int) ActionType.InitializePeriodForInquiry, typeof (InitializeForInquiryService)},
                {(int) ActionType.StartInquiry, typeof (StartInquiryService)},
                {(int) ActionType.CompleteInquiry, typeof (CompleteInquiryService)},
                {(int) ActionType.ClosePeriod, typeof (ClosePeriodService)},
                {(int) ActionType.CopyPeriodBasicData, typeof (BasicDataCopyService)},
                {(int) ActionType.GetPeriodInitializingInquiryStatus, typeof (GetPeriodInitializingInquiryStatusService)},
                {(int) ActionType.RollBackPeriodState, typeof (RollBackPeriodService)},

                {(int) ActionType.StartCliaming, typeof (StartClaimingPeriodService)},
                {(int) ActionType.FinishCliaming, typeof (FinishClaimingPeriodService)},

                {(int) ActionType.AddUnitInPeriod, typeof (AddUnitInPeriodService)},
                {(int) ActionType.ModifyUnitInPeriod, typeof (ModifyUnitInPeriodService)},
                {(int) ActionType.DeleteUnitInPeriod, typeof (DeleteUnitInPeriodService)},
                {(int) ActionType.ManageUnitInPeriodInquiry, typeof (ManageUnitInPeriodInquiryService)},

                {(int) ActionType.AddJobPositionInPeriod, typeof (AddJobPositionInPeriodService)},
                {(int) ActionType.DeleteJobPositionInPeriod, typeof (DeleteJobPositionInPeriodService)},
                {(int) ActionType.ManageJobPositionInPeriodInquiry, typeof (ManageJobPositionInPeriodInquiryService)},

                {(int) ActionType.AddJobIndexInPeriod, typeof (AddJobIndexInPeriodService)},
                {(int) ActionType.ModifyJobIndexInPeriod, typeof (ModifyJobIndexInPeriodService)},
                {(int) ActionType.DeleteJobIndexInPeriod, typeof (DeleteJobIndexInPeriodService)},

                {(int) ActionType.AddJobIndexGroupInPeriod, typeof (AddJobIndexGroupInPeriodService)},
                {(int) ActionType.ModifyJobIndexGroupInPeriod, typeof (ModifyJobIndexGroupInPeriodService)},
                {(int) ActionType.DeleteJobIndexGroupInPeriod, typeof (DeleteJobIndexGroupInPeriodService)},

                {(int) ActionType.AddUnitIndexInPeriod, typeof (AddUnitIndexInPeriodService)},
                {(int) ActionType.ModifyUnitIndexInPeriod, typeof (ModifyUnitIndexInPeriodService)},
                {(int) ActionType.DeleteUnitIndexInPeriod, typeof (DeleteUnitIndexInPeriodService)},

                {(int) ActionType.AddUnitIndexGroupInPeriod, typeof (AddUnitIndexGroupInPeriodService)},
                {(int) ActionType.ModifyUnitIndexGroupInPeriod, typeof (ModifyUnitIndexGroupInPeriodService)},
                {(int) ActionType.DeleteUnitIndexGroupInPeriod, typeof (DeleteUnitIndexGroupInPeriodService)},

                {(int) ActionType.FillInquiryForm, typeof (ManageInquiryFormService)},

                {(int) ActionType.AddCalculation, typeof (AddPeriodCalculationExecService)},
                //{(int) ActionType.ModifyPeriodCalculationExec, typeof (ModifyPeriodCalculationService)},
                {(int) ActionType.DeleteCalculation, typeof (DeletePeriodCalculationService)},
                {(int) ActionType.SetDeterministicCalculation, typeof (SetDeterministicCalculationService)},
                {(int) ActionType.UnsetDeterministicCalculation, typeof (UnsetDeterministicCalculationService)},
                {(int) ActionType.ShowCalculationResult, typeof (ShowCalculationResultService)},
                {(int) ActionType.ShowCalculationState, typeof (ShowPeriodCalculationStateService)},
                //{(int) ActionType.ShowPeriodCalculationResultDetails, typeof (ShowPeriodCalculationResultDetailsService)},
                {(int) ActionType.RunCalculation, typeof (RunPeriodCalculationService)},
                //{(int) ActionType.StopCalculation, typeof (ShowPeriodCalculationResultDetailsService)},
                {(int) ActionType.ShowCalculationException, typeof (ShowCalculationExceptionService)},
                {(int) ActionType.ShowAllCalculationException, typeof (ShowAllCalculationExceptionService)},
 
                {(int) ActionType.AddJobInPeriod, typeof (AddJobInPeriodService)},
                {(int) ActionType.ModifyJobInPeriod, typeof (ModifyJobInPeriodService)},
                {(int) ActionType.DeleteJobInPeriod, typeof (DeleteJobInPeriodService)},

                {(int) ActionType.AddClaim, typeof (AddClaimService)},
                {(int) ActionType.ShowClaim, typeof (ShowClaimService)},
                {(int) ActionType.ReplyToClaim, typeof (ModifyClaimService)},
                {(int) ActionType.DeleteClaim, typeof (DeleteClaimService)},
                {(int) ActionType.CancelClaim, typeof (CancelClaimService)},
               

                {(int) ActionType.AddPermittedUserToMyTasks, typeof (AddPermittedUserToMyTasksService)},
                {(int) ActionType.RemovePermittedUserFromMyTasks, typeof (RemovePermittedUserFromMyTasksService)},
                {(int) ActionType.SettingPermittedUserToMyTasks, typeof (SettingPermittedUserToMyTasksService)},


            });



        }
    }
}
