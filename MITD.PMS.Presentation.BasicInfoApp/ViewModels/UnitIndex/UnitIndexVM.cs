using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class UnitIndexVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateUnitIndexCustomFieldListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUnitIndexServiceWrapper unitIndexService;
        private readonly ICustomFieldServiceWrapper customFieldService;
        private ActionType actionType;

        #endregion

        #region Properties & BackField

        private IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources;
        public IBasicInfoAppLocalizedResources BasicInfoAppLocalizedResources
        {
            get { return basicInfoAppLocalizedResources; }
            set { this.SetField(vm => vm.BasicInfoAppLocalizedResources, ref basicInfoAppLocalizedResources, value); }
        }

        private UnitIndexDTO unitIndex;
        public UnitIndexDTO UnitIndex
        {
            get { return unitIndex; }
            set { this.SetField(vm => vm.UnitIndex, ref unitIndex, value); }
        }

        private CommandViewModel manageUnitFieldsCommand;
        public CommandViewModel ManageUnitFieldsCommand
        {
            get
            {
                if (manageUnitFieldsCommand == null)
                    manageUnitFieldsCommand = CommandHelper.GetControlCommands(this, appController, (int)ActionType.ManageUnitIndexCustomFields);
                return manageUnitFieldsCommand;
            }
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

        public UnitIndexVM()
        {

            UnitIndex = new UnitIndexDTO { Name = "شاخص یک", DictionaryName="UnitIndex1" };
        }

        public UnitIndexVM( IUnitIndexServiceWrapper unitIndexService, 
                           ICustomFieldServiceWrapper customFieldService,
                           IPMSController appController,
                           IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
           
            this.unitIndexService = unitIndexService;
            this.customFieldService = customFieldService;
            this.appController = appController;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            UnitIndex = new UnitIndexDTO();
            DisplayName = BasicInfoAppLocalizedResources.UnitIndexViewTitle;
        } 

        #endregion

        #region Methods

        public void Load(UnitIndexDTO unitIndexParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            UnitIndex = unitIndexParam;
        }

       
        private void save()
        {
            if (!unitIndex.Validate()) return;

            ShowBusyIndicator();
            if (actionType==ActionType.AddUnitIndex)
            {
                unitIndexService.AddUnitIndex((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), unitIndex);
            }
            else if (actionType == ActionType.ModifyUnitIndex)
            {
                unitIndexService.UpdateUnitIndex((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), unitIndex);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateUnitIndexTreeArgs());
            OnRequestClose();
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdateUnitIndexCustomFieldListArgs eventData)
        {
            UnitIndex.CustomFields = new List<CustomFieldDTO>();
            var fieldIdList = eventData.UnitIndexCustomFieldDescriptionList.Select(f => f.Id).ToList();
            customFieldService.GetAllCustomFields( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                        UnitIndex.CustomFields = res.Where(r => fieldIdList.Contains(r.Id)).ToList();
                else
                    appController.HandleException(exp);
            }),"UnitIndex");
        }

        #endregion
    }
}

