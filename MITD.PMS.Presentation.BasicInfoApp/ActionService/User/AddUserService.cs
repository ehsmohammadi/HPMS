using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class AddUserService : IActionService<UserListVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddUserService( IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(UserListVM vm)
        {
            var user = new UserDTO();
            basicInfoController.ShowUserView(user, ActionType.AddUser);
        }


    }
}

