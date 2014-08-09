using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class AddUserGroupService : IActionService<UserGroupListVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddUserGroupService( IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(UserGroupListVM vm)
        {
            var userGroup = new UserGroupDTO();
            basicInfoController.ShowUserGroupView(userGroup, ActionType.AddUserGroup);
        }


    }
}

