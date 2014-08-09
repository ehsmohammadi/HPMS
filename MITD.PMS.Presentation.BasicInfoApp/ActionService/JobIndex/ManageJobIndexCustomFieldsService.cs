using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ManageJobIndexCustomFieldsService : IActionService<JobIndexVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IJobIndexServiceWrapper jobIndexService;

        public ManageJobIndexCustomFieldsService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, IJobIndexServiceWrapper jobIndexService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.jobIndexService = jobIndexService;
        }


        public void DoAction(JobIndexVM vm)
        {
            if (vm.JobIndex.Id != 0)
                basicInfoController.ShowJobIndexCustomFieldManageView(vm.JobIndex, ActionType.ManageJobIndexCustomFields);
            else
                basicInfoController.ShowJobIndexCustomFieldManageView(vm.JobIndex, ActionType.AddJobIndexCustomFields);
        }


    }


    
}

