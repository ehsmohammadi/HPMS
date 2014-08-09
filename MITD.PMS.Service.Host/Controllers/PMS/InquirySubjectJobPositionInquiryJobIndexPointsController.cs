using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class InquirySubjectJobPositionInquiryJobIndexPointsController : ApiController
    {
        private readonly IInquiryServiceFacade inquiryService;

        public InquirySubjectJobPositionInquiryJobIndexPointsController(IInquiryServiceFacade inquiryService)
        {
            this.inquiryService = inquiryService;
        }

        public InquiryFormDTO GetInquiryForm(long periodId, string inquirerEmployeeNo, string inquirySubjectEmployeeNo, long jobPositionId, long inquirerJobPositionId)
        {
            return inquiryService.GetInquiryForm(periodId,inquirerJobPositionId, inquirerEmployeeNo, inquirySubjectEmployeeNo, jobPositionId);
        }

        public InquiryFormDTO PutInquirySubjectForm(InquiryFormDTO inquiryForm,string batch)
        {
            return inquiryService.UpdateInquirySubjectForm(inquiryForm);
        }

      
    }
}