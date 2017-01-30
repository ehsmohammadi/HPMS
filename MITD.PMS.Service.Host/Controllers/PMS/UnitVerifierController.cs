using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class UnitVerifiersController : ApiController
    {
        private readonly IPeriodUnitServiceFacade _periodUnitServiceFacade;


        public UnitVerifiersController(IPeriodUnitServiceFacade periodUnitServiceFacade)
        {
            this._periodUnitServiceFacade = periodUnitServiceFacade;
        }

        public List<InquirySubjectWithInquirersDTO> GetUnitVerifiers(long periodId, long unitId)
        {
            return _periodUnitServiceFacade.GetInquirySubjectsWithInquirers(periodId, unitId);
         
        }

       [HttpPut]
        public string PostVerifier(long periodId, long unitId,string employeeNo)
        {
            _periodUnitServiceFacade.AddVerifier(periodId, unitId, employeeNo);
             return "";
        }

       public string DeleteVerifier(long periodId, long unitId, string employeeNo)
       {
           _periodUnitServiceFacade.DeleteVerifier(periodId, unitId, employeeNo);
           return "";
       }
    }
}