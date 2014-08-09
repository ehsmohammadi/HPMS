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
    public class UnitVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUnitServiceWrapper unitService;
        private ActionType actionType;

        #endregion

        #region Properties

        private UnitDTO unit;
        public UnitDTO Unit
        {
            get { return unit; }
            set { this.SetField(vm => vm.Unit, ref unit, value); }
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

        public UnitVM()
        {
            Unit = new UnitDTO { Name = "پست سازمانی یک", DictionaryName="Unit1" };
        }
        public UnitVM(IPMSController appController, IUnitServiceWrapper unitService)
        {
            this.appController = appController;
            this.unitService = unitService;
            Unit = new UnitDTO();
            DisplayName = "واحد سازمانی ";
        } 

        #endregion

        #region Methods

        public void Load(UnitDTO unitParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            if (actionType == ActionType.ModifyUnit)
            {
                ShowBusyIndicator();
                unitService.GetUnit( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp == null)
                            Unit = res;
                        else
                            appController.HandleException(exp);
                    }),
                                        unitParam.Id);
            }
        }

       
        private void save()
        {
            if (!unit.Validate()) return;

            ShowBusyIndicator();
            if (actionType==ActionType.AddUnit)
            {
                unitService.AddUnit((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), unit);
            }
            else if (actionType == ActionType.ModifyUnit)
            {
                unitService.UpdateUnit((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), unit);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateUnitListArgs());
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

