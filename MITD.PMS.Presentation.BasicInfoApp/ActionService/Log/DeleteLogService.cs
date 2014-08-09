using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteLogService:IActionService<LogListVM>
    {
        private readonly IPMSController pmsController;
        private readonly ILogServiceWrapper unitService;

        public DeleteLogService(IPMSController pmsController,ILogServiceWrapper unitService)
        {
            this.pmsController = pmsController;
            this.unitService = unitService;
        }


        public void DoAction(LogListVM vm)
        {
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف لاگ اطمینان دارید؟", "حذف لاگ"))
                {
                    unitService.DeleteLog((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                        {
                            pmsController.ShowMessage("عملیات حذف لاگ با موفقیت انجام شد");
                            pmsController.Publish(new UpdateLogListArgs());
                        }
                        else
                            pmsController.HandleException(exp);
                    }),  vm.SelectedLog.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات لاگ جهت حذف معتبر نمی باشد");
        }


    }
}

