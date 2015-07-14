using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic.Wrapper.PeriodManagement.UnitIndex;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class UnitIndexInPeriodVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUnitIndexServiceWrapper unitIndexService;
        private readonly IUnitInPeriodServiceWrapper unitInPeriodService;
        private readonly IUnitIndexInPeriodServiceWrapper unitIndexInPeriodService;
        private ActionType actionType;

        #endregion

        #region Properties & BackField


        private UnitIndexInPeriodDTO unitIndexInPeriod;
        public UnitIndexInPeriodDTO UnitIndexInPeriod
        {
            get { return unitIndexInPeriod; }
            set { this.SetField(vm => vm.UnitIndexInPeriod, ref unitIndexInPeriod, value);}
        }

        private UnitIndexDTO selectedUnitIndex;
        public UnitIndexDTO SelectedUnitIndex
        {
            get { return selectedUnitIndex; }
            set
            { 
                this.SetField(vm => vm.SelectedUnitIndex, ref selectedUnitIndex, value);
                if (selectedUnitIndex != null )
                    setUnitIndexCustomFields();
            }
        }

        private List<UnitIndexDTO> unitIndexList = new List<UnitIndexDTO>();
        public List<UnitIndexDTO> UnitIndexList
        {
            get { return unitIndexList; }
            set { this.SetField(vm => vm.UnitIndexList, ref unitIndexList, value); }
        }


        private List<UnitInPeriodDTO> unitInPeriodList;
        public List<UnitInPeriodDTO> UnitInPeriodList
        {
            get { return unitInPeriodList; }
            set { this.SetField(vm => vm.UnitInPeriodList, ref unitInPeriodList, value); }
        }

        private bool unitIndexIsReadOnly;
        public bool UnitIndexIsReadOnly
        {
            get { return unitIndexIsReadOnly; }
            set { this.SetField(vm => vm.UnitIndexIsReadOnly, ref unitIndexIsReadOnly, value); }
        }

        private List<AbstractCustomFieldDescriptionDTO> abstractCustomFields;
        public List<AbstractCustomFieldDescriptionDTO> AbstractCustomFields
        {
            get { return abstractCustomFields; }
            set { this.SetField(vm => vm.AbstractCustomFields, ref abstractCustomFields, value); }
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

        public UnitIndexInPeriodVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public UnitIndexInPeriodVM(IUnitIndexInPeriodServiceWrapper unitIndexInPeriodService, 
                                   IPMSController appController,
                                   IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources,
                                   IUnitIndexServiceWrapper unitIndexService,
                                   IUnitInPeriodServiceWrapper unitInPeriodService )
        {
           
            this.unitIndexInPeriodService = unitIndexInPeriodService;
            this.appController = appController;
            this.unitIndexService = unitIndexService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();
        } 

        

        #endregion

        #region Methods

        private void init()
        {
            UnitIndexInPeriod = new UnitIndexInPeriodDTO();
            DisplayName = PeriodMgtAppLocalizedResources.UnitIndexInPeriodViewTitle;
            UnitIndexIsReadOnly = true;
        }

        public void Load(UnitIndexInPeriodDTO unitIndexInPeriodParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            UnitIndexInPeriod = unitIndexInPeriodParam;

            if (actionType == ActionType.ModifyUnitIndexInPeriod)
            {
                UnitIndexIsReadOnly = false;
                ShowBusyIndicator("در حال دریافت اطلاعات...");
                unitIndexService.GetUnitIndex((res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp == null)
                        {
                            UnitIndexList = new List<UnitIndexDTO>() { res };
                            SelectedUnitIndex = res;
                        }
                        else
                            appController.HandleException(exp);

                    }), UnitIndexInPeriod.UnitIndexId);
            }
            else
            {
                preLoad();
            }
        }

        private void preLoad()
        {
            unitIndexService.GetAllUnitIndex((res,exp) =>  appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                    {
                        setAcceptableUnitIndexList(res);
                    }
                    else
                    {
                        appController.HandleException(exp);
                    }
                }));
        }

      

        private void setAcceptableUnitIndexList(IList<UnitIndexDTO> allUnitIndex)
        {
            unitIndexInPeriodService.GetPeriodAbstractIndexes((res,exp)=>  appController.BeginInvokeOnDispatcher(() =>
                {

                    var unitIndexInPeriodList = res.OfType<UnitIndexInPeriodDTOWithActions>().ToList();
                    var jInPeriodIdList = unitIndexInPeriodList.Select(i => i.UnitIndexId).ToList();
                    UnitIndexList =  allUnitIndex.Where(all => !jInPeriodIdList.Contains(all.Id)).ToList();
                }), UnitIndexInPeriod.PeriodId);
        }

        private void setUnitIndexCustomFields()
        {
            if (selectedUnitIndex.CustomFields == null || selectedUnitIndex.CustomFields.Count == 0) // custom field not set
            {
                unitIndexService.GetUnitIndex((res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                            SelectedUnitIndex = res;
                        else
                            appController.HandleException(exp);
                        
                    }), SelectedUnitIndex.Id);
            }

            checkUnitCustomFields();
           
        }

        private void checkUnitCustomFields()
        {
            AbstractCustomFields = selectedUnitIndex.CustomFields.Select(c => new AbstractCustomFieldDescriptionDTO() { Id = c.Id, Name = c.Name, Value = "" }).ToList();

            AbstractCustomFields.Where(allFields => UnitIndexInPeriod.CustomFields.Select(f => f.Id)
                .Contains(allFields.Id)).ToList()
           .ForEach(field =>
           {
               field.IsChecked = true;
               field.Value = UnitIndexInPeriod.CustomFields.Single(f => f.Id == field.Id).Value;
           });

           
        }

        private void save()
        {
            //if (!unitIndexInPeriod.Validate()) return;

            ShowBusyIndicator();
            UnitIndexInPeriod.UnitIndexId = SelectedUnitIndex.Id;
            UnitIndexInPeriod.Name = selectedUnitIndex.Name;
            unitIndexInPeriod.DictionaryName = selectedUnitIndex.DictionaryName;
            UnitIndexInPeriod.CustomFields = AbstractCustomFields.Where(a => a.IsChecked).ToList();//.ToDictionary(a => a, a => a.Value);
            
            if (actionType==ActionType.AddUnitIndexInPeriod)
            {
                unitIndexInPeriodService.AddUnitIndexInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), UnitIndexInPeriod);
            }
            else if (actionType == ActionType.ModifyUnitIndexInPeriod)
            {
                unitIndexInPeriodService.UpdateUnitIndexInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), unitIndexInPeriod);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateUnitIndexInPeriodTreeArgs());
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


