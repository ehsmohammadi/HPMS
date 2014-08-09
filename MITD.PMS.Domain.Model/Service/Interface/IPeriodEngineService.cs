using MITD.Core;
using MITD.PMS.Domain.Model.Periods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Service
{
    public interface IPeriodEngineService:IService
    {
        void InitializeInquiry(PeriodId periodId);
        PeriodEngineState GetIntializeInquiryState(PeriodId periodId);
        void CopyBasicData(PeriodId sourcePeriodId, PeriodId destionationPeriodId);
        PeriodEngineState GetPeriodCopyingStateProgress(PeriodId periodId);
    }
}
