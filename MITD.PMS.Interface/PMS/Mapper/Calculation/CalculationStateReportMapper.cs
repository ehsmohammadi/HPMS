using MITD.Core;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Presentation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Interface
{
    public class CalculationStateReportMapper : BaseMapper<CalculationStateReport, CalculationStateWithRunSummaryDTO>, 
        IMapper<CalculationStateReport, CalculationStateWithRunSummaryDTO>
    {
        public override CalculationStateReport MapToEntity(CalculationStateWithRunSummaryDTO model)
        {
            throw new NotImplementedException();
        }
        public override CalculationStateWithRunSummaryDTO MapToModel(CalculationStateReport entity)
        {
            return new CalculationStateWithRunSummaryDTO 
            { 
                StateName = entity.Calculation.State.DisplayName,
                MessageList = entity.Messages.ToList(),
                Percent= entity.Progress==null?0:entity.Progress.Percent
            };
        }
    }
}
