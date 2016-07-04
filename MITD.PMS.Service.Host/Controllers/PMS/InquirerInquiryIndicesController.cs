using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class InquirerInquiryIndicesController : ApiController
    {
        private readonly IInquiryServiceFacade inquiryService;

        public InquirerInquiryIndicesController(IInquiryServiceFacade inquiryService)
        {
            this.inquiryService = inquiryService;
        }

        public List<InquiryIndexDTO> GetInquirerInquirySubjects(long periodId, string inquirerEmployeeNo)
        {
            return inquiryService.GetInquirerInquiryIndices(periodId, inquirerEmployeeNo);
        }

      
    }
}