using MITD.Core;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Service;
using MITD.PMS.Presentation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Interface
{
    public class PeriodInitializeInquiryStateReportMapper : BaseMapper<InquiryInitializingProgress, PeriodStateWithIntializeInquirySummaryDTO>,
        IMapper<InquiryInitializingProgress, PeriodStateWithIntializeInquirySummaryDTO>
    {
        public override InquiryInitializingProgress MapToEntity(PeriodStateWithIntializeInquirySummaryDTO model)
        {
            throw new NotImplementedException();
        }
        public override PeriodStateWithIntializeInquirySummaryDTO MapToModel(InquiryInitializingProgress entity)
        {
            return new PeriodStateWithIntializeInquirySummaryDTO 
            { 
                StateName = entity.State.DisplayName,
                MessageList = entity.Messages,
                Percent= entity.Percent
            };
        }
    }
}
