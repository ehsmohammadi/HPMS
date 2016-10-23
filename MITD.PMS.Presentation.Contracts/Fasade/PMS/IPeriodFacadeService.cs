using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts.Fasade
{
    public interface IPeriodServiceFacade:IFacadeService
    {
        PageResultDTO<PeriodDTOWithAction> GetAllPeriods(int pageSize, int pageIndex);

        List<PeriodDescriptionDTO> GetAllPeriods();

        string DeletePeriod(long id);

        PeriodDTO GetPeriod(long id);

        PeriodDTO AddPeriod(PeriodDTO period);

        PeriodDTO UpdatePeriod(PeriodDTO period);

        PeriodDTO GetCurrentPeriod();
        void ChangePeriodState(long periodId, PeriodStateDTO stateDto);
        void CopyBasicData(long sourcePeriodId, long destionationPeriodId, PeriodStateDTO stateDto);
        PeriodStateWithIntializeInquirySummaryDTO GetPeriodState(long id);
        PeriodStateWithCopyingSummaryDTO GetPeriodBasicDataCopyState(long periodId);
        void RollBackPeriodState(long id);
        void ChangePeriodActiveStatus(long id, bool active);
        List<PeriodDescriptionDTO> GetPeriodsWithConfirmedResult();
        EmployeeResultDTO GetEmployeeResultInPeriod(long periodId, string employeeNo);
        SubordinatesResultDTO GetSubordinatesResultInPeriod(long periodId, string managerEmployeeNo);
    }
}
