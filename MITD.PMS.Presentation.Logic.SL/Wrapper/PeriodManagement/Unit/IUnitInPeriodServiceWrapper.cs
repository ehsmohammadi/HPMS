using System.Collections.Generic;
using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;
using System;

namespace MITD.PMS.Presentation.Logic
{
    public interface IUnitInPeriodServiceWrapper : IServiceWrapper
    {
       
        void AddUnitInPeriod(Action<UnitInPeriodAssignmentDTO, Exception> action, UnitInPeriodAssignmentDTO jobPositionInPeriod);       
        void DeleteUnitInPeriod(Action<string, Exception> action, long periodId, long jobPositionId);
        void GetAllUnits(Action<List<UnitInPeriodDTO>,Exception> action,long periodId);
        void GetUnitsWithActions(Action<List<UnitInPeriodDTOWithActions>, Exception> action, long periodId);
    }
}
