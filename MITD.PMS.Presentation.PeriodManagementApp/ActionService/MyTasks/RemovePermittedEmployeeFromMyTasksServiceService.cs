using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class RemovePermittedUserFromMyTasksService : IActionService<PermittedUserListToMyTasksVM>
    {
        private readonly IPMSController pmsController;
        private readonly IMyTasksServiceWrapper myTasksService;

        public RemovePermittedUserFromMyTasksService(IPMSController pmsController, IMyTasksServiceWrapper myTasksService)
        {
            this.pmsController = pmsController;
            this.myTasksService = myTasksService;
        }


        public void DoAction(PermittedUserListToMyTasksVM vm)
        {
            //if (vm.SelectedUser != null)
            //{
            //    if (pmsController.ShowConfirmationBox("آیا از عملیات حذف درخواست اعتراض اطمینان دارید؟", "حذف درخواست اعتراض"))
            //    {
            //        myTasksService.RemovePermittedUserFromMyTasks((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
            //        {
            //            if (exp == null)
            //            {
            //                pmsController.ShowMessage("عملیات حذف درخواست اعتراض با موفقیت انجام شد");
            //                pmsController.Publish(new UpdatePermittedUserListArgs(vm.UserState.UserId));
            //            }
            //            else
            //            {
            //                pmsController.HandleException(exp);
            //            }
            //        }), vm.UserState.UserId, vm.SelectedUser.Id);
            //    }

            //}
            //else
            //    pmsController.ShowMessage("اطلاعات درخواست اعتراض جهت حذف معتبر نمی باشد");
        }


    }
}

