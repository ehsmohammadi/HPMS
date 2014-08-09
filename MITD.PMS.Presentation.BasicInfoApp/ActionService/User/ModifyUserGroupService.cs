using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
 
namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ModifyUserGroupService:IActionService<UserGroupListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IUserServiceWrapper userService;

        public ModifyUserGroupService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, IUserServiceWrapper userService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.userService = userService;
        }


        public void DoAction(UserGroupListVM vm)
        {

            userService.GetUserGroup( (res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    if (res != null)
                        basicInfoController.ShowUserGroupView(res, ActionType.ModifyUserGroup);
                    else
                        pmsController.ShowMessage("اطلاعات  جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);

            }), vm.SelectedUserGroup.PartyName);
        }


    }
}

