using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class InquirerInquiryUnitsController : ApiController
    {
        private readonly IUnitInquiryServiceFacade inquiryService;

        public InquirerInquiryUnitsController(IUnitInquiryServiceFacade inquiryService)
        {
            this.inquiryService = inquiryService;
        }

        public List<InquiryUnitDTO> GetInquirerInquirySubjects(long periodId, string inquirerEmployeeNo)
        {
            return inquiryService.GetInquirerInquirySubjects(periodId, inquirerEmployeeNo);
        }

      
    }
}