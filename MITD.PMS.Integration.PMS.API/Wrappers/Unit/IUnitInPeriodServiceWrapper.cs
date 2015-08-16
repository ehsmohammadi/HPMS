using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public interface IUnitInPeriodServiceWrapper
    {
        void AddInquirer(Action<string, Exception> action, long periodId, long unitId, string personalNo);
        void UpdateInquirySubjectInquirers(Action<InquirySubjectWithInquirersDTO, Exception> action, long periodId, long unitId, InquirySubjectWithInquirersDTO inquirySubjectWithInquirersDTO);
        void GetInquirySubjectWithInquirers(Action<List<InquirySubjectWithInquirersDTO>, Exception> action, long periodId, long unitId);
        void GetAllUnitInPeriod(Action<IList<UnitInPeriodDTO>, Exception> action, long periodId);
        void GetUnitInPeriod(Action<UnitInPeriodDTO, Exception> action, long periodId, long unitId);
        void AddUnitInPeriod(Action<UnitInPeriodAssignmentDTO, Exception> action, UnitInPeriodAssignmentDTO jobPositionInPeriod);
        void AddUnitInPeriod(Action<JobInPeriodDTO, Exception> action, long periodId, UnitInPeriodDTO unitInPeriodDto);

        void UpdateUnitInPeriod(Action<UnitInPeriodDTO, Exception> action, long periodId,
            UnitInPeriodDTO unitInPeriodDto);
        void DeleteUnitInPeriod(Action<string, Exception> action, long periodId, long jobPositionId);
        void GetAllUnits(Action<List<UnitInPeriodDTO>, Exception> action, long periodId);
        void GetUnitsWithActions(Action<List<UnitInPeriodDTOWithActions>, Exception> action, long periodId);

        void DeleteInquirer(Action<string, Exception> action, long periodId, long unitId, string personalNo);
    }
}
