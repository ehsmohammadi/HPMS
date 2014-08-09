using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class SettingPermittedUserToMyTasksService : IActionService<PermittedUserListToMyTasksVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly IMyTasksServiceWrapper myTasksService;

        public SettingPermittedUserToMyTasksService(IPMSController pmsController, IMyTasksServiceWrapper myTasksService,
            IPeriodController periodController)
        {
            this.pmsController = pmsController;
            this.myTasksService = myTasksService;
            this.periodController = periodController;
        }


        public void DoAction(PermittedUserListToMyTasksVM vm)
        {
            
        }
    }
}

