using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Integration.PMS.API
{
    public partial interface IPeriodServiceWrapper : IServiceWrapper
    {
        PeriodDTO GetActivePeriod();

        //void GetAllPeriods(Action<PageResultDTO<PeriodDTOWithAction>, Exception> action, int pageSize, int pageIndex);
        //void GetPeriod(Action<PeriodDTO, Exception> action, long id);
        //CalculationStateWithRunSummaryDTO LastFinalCalculation();
        //void AddPeriod(Action<PeriodDTO, Exception> action, PeriodDTO period);
        //void UpdatePeriod(Action<PeriodDTO, Exception> action, PeriodDTO period);
        //void DeletePeriod(Action<string, Exception> action, long id);
        //void ChangePeriodState(Action<Exception> action, long id, PeriodStateDTO periodStateDTO);
        //void CopyBasicDataFrom(Action<Exception> action, long sourcePeriodId, long destinationPeriodId, PeriodStateDTO state);
        //void GetPeriodStatus(Action<PeriodStateWithIntializeInquirySummaryDTO, Exception> action, long periodId);
        //void GetPeriodCopyBasicDataStatus(Action<PeriodStateWithCopyingSummaryDTO, Exception> action, long periodId);
        //void RollBackPeriodState(Action<Exception> action, long id);
        //void ChangePeriodActiveStatus(Action<Exception> action, PeriodDTO periodDto);

    }
}
