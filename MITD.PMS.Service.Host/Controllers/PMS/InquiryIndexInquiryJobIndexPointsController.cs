using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class InquiryIndexInquiryJobIndexPointsController : ApiController
    {
        private readonly IInquiryServiceFacade inquiryService;

        public InquiryIndexInquiryJobIndexPointsController(IInquiryServiceFacade inquiryService)
        {
            this.inquiryService = inquiryService;
        }

        public InquiryFormByIndexDTO GetInquiryFormByIndex(long periodId, string inquirerEmployeeNo, long jobIndexId)
        {
            return inquiryService.GetInquiryFormByIndex(periodId, inquirerEmployeeNo, jobIndexId);
        }

        public InquiryFormByIndexDTO PutJobIndexInquiryForm(InquiryFormByIndexDTO inquiryForm, string batch)
        {
            return inquiryService.UpdateJobIndexInquiryForm(inquiryForm);
        }

      
    }
}