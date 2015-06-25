using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System.Collections.Generic;


namespace MITD.PMS.Presentation.Logic
{
    public class UnitCustomFieldManageVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUnitServiceWrapper unitService;
        private readonly ICustomFieldServiceWrapper customFieldService;
        private ActionType actionType;

        #endregion

        #region Properties

        private UnitDTO _unitDto ;
        public UnitDTO UnitDto
        {
            get { return _unitDto; }
            set { this.SetField(vm => vm.UnitDto, ref _unitDto, value); }
        }

        private List<AbstractCustomFieldDescriptionDTO> unitCustomFieldDescriptionList;
        public List<AbstractCustomFieldDescriptionDTO> UnitCustomFieldDescriptionList
        {
            get { return unitCustomFieldDescriptionList; }
            set { this.SetField(vm => vm.UnitCustomFieldDescriptionList, ref unitCustomFieldDescriptionList, value); }
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

        public UnitCustomFieldManageVM()
        {
            BasicInfoAppLocalizedResources=new BasicInfoAppLocalizedResources();
            init();
            UnitDto = new UnitDTO(); 
        }


        public UnitCustomFieldManageVM(IPMSController appController, 
            IUnitServiceWrapper unitService,
            IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources,
            ICustomFieldServiceWrapper customFieldService)
        {
            this.appController = appController;
            this.unitService = unitService;
            this.customFieldService = customFieldService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            init();

        } 

        #endregion

        #region Methods

        private void init()
        {
            UnitDto = new UnitDTO();
            DisplayName = BasicInfoAppLocalizedResources.UnitCustomFieldManageViewTitle;
            UnitCustomFieldDescriptionList=new List<AbstractCustomFieldDescriptionDTO>();
            
        }


        public void Load(UnitDTO unitParam, ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            UnitDto = unitParam;
            ShowBusyIndicator();
            customFieldService.GetAllCustomFieldsDescription( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {

                    if (exp == null)
                    {
                        UnitCustomFieldDescriptionList = res;
                        if (actionType == ActionType.ManageUnitCustomFields)
                            setCurrentUnitCustomFields();
                        HideBusyIndicator();
                    }
                    else
                    {
                        HideBusyIndicator();
                        appController.HandleException(exp);
                    }
                }),"Unit");

        }

        private void setCurrentUnitCustomFields()
        {
            unitCustomFieldDescriptionList.Where(allFields => UnitDto.CustomFields.Select(f => f.Id).Contains(allFields.Id))
                .ToList()
                .ForEach(field => field.IsChecked = true);
        }

        private void save()
        {
            var selectedFields = UnitCustomFieldDescriptionList.Where(f => f.IsChecked).ToList();
            appController.Publish(new UpdateUnitCustomFieldListArgs(selectedFields));
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

