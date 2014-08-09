using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class AddPermittedUserToMyTasksService : IActionService<PermittedUserListToMyTasksVM>
    {
        private readonly IPeriodController periodController;

        public AddPermittedUserToMyTasksService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }

        public void DoAction(PermittedUserListToMyTasksVM vm)
        {
            periodController.ShowPermittedUserToMyTasksView(vm.UserState);
        }


    }
}

