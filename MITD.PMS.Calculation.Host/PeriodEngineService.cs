using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MITD.PMS.Calculation.Host
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalculationEngineService" in both code and config file together.
    public partial class CalculationEngineService : IPeriodEngineService
    {


        public void InitializeInquiry(long periodId)
        {
            periodService.InitializeInquiry(new PeriodId(periodId));
        }

        public PeriodEngineState GetIntializeInquiryState(long periodId)
        {
            var progress = periodService.GetPeriodInitializeInquiryState(new PeriodId(periodId));
            return new PeriodEngineState
            {
                MessageList = progress.Messages,
                Percent = progress.Percent,
                StateName = progress.State.DisplayName
            };
        }

        public void CopyBasicData(long sourcePeriodId, long destionationPeriodId)
        {
            periodService.CopyBasicData(sourcePeriodId, destionationPeriodId);
        }

        public PeriodEngineState GetPeriodCopyingStateProgress(long periodId)
        {
            var progress = periodService.GetPeriodCopyingStateProgress(new PeriodId(periodId));
            return new PeriodEngineState
            {
                MessageList = progress.Messages,
                Percent = progress.Percent,
                StateName = progress.State.DisplayName
            };
        }
    }
}
