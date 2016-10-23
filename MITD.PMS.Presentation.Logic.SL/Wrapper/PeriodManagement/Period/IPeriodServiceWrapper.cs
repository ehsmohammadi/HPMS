using System;
using System.Collections.ObjectModel;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IPeriodServiceWrapper:IServiceWrapper
    {
        void GetCurrentPeriod(Action<PeriodDTO, Exception> action);
        CalculationStateWithRunSummaryDTO LastFinalCalculation();
        void GetPeriod(Action<PeriodDTO, Exception> action, long id);
        void AddPeriod(Action<PeriodDTO, Exception> action, PeriodDTO period);
        void UpdatePeriod(Action<PeriodDTO, Exception> action, PeriodDTO period);
        void GetAllPeriods(Action<PageResultDTO<PeriodDTOWithAction>, Exception> action, int pageSize, int pageIndex);
        void GetAllPeriods(Action<ObservableCollection<PeriodDescriptionDTO>, Exception> action);
        void DeletePeriod(Action<string, Exception> action,long id);
        void ChangePeriodState(Action<Exception> action, long id, PeriodStateDTO periodStateDTO);
        void CopyBasicDataFrom(Action<Exception> action, long sourcePeriodId, long destinationPeriodId, PeriodStateDTO state);
        void GetPeriodStatus(Action<PeriodStateWithIntializeInquirySummaryDTO, Exception> action, long periodId);
        void GetPeriodCopyBasicDataStatus(Action<PeriodStateWithCopyingSummaryDTO, Exception> action, long periodId);
        void RollBackPeriodState(Action<Exception> action, long id);
        void ChangePeriodActiveStatus(Action<Exception> action, PeriodDTO periodDto);
        void GetPeriodsWithConfirmedResult(Action<ObservableCollection<PeriodDescriptionDTO>, Exception> action);
        void GetEmployeeResultInPeriod(Action<EmployeeResultDTO, Exception> action, long periodId, string employeeNo);
        void GetSubordinatesResultInPeriod(Action<SubordinatesResultDTO, Exception> action, long periodId, string managerEmployeeNo);
    }
}
