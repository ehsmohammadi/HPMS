using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System;
using System.Collections.Generic;


namespace MITD.PMS.Presentation.Logic
{
    public class UnitIndexCustomFieldManageVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUnitIndexServiceWrapper unitIndexService;
        private readonly ICustomFieldServiceWrapper customFieldService;
        private ActionType actionType;

        #endregion

        #region Properties

        private UnitIndexDTO unitIndex ;
        public  UnitIndexDTO UnitIndex
        {
            get { return unitIndex; }
            set { this.SetField(vm => vm.UnitIndex, ref unitIndex, value); }
        }

        private List<AbstractCustomFieldDescriptionDTO> unitIndexCustomFieldDescriptionList;
        public List<AbstractCustomFieldDescriptionDTO> UnitIndexCustomFieldDescriptionList
        {
            get { return unitIndexCustomFieldDescriptionList; }
            set { this.SetField(vm => vm.UnitIndexCustomFieldDescriptionList, ref unitIndexCustomFieldDescriptionList, value); }
        }

       
        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel("ذخیره", new DelegateCommand(save));
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

        public UnitIndexCustomFieldManageVM()
        {
            BasicInfoAppLocalizedResources=new BasicInfoAppLocalizedResources();
            init();
            UnitIndex = new UnitIndexDTO { Name = "شغل یک" };
        }


        public UnitIndexCustomFieldManageVM(IPMSController appController, 
            IUnitIndexServiceWrapper unitIndexService,
            ICustomFieldServiceWrapper customFieldService,
            IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            this.appController = appController;
            this.unitIndexService = unitIndexService;
            this.customFieldService = customFieldService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            init();

        } 

        #endregion

        #region Methods

        private void init()
        {
            UnitIndex = new UnitIndexDTO();
            DisplayName = BasicInfoAppLocalizedResources.UnitIndexCustomFieldManageViewTitle;
            
        }


        public void Load(UnitIndexDTO unitIndexParam, ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            UnitIndex = unitIndexParam;
            ShowBusyIndicator();
            customFieldService.GetAllCustomFieldsDescription( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {

                    if (exp == null)
                    {
                        UnitIndexCustomFieldDescriptionList = res;
                        if (actionType == ActionType.ManageUnitIndexCustomFields)
                            setCurrentUnitCustomFields();
                        HideBusyIndicator();
                    }
                    else
                    {
                        HideBusyIndicator();
                        appController.HandleException(exp);
                    }
                }),"UnitIndex");

        }



        private void setCurrentUnitCustomFields()
        {
            unitIndexCustomFieldDescriptionList.Where(allFields => unitIndex.CustomFields.Select(f => f.Id).Contains(allFields.Id))
                                         .ToList()
                                         .ForEach(field => field.IsChecked = true);
        }

        private void save()
        {
            var selectedFields = UnitIndexCustomFieldDescriptionList.Where(f => f.IsChecked).ToList();
            appController.Publish(new UpdateUnitIndexCustomFieldListArgs(selectedFields));
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

