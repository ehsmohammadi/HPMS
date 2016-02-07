using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.UnitIndices;

namespace MITD.PMSAdmin.Application
{
    public class UnitIndexService : IUnitIndexService
    {
        private readonly IUnitIndexRepository unitIndexRep;
        private readonly ICustomFieldRepository customFieldRep;


        public UnitIndexService(IUnitIndexRepository unitIndexRep
            , ICustomFieldRepository customFieldRep)
        {
            this.unitIndexRep = unitIndexRep;
            this.customFieldRep = customFieldRep;
        }


        public UnitIndexCategory AddUnitIndexCategory(AbstractUnitIndexId parentId, string name, string dictionaryName)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var id = unitIndexRep.GetNextId();
                    UnitIndexCategory parent = null;
                    if (parentId != null)
                        parent = unitIndexRep.GetUnitIndexCategory(new AbstractUnitIndexId(parentId.Id));
                    var unitIndexCategory = new UnitIndexCategory(id, parent, name, dictionaryName);
                    unitIndexRep.Add(unitIndexCategory);
                    scope.Complete();
                    return unitIndexCategory;

                }
            }
            catch (Exception exp)
            {
                var res = unitIndexRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }


        public UnitIndex AddUnitIndex(AbstractUnitIndexId categoryId, string name
            , string dictionaryName, IList<CustomFieldTypeId> customFieldTypeIdList, Guid transferId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var id = unitIndexRep.GetNextId();
                    var category = unitIndexRep.GetUnitIndexCategory(categoryId);
                    var unitIndex = new UnitIndex(id, category, name, dictionaryName);
                    assignUnitIndexCustomField(unitIndex, customFieldTypeIdList);
                    unitIndex.TransferId = transferId;
                    unitIndexRep.Add(unitIndex);
                    scope.Complete();
                    return unitIndex;

                }
            }
            catch (Exception exp)
            {
                var res = unitIndexRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public UnitIndexCategory UpdateUnitIndexCategory(AbstractUnitIndexId unitIndexCatId, AbstractUnitIndexId parentId, string name, string dictionaryName)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    UnitIndexCategory parent = null;
                    if (parentId != null)
                        parent = unitIndexRep.GetUnitIndexCategory(parentId);
                    var unitIndexCategory = new UnitIndexCategory(unitIndexCatId, parent, name, dictionaryName);
                    unitIndexRep.Update(unitIndexCategory);
                    scope.Complete();
                    return unitIndexCategory;

                }
            }
            catch (Exception exp)
            {
                var res = unitIndexRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public UnitIndex UpdateUnitIndex(AbstractUnitIndexId unitIndexId, AbstractUnitIndexId categoryId, string name
            , string dictionaryName, IList<CustomFieldTypeId> customFieldTypeIdList)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    if (categoryId == null)
                        throw new ArgumentException("CategoryId is null");
                    var category = unitIndexRep.GetUnitIndexCategory(categoryId);
                    var unitIndex = unitIndexRep.GetUnitIndex(unitIndexId);
                    var unitIndexCustomFields = customFieldRep.GetAll(EntityTypeEnum.UnitIndex)
                            .Where(c => customFieldTypeIdList.Contains(c.Id)).ToList();
                    unitIndex.Update(name, dictionaryName, category, unitIndexCustomFields);
                    unitIndexRep.Update(unitIndex);
                    scope.Complete();
                    return unitIndex;

                }
            }
            catch (Exception exp)
            {
                var res = unitIndexRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        private void assignUnitIndexCustomField(UnitIndex unitIndex, IList<CustomFieldTypeId> customFieldTypeIdList)
        {
            foreach (var customFieldId in customFieldTypeIdList)
            {
                var customField = customFieldRep.GetById(customFieldId);
                unitIndex.AssignCustomField(customField);
            }
        }

        public void DeleteAbstractUnitIndex(AbstractUnitIndexId unitIndexId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    unitIndexRep.Delete(unitIndexId);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = unitIndexRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public bool IsValidCustomFieldIdList(AbstractUnitIndexId unitIndexId, IList<CustomFieldTypeId> customFieldTypeIds)
        {
            var unitIndex = (UnitIndex)unitIndexRep.GetById(unitIndexId);
            //var customFieldList = customFieldRep.Find(c => customFieldTypeIds.Contains(c.Id));
            var customFieldList = new List<CustomFieldType>(); //customFieldRep.Find(c => customFieldTypeIds.Contains(c.Id));

            foreach (var customFieldTypeId in customFieldTypeIds)
            {
                customFieldList.Add(customFieldRep.GetById(customFieldTypeId));

            }
            return unitIndex.IsValidCustomFields(customFieldList);
        }

        public UnitIndex GetBy(AbstractUnitIndexId unitIndexId)
        {
            return unitIndexRep.GetUnitIndex(unitIndexId);
        }
    }
}
