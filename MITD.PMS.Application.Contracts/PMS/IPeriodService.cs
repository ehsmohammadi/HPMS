
using System;
using MITD.Core;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Application.Contracts
{
    public interface IPeriodService : IService
    {
        void Delete(PeriodId periodId);
        Period AddPeriod(string name, DateTime startDate, DateTime endDate);
        Period UpdatePeriod(PeriodId periodId, string name, DateTime startDate, DateTime endDate);
        Period GetCurrentPeriod();
        void StartInquiry(PeriodId periodId);
        void CompleteInquiry(PeriodId periodId);
        void Close(PeriodId periodId);
        InquiryInitializingProgress GetIntializeInquiryState(PeriodId periodId);
        void InitializeInquiry(PeriodId periodId);
        void CompleteInitializeInquiry(PeriodId periodId);
        void CopyBasicData(long sourcePeriodId, long destionationPeriodId);
        void CompleteCopyingBasicData(PeriodId periodId, PeriodState preState);
        InquiryInitializingProgress GetPeriodInitializeInquiryState(PeriodId periodId);
        void RollBack(PeriodId periodId);
        BasicDataCopyingProgress GetPeriodCopyingStateProgress(PeriodId periodId);
        void ChangePeriodActiveStatus(PeriodId periodId, bool active);
        void StartClaiming(PeriodId periodId);
        void FinishClaiming(PeriodId periodId);
    }
}
