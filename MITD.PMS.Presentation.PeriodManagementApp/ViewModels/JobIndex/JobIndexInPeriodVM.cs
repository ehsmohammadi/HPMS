using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class JobIndexInPeriodVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IJobIndexServiceWrapper jobIndexService;
        private readonly IJobInPeriodServiceWrapper jobInPeriodService;
        private readonly IJobIndexInPeriodServiceWrapper jobIndexInPeriodService;
        private ActionType actionType;

        #endregion

        #region Properties & BackField


        private JobIndexInPeriodDTO jobIndexInPeriod;
        public JobIndexInPeriodDTO JobIndexInPeriod
        {
            get { return jobIndexInPeriod; }
            set { this.SetField(vm => vm.JobIndexInPeriod, ref jobIndexInPeriod, value);}
        }

        private JobIndexDTO selectedJobIndex;
        public JobIndexDTO SelectedJobIndex
        {
            get { return selectedJobIndex; }
            set
            { 
                this.SetField(vm => vm.SelectedJobIndex, ref selectedJobIndex, value);
                if (selectedJobIndex != null )
                    setJobIndexCustomFields();
            }
        }

        private List<JobIndexDTO> jobIndexList = new List<JobIndexDTO>();
        public List<JobIndexDTO> JobIndexList
        {
            get { return jobIndexList; }
            set { this.SetField(vm => vm.JobIndexList, ref jobIndexList, value); }
        }


        private List<JobInPeriodDTO> jobInPeriodList;
        public List<JobInPeriodDTO> JobInPeriodList
        {
            get { return jobInPeriodList; }
            set { this.SetField(vm => vm.JobInPeriodList, ref jobInPeriodList, value); }
        }

        private bool jobIndexIsReadOnly;
        public bool JobIndexIsReadOnly
        {
            get { return jobIndexIsReadOnly; }
            set { this.SetField(vm => vm.JobIndexIsReadOnly, ref jobIndexIsReadOnly, value); }
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

        public JobIndexInPeriodVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public JobIndexInPeriodVM( IJobIndexInPeriodServiceWrapper jobIndexInPeriodService, 
                                   IPMSController appController,
                                   IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources,
                                   IJobIndexServiceWrapper jobIndexService,
                                   IJobInPeriodServiceWrapper jobInPeriodService )
        {
           
            this.jobIndexInPeriodService = jobIndexInPeriodService;
            this.appController = appController;
            this.jobIndexService = jobIndexService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();
        } 

        

        #endregion

        #region Methods

        private void init()
        {
            JobIndexInPeriod = new JobIndexInPeriodDTO();
            DisplayName = PeriodMgtAppLocalizedResources.JobIndexInPeriodViewTitle;
            JobIndexIsReadOnly = true;
        }

        public void Load(JobIndexInPeriodDTO jobIndexInPeriodParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            JobIndexInPeriod = jobIndexInPeriodParam;

            if (actionType == ActionType.ModifyJobIndexInPeriod)
            {
                JobIndexIsReadOnly = false;
                ShowBusyIndicator("در حال دریافت اطلاعات...");
                jobIndexService.GetJobIndex((res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp == null)
                        {
                            JobIndexList = new List<JobIndexDTO>() { res };
                            SelectedJobIndex = res;
                        }
                        else
                            appController.HandleException(exp);

                    }), JobIndexInPeriod.JobIndexId);
            }
            else
            {
                preLoad();
            }
        }

        private void preLoad()
        {
            jobIndexService.GetAllJobIndex((res,exp) =>  appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                    {
                        setAcceptableJobIndexList(res);
                    }
                    else
                    {
                        appController.HandleException(exp);
                    }
                }));
        }

      

        private void setAcceptableJobIndexList(IList<JobIndexDTO> allJobIndex)
        {
            jobIndexInPeriodService.GetPeriodAbstractIndexes((res,exp)=>  appController.BeginInvokeOnDispatcher(() =>
                {

                    var jobIndexInPeriodList = res.OfType<JobIndexInPeriodDTOWithActions>().ToList();
                    var jInPeriodIdList = jobIndexInPeriodList.Select(i => i.JobIndexId).ToList();
                    JobIndexList =  allJobIndex.Where(all => !jInPeriodIdList.Contains(all.Id)).ToList();
                }), JobIndexInPeriod.PeriodId);
        }

        private void setJobIndexCustomFields()
        {
            if (selectedJobIndex.CustomFields == null || selectedJobIndex.CustomFields.Count == 0) // custom field not set
            {
                jobIndexService.GetJobIndex((res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                            SelectedJobIndex = res;
                        else
                            appController.HandleException(exp);
                        
                    }), SelectedJobIndex.Id);
            }

            checkJobCustomFields();
           
        }

        private void checkJobCustomFields()
        {
            AbstractCustomFields = selectedJobIndex.CustomFields.Select(c => new AbstractCustomFieldDescriptionDTO() { Id = c.Id, Name = c.Name, Value = "" }).ToList();

            AbstractCustomFields.Where(allFields => JobIndexInPeriod.CustomFields.Select(f => f.Id)
                .Contains(allFields.Id)).ToList()
           .ForEach(field =>
           {
               field.IsChecked = true;
               field.Value = JobIndexInPeriod.CustomFields.Single(f => f.Id == field.Id).Value;
           });

           
        }

        private void save()
        {
            //if (!jobIndexInPeriod.Validate()) return;

            ShowBusyIndicator();
            JobIndexInPeriod.JobIndexId = SelectedJobIndex.Id;
            JobIndexInPeriod.Name = selectedJobIndex.Name;
            jobIndexInPeriod.DictionaryName = selectedJobIndex.DictionaryName;
            JobIndexInPeriod.CustomFields = AbstractCustomFields.Where(a => a.IsChecked).ToList();//.ToDictionary(a => a, a => a.Value);
            
            if (actionType==ActionType.AddJobIndexInPeriod)
            {
                jobIndexInPeriodService.AddJobIndexInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), JobIndexInPeriod);
            }
            else if (actionType == ActionType.ModifyJobIndexInPeriod)
            {
                jobIndexInPeriodService.UpdateJobIndexInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), jobIndexInPeriod);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateJobIndexInPeriodTreeArgs());
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


