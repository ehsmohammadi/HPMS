using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class JobPositionInquirySubjectsController : ApiController
    {
        private readonly IPeriodJobPositionServiceFacade jobPositionService;


        public JobPositionInquirySubjectsController(IPeriodJobPositionServiceFacade jobPositionService)
        {
            this.jobPositionService = jobPositionService;
        }

        public List<InquirySubjectWithInquirersDTO> GetJobPositionInquirySubjects(long periodId, long jobPositionId, string include)
        {
            return jobPositionService.GetInquirySubjectsWithInquirers(periodId, jobPositionId);
        }
    }
}