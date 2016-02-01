using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using MITD.PMS.Presentation.BasicInfoApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Castle.Core.Internal;

namespace MITD.PMS.Presentation.Logic
{
    public class JobPositionVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private IPMSController appController;
        private IJobPositionServiceWrapper jobPositionService;
        private ActionType actionType;

        #endregion

        #region Properties

        private JobPositionDTO jobPosition;
        public JobPositionDTO JobPosition
        {
            get { return jobPosition; }
            set { this.SetField(vm => vm.JobPosition, ref jobPosition, value); }
        }

        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel("تایید", new DelegateCommand(save));
                }
                return saveCommand;
            }
        }

        private CommandViewModel cancelCommand;
        public CommandViewModel CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new CommandViewModel("انصراف",new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        } 

        #endregion

        #region Constructors

        public JobPositionVM()
        {
            JobPosition = new JobPositionDTO { Name = "پست سازمانی یک", DictionaryName="JobPosition1" };
        }
        public JobPositionVM(IPMSController appController, IJobPositionServiceWrapper jobPositionService)
        {
            this.appController = appController;
            this.jobPositionService = jobPositionService;
            JobPosition = new JobPositionDTO();
            DisplayName = "پست سازمانی ";
        } 

        #endregion

        #region Methods

        public void Load(JobPositionDTO jobPositionParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            if (actionType == ActionType.ModifyJobPosition)
            {
                ShowBusyIndicator();
                jobPositionService.GetJobPosition( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp == null)
                            JobPosition = res;
                        else
                            appController.HandleException(exp);
                    }),
                                        jobPositionParam.Id);
            }
        }

       
        private void save()
        {
            if (!jobPosition.Validate()) return;

            ShowBusyIndicator();

            jobPosition.TransferId = Guid.NewGuid();
            if (actionType==ActionType.AddJobPosition)
            {
                jobPositionService.AddJobPosition((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), jobPosition);
            }
            else if (actionType == ActionType.ModifyJobPosition)
            {
                jobPositionService.UpdateJobPosition((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), jobPosition);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateJobPositionListArgs());
            OnRequestClose();
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        } 

        #endregion

    }
}

