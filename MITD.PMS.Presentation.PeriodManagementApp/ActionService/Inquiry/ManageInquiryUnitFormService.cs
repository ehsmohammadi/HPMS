using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class ManageInquiryUnitFormService : IActionService<InquirerInquiryUnitListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly IUnitInquiryServiceWrapper inquiryService;

        public ManageInquiryUnitFormService(IPeriodController periodController,
            IPMSController pmsController, IUnitInquiryServiceWrapper inquiryService)
        {
            this.periodController = periodController;
            this.pmsController = pmsController;
            this.inquiryService = inquiryService;
        }


        public void DoAction(InquirerInquiryUnitListVM vm)
        {
            if (vm.SelectedInquirySubject == null)
                return;
            inquiryService.GetInquiryForm((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                    {
                        res.UnitName = vm.SelectedInquirySubject.UnitName;
                        periodController.ShowInquiryUnitFormView(res, ActionType.FillInquiryUnitForm);
                    }
                    else
                    {
                        pmsController.HandleException(exp);
                    }
                }), vm.PeriodId, vm.InquirerEmployeeNo,vm.SelectedInquirySubject.UnitId);


        }


    }
}

