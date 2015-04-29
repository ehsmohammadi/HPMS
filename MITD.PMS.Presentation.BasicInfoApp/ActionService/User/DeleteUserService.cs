using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteUserService:IActionService<UserListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IUserServiceWrapper userService;

        public DeleteUserService(IPMSController pmsController,IUserServiceWrapper userService)
        {
            this.pmsController = pmsController;
            this.userService = userService;
        }


        public void DoAction(UserListVM vm)
        {
            
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف  اطمینان دارید؟", "حذف کاربر"))
                {
                    userService.DeleteUser((res, exp) => pmsController.BeginInvokeOnDispatcher(()=>
                        {
                            if (exp == null)
                            {
                                pmsController.ShowMessage("عملیات حذف  با موفقیت انجام شد");
                                pmsController.Publish(new UpdateUserListArgs());
                            }
                            else 
                            {
                                pmsController.HandleException(exp);
                            }

                        }
                        ),vm.SelectedUser.PartyName);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات  جهت حذف معتبر نمی باشد");
        }


    }
}

