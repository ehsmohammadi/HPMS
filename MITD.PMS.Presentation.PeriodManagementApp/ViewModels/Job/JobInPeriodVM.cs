using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class JobInPeriodVM : WorkspaceViewModel, IEventHandler<UpdateJobInPeriodCustomFieldListArgs>,IEventHandler<UpdateJobInPeriodJobIndexListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IPeriodController periodController;     
        private readonly IJobInPeriodServiceWrapper jobInPeriodService;
        private readonly IJobServiceWrapper jobService;
        private readonly ICustomFieldServiceWrapper customFieldService;
        private readonly IPeriodServiceWrapper periodService;
        private ActionType actionType;
    
        #endregion

        #region Properties & Back Fields

        private List<JobInPeriodDTO> jobs;
        public List<JobInPeriodDTO> Jobs
        {
            get { return jobs; }
            set { this.SetField(vm => vm.Jobs, ref jobs, value); }
        }

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(vm => vm.Period, ref period, value); }
        }

        private JobInPeriodDTO selectedJobInPeriod;
        public JobInPeriodDTO SelectedJobInPeriod
        {
            get { return selectedJobInPeriod; }
            set { this.SetField(vm => vm.SelectedJobInPeriod, ref selectedJobInPeriod, value); }
        }


        private CommandViewModel addFields;
        public CommandViewModel AddFields
        {
            get
            {
                if (addFields == null )
                {
                    addFields = new CommandViewModel("مدیریت فیلدها شغل",
                                                     //new DelegateCommand(()=>appController.PMSActions.Single(a=>a.ActionCode==ActionType.AddJobInPrdField).DoAction(JobInPeriodAssign)));
                                                     new DelegateCommand(
                                                         () =>
                                                         periodController.ShowJobInPeriodCustomFieldManageView(
                                                             Period.Id, SelectedJobInPeriod,
                                                             ActionType.ModifyJobInPeriod)));
                }
                return addFields;
            }
        }


        private CommandViewModel addJobIndices;
        public CommandViewModel AddJobIndices
        {
            get
            {
                if (addJobIndices == null )
                {
                    addJobIndices = new CommandViewModel("مدیریت شاخص های شغل",
                        //new DelegateCommand(()=>appController.PMSActions.Single(a=>a.ActionCode==ActionType.AddJobInPrdField).DoAction(JobInPeriodAssign)));
                                                     new DelegateCommand(
                                                         () =>
                                                         periodController.ShowJobInPeriodJobIndicesManageView(
                                                             Period.Id, SelectedJobInPeriod,
                                                             ActionType.ModifyJobInPeriod)));
                }
                return addJobIndices;
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

        private ReadOnlyCollection<DataGridCommandViewModel> jobInPrdFieldCommands;
        public ReadOnlyCollection<DataGridCommandViewModel> JobInPrdFieldCommands
        {
            get
            {
                if (jobInPrdFieldCommands == null)
                {
                    var cmds = createCommands();
                    jobInPrdFieldCommands = new ReadOnlyCollection<DataGridCommandViewModel>(cmds);
                }
                return jobInPrdFieldCommands;
            }
            private set
            {
                this.SetField(p => p.JobInPrdFieldCommands, ref jobInPrdFieldCommands, value);
            }

        }

        #endregion

        #region Constructors

        public JobInPeriodVM()
        {
            //Period = new Period { Name = "دوره اول", StartDate = DateTime.Now, EndDate = DateTime.Now };
        }
        public JobInPeriodVM(IPMSController appController,
                             IPeriodController periodController,
                             IJobInPeriodServiceWrapper jobInPeriodService,
                             IJobServiceWrapper jobService,
                             ICustomFieldServiceWrapper customFieldService,
                             IPeriodServiceWrapper periodService
            )
        {
            this.appController = appController;
            this.periodController = periodController;
            this.jobInPeriodService = jobInPeriodService;
            this.jobService = jobService;
            this.customFieldService = customFieldService;
            this.periodService = periodService;
            DisplayName = "مدیریت شغل در دوره ";
        } 

        #endregion

        #region Methods

        private List<DataGridCommandViewModel> createCommands()
        {
            var filterCommand = new List<DataGridCommandViewModel>();
            //appController.PMSActions.Where(a => SelectedJobInPrdField.ActionCodes.Contains((int)a.ActionCode)).ForEach(
            //    action => filterCommand.Add(new DataGridCommandViewModel
            //    {

            //        CommandViewModel = new CommandViewModel(action.ActionName,
            //                                                new DelegateCommand(
            //                                                    () => action.DoAction(SelectedJobInPrdField),
            //                                                    () => true)),
            //        Icon = action.ActionIcon
            //    }));

            return filterCommand;
        }

        public void Load(long periodId,long? jobId,ActionType actionTypeParam)
        {
            
            actionType = actionTypeParam;
            preLoad(periodId,jobId);
            
            if (jobId.HasValue) // modify job
            {
                Jobs = new List<JobInPeriodDTO>();
                ShowBusyIndicator("در حال دریافت اطلاعات...");
                jobInPeriodService.GetJobInPeriod((res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp == null)
                        {  
                            Jobs.Add(res);
                            SelectedJobInPeriod = res;
                        }
                    }),periodId,jobId.Value);
            }
            else // add new job => action is  ActionType.AddJobInPrdField
            {
                ShowBusyIndicator();
                jobInPeriodService.GetAllJobInPeriod((jobInPeriodListRes, exp) => appController.BeginInvokeOnDispatcher(() => 
                    {
                        if (exp == null)
                        {
                            jobService.GetAllJobs((jobsRes, jobsExp) => appController.BeginInvokeOnDispatcher(() => 
                                {
                                    HideBusyIndicator();
                                   
                                    if (jobsExp == null)
                                    {
                                        var jList = jobsRes.Where(r => !jobInPeriodListRes.Select(jip => jip.JobId).Contains(r.Id)).ToList();
                                        Jobs = jList.Select(
                                            j => new JobInPeriodDTO() { Name = j.Name, JobId = j.Id, CustomFields = new List<CustomFieldDTO>() }).ToList();

                                    }else
                                        appController.HandleException(jobsExp);
                                        
                                }));
                            
                        }
                        else
                            appController.HandleException(exp);
                    }),periodId);
                 
              
            }
        }

        private void preLoad(long periodId,long? jobId)
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            periodService.GetPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                        Period = res;

                }),periodId);

            
            if (!jobId.HasValue)
                return;
            
            jobInPeriodService.GetJobInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                        SelectedJobInPeriod = res;
                }),periodId,jobId.Value);
        }

        private void save()
        {
            if (SelectedJobInPeriod ==null || !SelectedJobInPeriod.Validate()) 
                return;

            ShowBusyIndicator();
            if (actionType==ActionType.AddJobInPeriod)
            {
                jobInPeriodService.AddJobInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }),Period.Id, SelectedJobInPeriod);
            }
            else if (actionType == ActionType.ModifyJobInPeriod)
            {
                jobInPeriodService.UpdateJobInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), Period.Id, SelectedJobInPeriod);
            }

        }
        
        private void finalizeAction()
        {
            appController.Publish(new UpdateJobInPeriodListArgs());
            OnRequestClose();
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            //if (propertyName == "SelectedJobInPrdField" && JobInPeriod.JobInPrdFields.Count > 0)
            //{
            //    JobInPrdFieldCommands = new ReadOnlyCollection<DataGridCommandViewModel>(createCommands());
            //    if (View != null)
            //        ((JobInPeriodView)View).CreateContextMenu(JobInPrdFieldCommands);
            //}
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdateJobInPeriodCustomFieldListArgs eventData)
        {
            if (Period.Id != eventData.PeriodId || SelectedJobInPeriod.JobId != eventData.JobId)
                return;

            SelectedJobInPeriod.CustomFields = new List<CustomFieldDTO>();
            var fieldIdList = eventData.JobInPeriodCustomFieldDescriptionList.Select(f => f.Id).ToList();

            customFieldService.GetAllCustomFields((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                    SelectedJobInPeriod.CustomFields = res.Where(f => fieldIdList.Contains(f.Id)).ToList();
                else
                    appController.HandleException(exp);
            }), "Job");

        }

        

        
        #endregion

        public void Handle(UpdateJobInPeriodJobIndexListArgs eventData)
        {
            if (Period.Id != eventData.PeriodId || SelectedJobInPeriod.JobId != eventData.JobId)
                return;

            SelectedJobInPeriod.JobIndices = eventData.JobInPeriodJobIndices;

            //SelectedJobInPeriod.CustomFields = new List<CustomFieldDTO>();
            //var fieldIdList = eventData.JobInPeriodCustomFieldDescriptionList.Select(f => f.Id).ToList();

            //customFieldService.GetAllCustomFields((res, exp) =>
            //{
            //    if (exp == null)
            //    {
            //        SelectedJobInPeriod.CustomFields = new List<CustomFieldDTO>(res
            //             .Where(f => fieldIdList.Contains(f.Id))
            //             .Select(r => new CustomFieldDTO() { Id = r.Id, Name = r.Name, DictionaryName = r.DictionaryName, MinValue = r.MinValue, MaxValue = r.MaxValue, TypeId = r.TypeName }));
            //        int i = 0;
            //    }

            //    else
            //        appController.HandleException(exp);
            //}, "Job");
        }
    }
    
}
