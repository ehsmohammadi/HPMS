using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Repository;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.Units;
using System.Transactions;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Application
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository unitRep;
        private readonly ICustomFieldRepository customFieldRep;
        public UnitService(IUnitRepository unitRep, ICustomFieldRepository customFieldRep)
        {
            this.unitRep = unitRep;
            this.customFieldRep = customFieldRep;
        }

        public Unit AddUnit(string name, string dictionaryName, IList<CustomFieldTypeId> customFieldTypeIdList, Guid transferId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var id = unitRep.GetNextId();
                    var unit = new Unit(id, name, dictionaryName);
                    unit.TransferId = transferId;
                    assignJobCustomField(unit, customFieldTypeIdList);
                    unitRep.Add(unit);
                    scope.Complete();
                    return unit;

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

        public Unit UppdateUnit(UnitId unitId, string name, string dictionaryName, IList<CustomFieldTypeId> customFieldTypeIdList)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var unit = unitRep.GetById(unitId);
                    var unitCustomFields = customFieldRep.GetAll(EntityTypeEnum.Unit)
                             .Where(c => customFieldTypeIdList.Contains(c.Id)).ToList();
                    unit.Update(name, dictionaryName, unitCustomFields);
                    unitRep.UpdateUnit(unit);
                    scope.Complete();
                    return unit;

                    
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

        public void DeleteUnit(UnitId unitId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var unit = unitRep.GetById(unitId);
                    unitRep.DeleteUnit(unit);
                    scope.Complete();
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

        public Unit GetBy(UnitId unitId)
        {
            return unitRep.GetById(unitId);
        }
        private void assignJobCustomField(Unit unit, IList<CustomFieldTypeId> customFieldTypeIdList)
        {
            foreach (var customFieldId in customFieldTypeIdList)
            {
                var customField = customFieldRep.GetById(customFieldId);
                unit.AssignCustomField(customField);
            }
        }

        public bool IsValidCustomFieldIdList(UnitId unitId, IList<CustomFieldTypeId> customFieldTypeIds)
        {
            var unit = unitRep.GetById(unitId);
            var customFieldList = new List<CustomFieldType>(); //customFieldRep.Find(c => customFieldTypeIds.Contains(c.Id));
            foreach (var customFieldTypeId in customFieldTypeIds)
            {
                customFieldList.Add(customFieldRep.GetById(customFieldTypeId));
            }
            return unit.IsValidCustomFields(customFieldList);
        }
    }
}
