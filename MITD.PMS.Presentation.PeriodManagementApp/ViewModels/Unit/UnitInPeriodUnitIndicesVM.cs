using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic.Wrapper.PeriodManagement.UnitIndex;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;
 
namespace MITD.PMS.Presentation.Logic
{
    public sealed class UnitInPeriodUnitIndicesVM : WorkspaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUnitInPeriodServiceWrapper unitInPeriodService;
        private readonly IUnitServiceWrapper unitService;
        private readonly IPeriodServiceWrapper periodService;
        private readonly IUnitIndexInPeriodServiceWrapper unitIndexInPeriodService;
        private ActionType actionType;

        #endregion

        #region Properties

        private UnitInPeriodDTO unitInPeriod;
        public UnitInPeriodDTO UnitInPeriod
        {
            get { return unitInPeriod; }
            set { this.SetField(vm => vm.UnitInPeriod, ref unitInPeriod, value); }
        }

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(vm => vm.Period, ref period, value); }
        }


        private ObservableCollection<UnitInPeriodUnitIndexDTO> unitIndexInPeriodList = new ObservableCollection<UnitInPeriodUnitIndexDTO>();
        public ObservableCollection<UnitInPeriodUnitIndexDTO> UnitIndexInPeriodList
        {
            get { return unitIndexInPeriodList; }
            set { this.SetField(vm => vm.UnitIndexInPeriodList, ref unitIndexInPeriodList, value); }
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

        public UnitInPeriodUnitIndicesVM()
        {
        }
        public UnitInPeriodUnitIndicesVM(IPMSController appController, 
            IUnitInPeriodServiceWrapper unitInPeriodService,
            IUnitServiceWrapper unitService,IPeriodServiceWrapper periodService,
            IUnitIndexInPeriodServiceWrapper unitIndexInPeriodService
            )
        {
            this.appController = appController;
            this.unitInPeriodService = unitInPeriodService;
            this.unitService = unitService;
            this.periodService = periodService;
            this.unitIndexInPeriodService = unitIndexInPeriodService;
            DisplayName = " مدیریت شاخص های مرتبط با شغل ";
        }

        #endregion

        #region Methods

        public void Load(long periodId,UnitInPeriodDTO unitInPeriodParam, ActionType actionTypeParam)
        {
            if (unitInPeriodParam == null)
                return;
            UnitInPeriod = unitInPeriodParam;
            actionType = actionTypeParam;
            preLoad(periodId);

            unitIndexInPeriodService.GetAllPeriodUnitIndexes((res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
            {

                if (exp == null)
                {
                    foreach (var unitIndex in res)
                    {
                        var unitInPeriodUnitIndex = UnitInPeriod.UnitIndices.SingleOrDefault(j => j.Id == unitIndex.Id);
                        if (unitInPeriodUnitIndex != null)
                        {
                            unitInPeriodUnitIndex.IsChecked = true;
                            UnitIndexInPeriodList.Add(unitInPeriodUnitIndex);
                        }                           
                        else
                        {
                            UnitIndexInPeriodList.Add(new UnitInPeriodUnitIndexDTO
                            {
                                Id=unitIndex.Id,
                                Name = unitIndex.Name,
                                IsInquireable = unitIndex.IsInquireable,
                                ShowforLowLevel = true,
                                ShowforSameLevel = true,
                                ShowforTopLevel = true
                            });
                        }
                    }
                    HideBusyIndicator();
                }
                else
                {
                    HideBusyIndicator();
                    appController.HandleException(exp);
                    
                }
                   

            }),periodId);
        }


        private void preLoad(long periodId)
        {
            ShowBusyIndicator();

            periodService.GetPeriod((res, exp) =>appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                        Period = res;
                    else
                        appController.HandleException(exp);
                }),periodId);

           
            
        }

        private void save()
        {
            var selectedUnitIndices = unitIndexInPeriodList.Where(f => f.IsChecked).ToList();
            if (period != null && UnitInPeriod != null)
                appController.Publish(new UpdateUnitInPeriodUnitIndexListArgs(selectedUnitIndices, Period.Id, UnitInPeriod.UnitId));
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
