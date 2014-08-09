using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ManageJobCustomFieldsService : IActionService<JobVM>, IActionService<JobListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IJobServiceWrapper jobService;

        public ManageJobCustomFieldsService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, IJobServiceWrapper jobService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.jobService = jobService;
        }


        public void DoAction(JobVM vm)
        {
            if (vm.Job.Id != 0)
                basicInfoController.ShowJobCustomFieldManageView(vm.Job, ActionType.ManageJobCustomFields);
            else
                basicInfoController.ShowJobCustomFieldManageView(vm.Job, ActionType.AddJobCustomFields);
        }


        public void DoAction(JobListVM vm)
        {
            if (vm.SelectedJob.Id != 0)
                basicInfoController.ShowJobCustomFieldManageView(vm.SelectedJob, ActionType.ManageJobCustomFields);
            else
                basicInfoController.ShowJobCustomFieldManageView(vm.SelectedJob, ActionType.AddJobCustomFields);
        }
    }


    
}

