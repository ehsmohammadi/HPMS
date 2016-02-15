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

        //todo:(LOW) Set correct action type for modify and add custom fields
        public void DoAction(JobVM vm)
        {
            if (vm.Job.Id != 0)
                basicInfoController.ShowJobCustomFieldManageView(vm.Job, ActionType.ManageJobCustomFields);
            else
                basicInfoController.ShowJobCustomFieldManageView(vm.Job, ActionType.AssignJobCustomFields);
        }


        public void DoAction(JobListVM vm)
        {
            if (vm.SelectedJob.Id != 0)
                basicInfoController.ShowJobCustomFieldManageView(vm.SelectedJob, ActionType.ManageJobCustomFields);
            else
                basicInfoController.ShowJobCustomFieldManageView(vm.SelectedJob, ActionType.AssignJobCustomFields);
        }
    }


    
}

