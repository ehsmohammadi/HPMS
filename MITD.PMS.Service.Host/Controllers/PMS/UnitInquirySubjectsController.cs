using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class UnitInquirySubjectsController : ApiController
    {
        private readonly IPeriodUnitServiceFacade _periodUnitServiceFacade;


        public UnitInquirySubjectsController(IPeriodUnitServiceFacade periodUnitServiceFacade)
        {
            this._periodUnitServiceFacade = periodUnitServiceFacade;
        }

        public List<InquirySubjectWithInquirersDTO> GetUnitInquirySubjects(long periodId, long unitId, string include)
        {
            return _periodUnitServiceFacade.GetInquirySubjectsWithInquirers(periodId, unitId);
            return null;
        }

       [HttpPut]
        public string PutInquiry(long periodId, long unitId,string employeeNo)
        {
             _periodUnitServiceFacade.AddInquirer( periodId,  unitId, employeeNo);
             return "";
        }

       public string DeleteInquiry(long periodId, long unitId, string employeeNo)
       {
           _periodUnitServiceFacade.RemoveInquirer(periodId, unitId, employeeNo);
           return "";
       }
    }
}