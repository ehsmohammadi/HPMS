using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class InquiryUnitInquiryUnitIndexPointsController : ApiController
    {
        private readonly IUnitInquiryServiceFacade inquiryService;

        public InquiryUnitInquiryUnitIndexPointsController(IUnitInquiryServiceFacade inquiryService)
        {
            this.inquiryService = inquiryService;
        }

        public InquiryUnitFormDTO GetInquiryForm(long periodId, string inquirerEmployeeNo, long unitId)
        {
            return inquiryService.GetInquiryForm(periodId, inquirerEmployeeNo, unitId);
        }

        public InquiryUnitFormDTO PutInquirySubjectForm(InquiryUnitFormDTO inquiryForm)
        {
            return inquiryService.UpdateInquirySubjectForm(inquiryForm);
        }

      
    }
}