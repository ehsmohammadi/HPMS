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
    public sealed class IndexGroupInPeriodVM : WorkspaceViewModel,IEventHandler<UpdateIndexGroupInPeriodArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IPeriodController periodController;
        private CommandViewModel saveCommand;
       // private readonly IIndexInPeriodServiceWrapper indexInPeriodService;
        private readonly IJobIndexServiceWrapper IndexGroupService;
        private ActionEnum actionType;
        private CommandViewModel cancelCommand;
       // private JobIndexGroupInPeriod indexInPeriod;
        //private ObservableCollection<JobIndexGroupDTO> indexGroups;
        private CommandViewModel addFields;
       // private JobIndexGroupInPrdField selectedIndexGroupInPrdField;
        private ReadOnlyCollection<DataGridCommandViewModel> indexInPrdFieldCommands;

        #endregion

        #region Properties

        //public ObservableCollection<JobIndexGroupDTO> IndexGroupes
        //{
        //    get { return indexGroups; }
        //    set { this.SetField(vm => vm.IndexGroupes, ref indexGroups, value); }
        //}

        //public JobIndexGroupInPrdField SelectedIndexGroupInPrdField
        //{
        //    get { return selectedIndexGroupInPrdField; }
        //    set { this.SetField(vm => vm.SelectedIndexGroupInPrdField, ref selectedIndexGroupInPrdField, value); }
        //}

        //public JobIndexGroupInPeriod IndexGroupInPeriod
        //{
        //    get { return indexInPeriod; }
        //    set { this.SetField(vm => vm.IndexGroupInPeriod, ref indexInPeriod, value); }
        //}

        public CommandViewModel AddField
        {
            get
            {
                if (addFields == null)
                {
                    //addFields = new CommandViewModel("اضافه کردن فیلد", new DelegateCommand(()=>appController.PMSActions.Single(a=>a.ActionCode==ActionEnum.AddIndexGroupInPrdField).DoAction(IndexGroupInPeriod)));
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

        public ReadOnlyCollection<DataGridCommandViewModel> IndexGroupInPrdFieldCommands
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
                this.SetField(p => p.IndexGroupInPrdFieldCommands, ref indexInPrdFieldCommands, value);
            }

        }
        #endregion

        #region Constructors

        public IndexGroupInPeriodVM()
        {
        }

        public IndexGroupInPeriodVM(IPMSController appController,
                             IPeriodController periodController,
                             //IIndexInPeriodServiceWrapper indexInPeriodService, 
                             IJobIndexServiceWrapper indexService)
        {
            this.appController = appController;
            this.periodController = periodController;
            //this.indexInPeriodService = indexInPeriodService;
            this.IndexGroupService = indexService;
            DisplayName = "گروه شاخص در دوره ";
        } 

        #endregion

        #region Methods

        private List<DataGridCommandViewModel> createCommands()
        {
            var filterCommand = new List<DataGridCommandViewModel>();
            //appController.PMSActions.Where(a => SelectedIndexGroupInPrdField.ActionCodes.Contains((int)a.ActionCode)).ForEach(
            //    action => filterCommand.Add(new DataGridCommandViewModel
            //    {

            //        CommandViewModel = new CommandViewModel(action.ActionName,
            //                                                new DelegateCommand(
            //                                                    () => action.DoAction(SelectedIndexGroupInPrdField),
            //                                                    () => true)),
            //        Icon = action.ActionIcon
            //    }));

            return filterCommand;
        }

        //public void Load(JobIndexGroupInPeriod indexGroupInPeriodParam,ActionEnum actionTypeParam)
        //{
        //    actionType = actionTypeParam;
        //    IndexGroupInPeriod = indexGroupInPeriodParam;
        //    preLoad();
        //    if (actionType == ActionEnum.ModifyIndexGroupInPrdField)
        //    {
        //        ShowBusyIndicator();
        //        indexInPeriodService.GetIndexGroupInPeriod((res, exp) =>
        //            {
        //                HideBusyIndicator();
        //                if (exp == null)
        //                    IndexGroupInPeriod = res;
        //                else
        //                    appController.HandleException(exp);
        //            },
        //                                indexGroupInPeriodParam.Id);
        //    }
        //}

        private void preLoad()
        {
            //indexInPeriodService.GetAllIndexGroup((res, exp) =>
            //        {
            //            HideBusyIndicator();
            //            if (exp == null)
            //                IndexGroupes = res;
            //            else
            //                appController.HandleException(exp);
            //        });
        }

        private void save()
        {
            //if (!IndexGroupInPeriod.Validate()) return;

            //ShowBusyIndicator();
            //if (actionType==ActionEnum.AddIndexGroupInPeriod)
            //{
            //    indexInPeriodService.AddIndexGroupInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //        {
            //            HideBusyIndicator();
            //            if (exp != null)
            //                appController.HandleException(exp);
            //            else
            //                FinalizeAction();
            //        }), IndexGroupInPeriod);
            //}
            //else if (actionType == ActionEnum.ModifyIndexGroupInPeriod)
            //{
            //    indexInPeriodService.UpdateIndexGroupInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //        {
            //            HideBusyIndicator();
            //            if (exp != null)
            //                appController.HandleException(exp);
            //            else
            //                FinalizeAction();
            //        }), IndexGroupInPeriod);
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
            //if (propertyName == "SelectedIndexGroupInPrdField" && IndexGroupInPeriod.Fields.Count > 0)
            //{
            //    IndexGroupInPrdFieldCommands = new ReadOnlyCollection<DataGridCommandViewModel>(createCommands());
            //    if (View != null)
            //        ((IndexGroupInPeriodView)View).CreateContextMenu(IndexGroupInPrdFieldCommands);
            //}
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        } 

        #endregion

        public void Handle(UpdateIndexGroupInPeriodArgs eventData)
        {
           //// GetIndexGroupInPrdFields();
           // var indexInPeriodField= eventData.IndexGroupInPrdField;
           // if (eventData.ActionType==ActionEnum.AddIndexGroupInPrdField)
           // {
           //     indexInPeriodField.ActionCodes=new List<int>{59,60};
           //     if (IndexGroupInPeriod.Fields != null) IndexGroupInPeriod.Fields.Add(indexInPeriodField);
           // }
           // else if (eventData.ActionType == ActionEnum.ModifyIndexGroupInPrdField)
           // {
           //     if (IndexGroupInPeriod.Fields != null)
           //     {
           //         var element = IndexGroupInPeriod.Fields.FirstOrDefault(p => p.Id == indexInPeriodField.Id);
           //         IndexGroupInPeriod.Fields.Remove(element);
           //         IndexGroupInPeriod.Fields.Add(indexInPeriodField);
           //     }
             
           // }
           // else if (eventData.ActionType == ActionEnum.DeleteIndexGroupInPrdField)
           // {
           //     if (IndexGroupInPeriod.Fields != null)
           //     {
           //         var element = IndexGroupInPeriod.Fields.FirstOrDefault(p => p.Id == indexInPeriodField.Id);
           //         IndexGroupInPeriod.Fields.Remove(element);
           //     }
           // }

        }
    }
    
}
