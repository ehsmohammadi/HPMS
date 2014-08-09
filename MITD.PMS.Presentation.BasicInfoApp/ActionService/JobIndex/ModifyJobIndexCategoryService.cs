using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ModifyJobIndexCategoryService : IActionService<JobIndexTreeVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IJobIndexServiceWrapper jobIndexCategoryService;

        public ModifyJobIndexCategoryService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, IJobIndexServiceWrapper jobIndexCategoryService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.jobIndexCategoryService = jobIndexCategoryService;
        }


        public void DoAction(JobIndexTreeVM jlvm)
        {
            jobIndexCategoryService.GetJobIndexCategory((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                    {
                        if (res != null)
                            basicInfoController.ShowJobIndexCategoryView(res, ActionType.ModifyJobIndexCategory);
                        else
                            pmsController.ShowMessage("اطلاعات دسته شاخص شغل جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                    }
                    else
                        pmsController.HandleException(exp);

                }), jlvm.SelectedJobIndex.Data.Id);



        }


    }


    
}

