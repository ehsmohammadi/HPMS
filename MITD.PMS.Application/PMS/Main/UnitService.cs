using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.UnitIndices;
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
        private readonly IUnitIndexRepository unitIndexRep;
        private readonly IPeriodManagerService periodChecker;
        private readonly IUnitInquiryConfiguratorService _configuratorService;

        public UnitService(
                           IUnitRepository unitRep,
                           IPeriodRepository periodRep,
                           IUnitIndexRepository unitIndexRep
                           ,IPMSAdminService ipmsAdminService,
                            IPeriodManagerService periodChecker,
            IUnitInquiryConfiguratorService configuratorService)
        {
            this.periodRep = periodRep;
            this.ipmsAdminService = ipmsAdminService;
            this.unitRep = unitRep;
            this.unitIndexRep = unitIndexRep;
            this.periodChecker = periodChecker;
            _configuratorService = configuratorService;
        }


        public Unit UpdateUnit(UnitId unitId, List<SharedUnitCustomFieldId> customFieldIdList, IList<UnitIndexForUnit> unitIndexList)
        {
            using (var tr = new TransactionScope())
            {

                var unit = unitRep.GetBy(unitId);
                var sharedUnitCustomFields = ipmsAdminService.GetSharedCutomFieldListForUnit(unitId.SharedUnitId, customFieldIdList);

                var unitCustomFields = new List<UnitCustomField>();
                foreach (var sharedUnitCustomField in sharedUnitCustomFields)
                {
                    unitCustomFields.Add(new UnitCustomField(new UnitCustomFieldId(unitId.PeriodId, sharedUnitCustomField.Id, unitId.SharedUnitId),
                        sharedUnitCustomField
                        ));
                }
                unit.UpdateCustomFields(unitCustomFields, periodChecker);
                var unitindexIdList = unitIndexList.Select(jj => jj.UnitIndexId).ToList();
                var unitIndices = unitIndexRep.FindUnitIndices(j => unitindexIdList.Contains(j.Id));

                var unitUnitIndices = new List<UnitUnitIndex>();
                foreach (var unitIndex in unitIndices)
                {
                    var unitindexForUnit = unitIndexList.Single(j => j.UnitIndexId == unitIndex.Id);
                    unitUnitIndices.Add(new UnitUnitIndex(unitIndex.Id, unitindexForUnit.ShowforTopLevel, unitindexForUnit.ShowforSameLevel, unitindexForUnit.ShowforLowLevel));
                }
                unit.UpdateUnitIndices(unitUnitIndices, periodChecker);
                tr.Complete();
                return unit;

            }
        }

        public Unit AssignUnit(UnitId parentId,UnitId unitId, List<SharedUnitCustomFieldId> customFieldIdList, 
            IList<UnitIndexForUnit> unitIndexList)
        {
            using (var tr = new TransactionScope())
            {
                var period = periodRep.GetById(unitId.PeriodId);
                var sharedUnit = ipmsAdminService.GetSharedUnit(unitId.SharedUnitId);

                var sharedUnitCustomFields = ipmsAdminService.GetSharedCutomFieldListForUnit(unitId.SharedUnitId, customFieldIdList);

                var unitCustomFields = new List<UnitCustomField>();
                foreach (var sharedUnitCustomField in sharedUnitCustomFields)
                {
                    unitCustomFields.Add(new UnitCustomField(new UnitCustomFieldId(period.Id, sharedUnitCustomField.Id, unitId.SharedUnitId),
                        sharedUnitCustomField
                        ));
                }

                var unitIndexIds = unitIndexList.Select(jj => jj.UnitIndexId).ToList();
                var unitIndices = unitIndexRep.FindUnitIndices(j =>unitIndexIds.Contains(j.Id));
                //unit.UpdateUnitIndices(unitIndices.ToList());
                var unitUnitInddices = new List<UnitUnitIndex>();
                foreach (var unitIndex in unitIndices)
                {
                    var unitindexForUnit = unitIndexList.Single(j => j.UnitIndexId == unitIndex.Id);
                    unitUnitInddices.Add(new UnitUnitIndex(unitIndex.Id, unitindexForUnit.ShowforTopLevel, unitindexForUnit.ShowforSameLevel, unitindexForUnit.ShowforLowLevel));
                }

                var parent = unitRep.GetBy(parentId);
                
                var unit = new Unit(period, sharedUnit, unitCustomFields, unitUnitInddices,parent);
                unitRep.Add(unit);
                tr.Complete();
                return unit;

            }
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

        public List<UnitInquiryConfigurationItem> GetInquirySubjectWithInquirer(UnitId unitId)
        {
            using (var tr = new TransactionScope())
            {
                var unit = unitRep.GetBy(unitId);
                unit.ConfigeInquirer(_configuratorService, false);
                tr.Complete();
                return unit.ConfigurationItemList.ToList();

            }
        }
    }
}
