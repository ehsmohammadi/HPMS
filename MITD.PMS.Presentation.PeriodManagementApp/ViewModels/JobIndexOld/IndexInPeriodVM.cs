using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Castle.Core.Internal;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class IndexInPeriodVM : WorkspaceViewModel,IEventHandler<UpdateIndexInPeriodArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IPeriodController periodController;
        private CommandViewModel saveCommand;
        //private readonly IIndexInPeriodServiceWrapper indexInPeriodService;
        private readonly IJobIndexServiceWrapper IndexService;
        private ActionEnum actionType;
        private CommandViewModel cancelCommand;
        //private JobIndexInPeriod indexInPeriod;
        private ObservableCollection<JobIndexDTO> indexes;
        private CommandViewModel addFields;
        //private JobIndexInPrdField selectedIndexInPrdField;
        private ReadOnlyCollection<DataGridCommandViewModel> indexInPrdFieldCommands;

        #endregion

        #region Properties

        public ObservableCollection<JobIndexDTO> Indexes
        {
            get { return indexes; }
            set { this.SetField(vm => vm.Indexes, ref indexes, value); }
        }

        //public JobIndexInPrdField SelectedIndexInPrdField
        //{
        //    get { return selectedIndexInPrdField; }
        //    set { this.SetField(vm => vm.SelectedIndexInPrdField, ref selectedIndexInPrdField, value); }
        //}

        //public JobIndexInPeriod IndexInPeriod
        //{
        //    get { return indexInPeriod; }
        //    set { this.SetField(vm => vm.IndexInPeriod, ref indexInPeriod, value); }
        //}

        public CommandViewModel AddFields
        {
            get
            {
                if (addFields == null)
                {
                    //addFields = new CommandViewModel("اضافه کردن فیلد", new DelegateCommand(()=>appController.PMSActions.Single(a=>a.ActionCode==ActionEnum.AddIndexInPrdField).DoAction(IndexInPeriod)));
                }
                return addFields;
            }
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

        public ReadOnlyCollection<DataGridCommandViewModel> IndexInPrdFieldCommands
        {
            get
            {
                if (indexInPrdFieldCommands == null)
                {
                    var cmds = createCommands();
                    indexInPrdFieldCommands = new ReadOnlyCollection<DataGridCommandViewModel>(cmds);
                }
                return indexInPrdFieldCommands;
            }
            private set
            {
                this.SetField(p => p.IndexInPrdFieldCommands, ref indexInPrdFieldCommands, value);
            }

        }
        #endregion

        #region Constructors

        public IndexInPeriodVM()
        {
        }

        public IndexInPeriodVM(IPMSController appController,
                             IPeriodController periodController,
                             //IIndexInPeriodServiceWrapper indexInPeriodService, 
                             IJobIndexServiceWrapper indexService)
        {
            this.appController = appController;
            this.periodController = periodController;
            //this.indexInPeriodService = indexInPeriodService;
            this.IndexService = indexService;
            DisplayName = " شاخص/گروه در دوره ";
        } 

        #endregion

        #region Methods

        private List<DataGridCommandViewModel> createCommands()
        {
            var filterCommand = new List<DataGridCommandViewModel>();
            //appController.PMSActions.Where(a => SelectedIndexInPrdField.ActionCodes.Contains((int)a.ActionCode)).ForEach(
            //    action => filterCommand.Add(new DataGridCommandViewModel
            //    {

            //        CommandViewModel = new CommandViewModel(action.ActionName,
            //                                                new DelegateCommand(
            //                                                    () => action.DoAction(SelectedIndexInPrdField),
            //                                                    () => true)),
            //        Icon = action.ActionIcon
            //    }));

            return filterCommand;
        }

        //public void Load(JobIndexInPeriod indexInPeriodParam, ActionEnum actionTypeParam)
        //{
        //    actionType = actionTypeParam;
        //    IndexInPeriod = indexInPeriodParam;
        //    preLoad();
        //    if (actionType == ActionEnum.ModifyIndexInPrdField)
        //    {
        //        ShowBusyIndicator();
        //        indexInPeriodService.GetIndexInPeriod((res, exp) =>
        //            {
        //                HideBusyIndicator();
        //                if (exp == null)
        //                    IndexInPeriod = res;
        //                else
        //                    appController.HandleException(exp);
        //            },
        //                                indexInPeriodParam.Id);
        //    }
        //}

        private void preLoad()
        {
            //indexInPeriodService.GetAllJobIndex((res, exp) =>
            //        {
            //            HideBusyIndicator();
            //            if (exp == null)
            //                Indexes = res;
            //            else
            //                appController.HandleException(exp);
            //        });
        }

        private void save()
        {
            //if (!IndexInPeriod.Validate()) return;

            //ShowBusyIndicator();
            //if (actionType==ActionEnum.AddIndexInPeriod)
            //{
            //    indexInPeriodService.AddIndexInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //        {
            //            HideBusyIndicator();
            //            if (exp != null)
            //                appController.HandleException(exp);
            //            else
            //                FinalizeAction();
            //        }), IndexInPeriod);
            //}
            //else if (actionType == ActionEnum.ModifyIndexInPeriod)
            //{
            //    indexInPeriodService.UpdateIndexInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //        {
            //            HideBusyIndicator();
            //            if (exp != null)
            //                appController.HandleException(exp);
            //            else
            //                FinalizeAction();
            //        }), IndexInPeriod);
            //}
        }
        
        private void FinalizeAction()
        {
            appController.Publish(new UpdateIndexTreeArgs());
            OnRequestClose();
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            //if (propertyName == "SelectedIndexInPrdField" && IndexInPeriod.Fields.Count > 0)
            //{
            //    IndexInPrdFieldCommands = new ReadOnlyCollection<DataGridCommandViewModel>(createCommands());
            //    if (View != null)
            //        ((IndexInPeriodView)View).CreateContextMenu(IndexInPrdFieldCommands);
            //}
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        } 

        #endregion

        public void Handle(UpdateIndexInPeriodArgs eventData)
        {
           // GetIndexInPrdFields();
            //var indexInPeriodField= eventData.IndexInPrdField;
            //if (eventData.actionType==ActionEnum.AddIndexInPrdField)
            //{
            //    indexInPeriodField.ActionCodes=new List<int>{59,60};
            //    if (IndexInPeriod.Fields != null) IndexInPeriod.Fields.Add(indexInPeriodField);
            //}
            //else if (eventData.actionType == ActionEnum.ModifyIndexInPrdField)
            //{
            //    if (IndexInPeriod.Fields != null)
            //    {
            //        var element = IndexInPeriod.Fields.FirstOrDefault(p => p.Id == indexInPeriodField.Id);
            //        IndexInPeriod.Fields.Remove(element);
            //        IndexInPeriod.Fields.Add(indexInPeriodField);
            //    }
             
            //}
            //else if (eventData.actionType == ActionEnum.DeleteIndexInPrdField)
            //{
            //    if (IndexInPeriod.Fields != null)
            //    {
            //        var element = IndexInPeriod.Fields.FirstOrDefault(p => p.Id == indexInPeriodField.Id);
            //        IndexInPeriod.Fields.Remove(element);
            //    }
            //}

        }
    }
    
}
