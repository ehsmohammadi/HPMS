using System;
using System.Collections.Generic;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;
using System.Transactions;
using MITD.PMS.Exceptions;


namespace MITD.PMS.Application
{
    public class UnitService : IUnitService
    {
        private readonly IPeriodRepository periodRep;
        private readonly IPMSAdminService ipmsAdminService;
        private readonly IUnitRepository unitRep;

        public UnitService(
                           IUnitRepository unitRep,
                           IPeriodRepository periodRep
            //)
                           ,IPMSAdminService ipmsAdminService)
        {
            this.periodRep = periodRep;
            this.ipmsAdminService = ipmsAdminService;
            this.unitRep = unitRep;
        }


        public Unit AssignUnit(PeriodId periodId, SharedUnitId sharedUnitId, SharedUnitId parentSharedUnitId)
        {
            using (var tr = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                for (int i = 0; i < 100; i++)
                {
                    var s = ipmsAdminService.GetSharedUnit(sharedUnitId);
                }
                var sharedUnit = ipmsAdminService.GetSharedUnit(sharedUnitId);
                Unit parent = null;
                if (parentSharedUnitId != null)
                    parent = unitRep.GetBy(new UnitId(periodId, parentSharedUnitId));
                var unit = new Unit(period, sharedUnit, parent);
                unitRep.Add(unit);
                tr.Complete();
                return unit;

            }
        }

        public void RemoveUnit(PeriodId periodId, SharedUnitId sharedUnitId)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var period = periodRep.GetById(periodId);
                    period.CheckRemovingUnit();
                    var unit = unitRep.GetBy(new UnitId(periodId, sharedUnitId));
                    unitRep.DeleteUnit(unit);
                    tr.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = unitRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public List<Unit> GetAllParentUnits(Period period)
        {
            using (var tr = new TransactionScope())
            {
                var units = unitRep.GetAllParentUnits(period);
                tr.Complete();
                return units;
            }
        }

        public List<Unit> GetAllUnitByParentId(UnitId id)
        {
            using (var tr = new TransactionScope())
            {
                var units = unitRep.GetAllUnitByParentId(id);
                tr.Complete();
                return units;
            }
        }

        public UnitId GetUnitIdBy(Period period, SharedUnitId sharedUnitId)
        {
            using (var tr = new TransactionScope())
            {
                var unitId = unitRep.GetUnitIdBy(period, sharedUnitId);
                tr.Complete();
                return unitId;
            }
        }

        public Unit GetUnitBy(UnitId unitId)
        {
            var unit = unitRep.GetBy(unitId);
            return unit;
        }
    }
}
