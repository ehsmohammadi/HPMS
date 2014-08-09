using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class InquirySubjectInquirersController : ApiController
    {
        private readonly IPeriodJobPositionServiceFacade jobPositionService;

        public InquirySubjectInquirersController(IPeriodJobPositionServiceFacade jobPositionService)
        {
            this.jobPositionService = jobPositionService;
        }

        public InquirySubjectWithInquirersDTO PutInquirySubjectInquirers( long periodId, long jobPositionId,string inquirySubjectEmployeeNo,int batch,
           InquirySubjectWithInquirersDTO inquirySubjectWithInquirersDTO)
        {
            return jobPositionService.UpdateInquirySubjectInquirers(periodId, jobPositionId, inquirySubjectEmployeeNo,
                inquirySubjectWithInquirersDTO);
        }

      
    }
}