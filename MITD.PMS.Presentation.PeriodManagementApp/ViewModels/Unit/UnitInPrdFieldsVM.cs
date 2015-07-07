using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;
 
namespace MITD.PMS.Presentation.Logic
{
    public sealed class UnitInPrdFieldsVM : WorkspaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUnitInPeriodServiceWrapper unitInPeriodService;
        private readonly IUnitServiceWrapper unitService;
        private readonly IPeriodServiceWrapper periodService;
        private ActionType actionType;

        #endregion

        #region Properties

        private UnitDTO unit;
        public UnitDTO Unit
        {
            get { return unit; }
            set { this.SetField(vm => vm.Unit, ref unit, value); }
        }

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(vm => vm.Period, ref period, value); }
        }


        private List<AbstractCustomFieldDescriptionDTO> unitCustomFieldDescriptionList = new List<AbstractCustomFieldDescriptionDTO>();
        public List<AbstractCustomFieldDescriptionDTO> UnitCustomFieldDescriptionList
        {
            get { return unitCustomFieldDescriptionList; }
            set { this.SetField(vm => vm.UnitCustomFieldDescriptionList, ref unitCustomFieldDescriptionList, value); }
        }


        //private UnitInPrdField unitInPrdField;
        //public UnitInPrdField UnitInPrdField
        //{
        //    get { return unitInPrdField; }
        //    set { this.SetField(vm => vm.UnitInPrdField, ref unitInPrdField, value); }
        //}

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
                    cancelCommand = new CommandViewModel("انصراف", new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        }

        #endregion

        #region Constructors

        public UnitInPrdFieldsVM()
        {
            //Period = new Period { Name = "دوره اول", StartDate = DateTime.Now, EndDate = DateTime.Now };
        }
        public UnitInPrdFieldsVM(IPMSController appController, 
            IUnitInPeriodServiceWrapper unitInPeriodService,
            IUnitServiceWrapper unitService,IPeriodServiceWrapper periodService)
        {
            this.appController = appController;
            this.unitInPeriodService = unitInPeriodService;
            this.unitService = unitService;
            this.periodService = periodService;
            DisplayName = " مدیریت فیلدهای مرتبط با واحد ";
        }

        #endregion

        #region Methods

        //public void Load(UnitInPrdField unitInPrdFieldParam, ActionType actionTypeParam)
        public void Load(long periodId,UnitInPeriodDTO unitInPeriod, ActionType actionTypeParam)
        {
            if (unitInPeriod == null)
                return;

            actionType = actionTypeParam;
            preLoad(periodId);
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            unitService.GetUnit((res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    Unit = res;
                    UnitCustomFieldDescriptionList = new List<AbstractCustomFieldDescriptionDTO>(Unit.CustomFields
                        .Select(f => new AbstractCustomFieldDescriptionDTO() { Name = f.Name, Id = f.Id }).ToList());

                    unitCustomFieldDescriptionList.Where(allFields => unitInPeriod.CustomFields.Select(f => f.Id).Contains(allFields.Id))
                                   .ToList()
                                   .ForEach(field => field.IsChecked = true);
                }
                else
                    appController.HandleException(exp);

            }), unitInPeriod.UnitId);
        }


        private void preLoad(long periodId)
        {
            ShowBusyIndicator();

            periodService.GetPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                        Period = res;
                    else
                        appController.HandleException(exp);
                }),periodId);

           
            
        }

        private void save()
        {
            var selectedFields = UnitCustomFieldDescriptionList.Where(f => f.IsChecked).ToList();
            if(period != null && Unit != null)
                appController.Publish(new UpdateUnitInPeriodCustomFieldListArgs(selectedFields,Period.Id,Unit.Id));
            OnRequestClose();
        }

        //private void FinalizeAction(UnitInPrdField res)
        //{
        //    //appController.Publish(new UpdateUnitInPeriodArgs(res, actionType));
        //    OnRequestClose();
        //}


        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion
    }

}
