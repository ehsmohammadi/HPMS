using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IPeriodUnitServiceFacade : IFacadeService
    {
        List<InquirySubjectWithInquirersDTO> GetInquirySubjectsWithInquirers(long periodId, long unitId);
        PageResultDTO<UnitInPeriodDTOWithActions> GetAllUnits(long periodId, int pageSize, int pageIndex,
                                                            QueryStringConditions queryStringConditions, string selectedColumns);
        UnitInPeriodDTO UpdateUnit(long periodId, UnitInPeriodDTO unitInPeriod);

        UnitInPeriodDTO AssignUnit(long periodId, UnitInPeriodDTO unitInPeriod);
        string RemoveUnit(long periodId, long unitId);
        IEnumerable<UnitInPeriodDTOWithActions> GetUnitsWithActions(long periodId);
        IEnumerable<UnitInPeriodDTO> GetUnits(long periodId);

        UnitInPeriodDTO GetUnit(long periodId, long unitId, string selectedColumns);
        void AddInquirer(long periodId, long unitId, string employeeNo,long unitIndexInPeiodUnit);
        void RemoveInquirer(long periodId, long unitId, string employeeNo);
    }
}
