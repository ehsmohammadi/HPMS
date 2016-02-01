using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ManageGroupCustomActionsService : IActionService<UserGroupVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;

        public ManageGroupCustomActionsService(IBasicInfoController basicInfoController
            ,IPMSController pmsController)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
        }


        public void DoAction(UserGroupVM vm)
        {
            basicInfoController.ShowCustomActionsManageViews(vm.UserGroup, true, vm.UserGroup.Id);
        }
        
    }


    
}

