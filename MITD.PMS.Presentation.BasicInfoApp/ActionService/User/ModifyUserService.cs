using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
 
namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ModifyUserService:IActionService<UserListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IUserServiceWrapper userService;

        public ModifyUserService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, IUserServiceWrapper userService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.userService = userService;
        }


        public void DoAction(UserListVM vm)
        {

            userService.GetUser( (res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    if (res != null)
                        basicInfoController.ShowUserView(res, ActionType.ModifyUser);
                    else
                        pmsController.ShowMessage("اطلاعات  جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);

            }), vm.SelectedUser.PartyName);
        }


    }
}

