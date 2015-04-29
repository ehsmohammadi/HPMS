using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteJobIndexCategoryService:IActionService<JobIndexTreeVM>
    {
        private readonly IPMSController pmsController;
        private readonly IJobIndexServiceWrapper jobIndexCategoryService;

        public DeleteJobIndexCategoryService(IPMSController pmsController,IJobIndexServiceWrapper jobIndexCategoryService)
        {
            this.pmsController = pmsController;
            this.jobIndexCategoryService = jobIndexCategoryService;
        }


        public void DoAction(JobIndexTreeVM vm)
        {
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف دسته شاخص اطمینان دارید؟", "حذف دسته شاخص"))
                {
                    jobIndexCategoryService.DeleteJobIndexCategory((res, exp) => pmsController.BeginInvokeOnDispatcher(()=>
                        {
                            if (exp == null )
                            {
                                pmsController.ShowMessage("عملیات حذف دسته شاخص با موفقیت انجام شد");
                                pmsController.Publish(new UpdateJobIndexTreeArgs());
                            }
                            else 
                                pmsController.HandleException(exp);
                        }
                        ),vm.SelectedJobIndex.Data.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات دسته شاخص جهت  حذف معتبر نمی باشد");
        }


    }
}

