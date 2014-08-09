using System;
using System.Collections.ObjectModel;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class IndexInPrdFieldVM : WorkspaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private CommandViewModel saveCommand;
        //private readonly IIndexInPeriodServiceWrapper indexInPeriodService;
        private ActionEnum actionType;
        private CommandViewModel cancelCommand;
        //private JobIndexInPeriod indexInPeriod;
        private CommandViewModel addFields;
        private CustomFieldDTO field;

        #endregion

        #region Properties

        public CustomFieldDTO Field
        {
            get { return field; }
            set { this.SetField(vm => vm.Field, ref field, value); }
        }

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

        public IndexInPrdFieldVM()
        {
            //Period = new Period { Name = "دوره اول", StartDate = DateTime.Now, EndDate = DateTime.Now };
        }
        public IndexInPrdFieldVM(IPMSController appController)
        {
            this.appController = appController;
            //this.indexInPeriodService = indexInPeriodService;
          //  IndexInPeriod = new IndexInPeriod();
            DisplayName = " مدیریت فیلدهای مرتبط با شاخص ";
        } 

        #endregion

        #region Methods

        public void Load(CustomFieldDTO FieldParam,ActionEnum actionTypeParam)
        {
            actionType = actionTypeParam;
            Field = FieldParam;
            if (actionType == ActionEnum.ModifyIndexInPrdField)
            {
                ShowBusyIndicator();
                //indexInPeriodService.GetIndexInPrdField((res, exp) =>
                //    {
                //        HideBusyIndicator();
                //        if (exp == null)
                //            //Field = res;
                //        //else
                //            appController.HandleException(exp);
                //    },
                //                        FieldParam.Id);
            }
        }


        private void preLoad()
        {
          
        }

        private void save()
        {
            if (!Field.Validate()) return;

            ShowBusyIndicator();
            //if (actionType == ActionEnum.AddIndexInPrdField)
            //{
            //    indexInPeriodService.AddIndexInPrdField((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //        {
            //            HideBusyIndicator();
            //            //if (exp != null)
            //            //    appController.HandleException(exp);
            //            //else
            //            //    FinalizeAction(res);
            //        }), (JobIndexInPrdField)Field);
            //}
            //else if (actionType == ActionEnum.ModifyIndexInPrdField)
            //{
            //    indexInPeriodService.UpdateIndexInPrdField((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //        {
            //            HideBusyIndicator();
            //            //if (exp != null)
            //            //    appController.HandleException(exp);
            //            //else
            //            //    FinalizeAction(res);
            //        }), (JobIndexInPrdField)Field);
            //}
            //if (actionType == ActionEnum.AddIndexGroupInPrdField)
            //{
            //    indexInPeriodService.AddIndexGroupInPrdField((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //    {
            //        HideBusyIndicator();
            //        if (exp != null)
            //            appController.HandleException(exp);
            //        else
            //            FinalizeAction(res);
            //    }), (JobIndexGroupInPrdField)Field);
            //}
            //else if (actionType == ActionEnum.ModifyIndexGroupInPrdField)
            //{
            //    indexInPeriodService.UpdateIndexGroupInPrdField((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //    {
            //        HideBusyIndicator();
            //        if (exp != null)
            //            appController.HandleException(exp);
            //        else
            //            FinalizeAction(res);
            //    }), (JobIndexGroupInPrdField)Field);
            //}
        }
        
        private void FinalizeAction(CustomFieldDTO res)
        {
            //if (res is JobIndexInPrdField)
            //    appController.Publish(new UpdateIndexInPeriodArgs((JobIndexInPrdField)res, actionType));
            //if (res is JobIndexGroupInPrdField)
            //    appController.Publish(new UpdateIndexGroupInPeriodArgs((JobIndexGroupInPrdField)res, actionType));
            //OnRequestClose();
        }


        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        } 

        #endregion
    }
    
}
