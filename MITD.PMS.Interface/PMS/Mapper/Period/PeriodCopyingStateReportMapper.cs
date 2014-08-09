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
    public class PeriodCopyingStateReportMapper : BaseMapper<BasicDataCopyingProgress, PeriodStateWithCopyingSummaryDTO>,
        IMapper<BasicDataCopyingProgress, PeriodStateWithCopyingSummaryDTO>
    {
        public override BasicDataCopyingProgress MapToEntity(PeriodStateWithCopyingSummaryDTO model)
        {
            throw new NotImplementedException();
        }
        public override PeriodStateWithCopyingSummaryDTO MapToModel(BasicDataCopyingProgress entity)
        {
            return new PeriodStateWithCopyingSummaryDTO 
            {
                StateName = entity.State.DisplayName,
                MessageList = entity.Messages,
                Percent= entity.Percent
            };
        }
    }
}
