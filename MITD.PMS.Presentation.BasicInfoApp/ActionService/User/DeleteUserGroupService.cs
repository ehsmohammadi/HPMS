using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteUserGroupService:IActionService<UserGroupListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IUserServiceWrapper userService;

        public DeleteUserGroupService(IPMSController pmsController,IUserServiceWrapper userService)
        {
            this.pmsController = pmsController;
            this.userService = userService;
        }


        public void DoAction(UserGroupListVM vm)
        {
            
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف  اطمینان دارید؟", "حذف گروه کاربر"))
                {
                    userService.DeleteUserGroup((res, exp) => pmsController.BeginInvokeOnDispatcher(()=>
                        {
                            if (exp == null)
                            {
                                pmsController.ShowMessage("عملیات حذف  با موفقیت انجام شد");
                                pmsController.Publish(new UpdateUserGroupListArgs());
                            }
                            else 
                            {
                                pmsController.HandleException(exp);
                            }

                        }
                        ),vm.SelectedUserGroup.PartyName);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات  جهت حذف معتبر نمی باشد");
        }


    }
}

