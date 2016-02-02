using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.Contract
{
    public interface IUnitIndexInPeriodServiceWrapper
    {
        void GetUnitIndexInPeriod(Action<UnitIndexInPeriodDTO, Exception> action, long periodId, long abstractId);
        void AddUnitIndexInPeriod(Action<UnitIndexInPeriodDTO, Exception> action, UnitIndexInPeriodDTO unitIndexInPeriod);
        void UpdateUnitIndexInPeriod(Action<UnitIndexInPeriodDTO, Exception> action, UnitIndexInPeriodDTO unitIndexInPeriod);
        void DeleteUnitIndexInPeriod(Action<string, Exception> action, long periodId, long abstractId);

        void GetPeriodAbstractIndexes(Action<List<AbstractIndexInPeriodDTOWithActions>, Exception> action, long periodId);
        void GetAllPeriodUnitIndexes(Action<List<UnitIndexInPeriodDTO>, Exception> action, long periodId);

        void AddUnitIndexGroupInPeriod(Action<UnitIndexGroupInPeriodDTO, Exception> action, UnitIndexGroupInPeriodDTO unitIndexGroupInPeriod);
        void UpdateUnitIndexGroupInPeriod(Action<UnitIndexGroupInPeriodDTO, Exception> action, UnitIndexGroupInPeriodDTO unitIndexGroupInPeriod);
        void GetUnitIndexGroupInPeriod(Action<UnitIndexGroupInPeriodDTO, Exception> action, long periodId, long abstractId);
        void DeleteUnitIndexGroupInPeriod(Action<string, Exception> action, long periodId, long abstractId);

        void GetPeriodUnitIndexes(Action<List<UnitIndexGroupInPeriodDTO>, Exception> action, long periodId);

    }
}
