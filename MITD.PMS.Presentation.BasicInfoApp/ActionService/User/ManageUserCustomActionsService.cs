using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ManageUserCustomActionsService : IActionService<UserVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;

        public ManageUserCustomActionsService(IBasicInfoController basicInfoController
            ,IPMSController pmsController)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
        }


        public void DoAction(UserVM vm)
        {
            basicInfoController.ShowCustomActionsManageViews(vm.User, false, "");
        }


    }


    
}

