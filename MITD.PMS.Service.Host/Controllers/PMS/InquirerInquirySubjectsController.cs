using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class InquirerInquirySubjectsController : ApiController
    {
        private readonly IInquiryServiceFacade inquiryService;

        public InquirerInquirySubjectsController(IInquiryServiceFacade inquiryService)
        {
            this.inquiryService = inquiryService;
        }

        public List<InquirySubjectDTO> GetInquirerInquirySubjects(long periodId, string inquirerEmployeeNo)
        {
            return inquiryService.GetInquirerInquirySubjects(periodId, inquirerEmployeeNo);
        }

      
    }
}