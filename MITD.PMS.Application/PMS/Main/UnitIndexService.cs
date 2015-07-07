using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Service;
using System.Transactions;
using MITD.PMS.Exceptions;


namespace MITD.PMS.Application
{
    public class UnitIndexService : IUnitIndexService
    {
        private readonly IPeriodRepository periodRep;
        private readonly IPMSAdminService pmsAdminService;
        private readonly IPeriodManagerService periodChecker;
        private readonly IUnitIndexRepository unitIndexRep;

        public UnitIndexService(

            IUnitIndexRepository unitIndexRep,
            IPeriodRepository periodRep,
            IPMSAdminService ipmsAdminService,
            IPeriodManagerService periodChecker
            )
        {
            this.periodRep = periodRep;
            this.pmsAdminService = ipmsAdminService;
            this.periodChecker = periodChecker;
            this.unitIndexRep = unitIndexRep;
        }

        public UnitIndexGroup AddUnitIndexGroup(PeriodId periodId, AbstractUnitIndexId parentId, string name,
            string dictionaryName)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var id = unitIndexRep.GetNextId();
                    var period = periodRep.GetById(periodId);
                    UnitIndexGroup parent = null;
                    if (parentId != null)
                        parent = unitIndexRep.GetUnitIndexGroupById(new AbstractUnitIndexId(parentId.Id));
                    var unitIndexGroup = new UnitIndexGroup(id, period, parent, name, dictionaryName);
                    unitIndexRep.Add(unitIndexGroup);
                    tr.Complete();
                    return unitIndexGroup;

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

        public UnitIndex AddUnitIndex(PeriodId periodId, AbstractUnitIndexId groupId, SharedUnitIndexId unitIndexId, IDictionary<SharedUnitIndexCustomFieldId, string> customFieldValues, bool isInquireable, int calculationOrder, long calculationLevel)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var period = periodRep.GetById(periodId);
                    var sharedUnit = pmsAdminService.GetSharedUnitIndex(unitIndexId);
                    var id = unitIndexRep.GetNextId();
                    if (groupId == null)
                        throw new ArgumentException("groupId is null");
                    var group = unitIndexRep.GetUnitIndexGroupById(groupId);
                    var unitIndex = new UnitIndex(id, period, sharedUnit, group, isInquireable, calculationLevel,
                                                calculationOrder);
                    var validateCustomFldValues = getSharedUnitIndexCustomFields(unitIndexId, customFieldValues);
                    unitIndex.UpdateCustomFields(validateCustomFldValues);
                    unitIndexRep.Add(unitIndex);
                    tr.Complete();
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

        private Dictionary<SharedUnitIndexCustomField, string> getSharedUnitIndexCustomFields(SharedUnitIndexId jabIndexId,
            IDictionary<SharedUnitIndexCustomFieldId, string> fieldIdValues)
        {
            var validateCustomFieldValues = new Dictionary<SharedUnitIndexCustomField, string>();
            var validateCustomFields = pmsAdminService.GetSharedCutomFieldListForUnitIndex(jabIndexId,
                new List<SharedUnitIndexCustomFieldId>(fieldIdValues.Keys));
            foreach (var itm in validateCustomFields)
            {
                validateCustomFieldValues.Add(itm, fieldIdValues[itm.Id]);
            }
            return validateCustomFieldValues;
        }

        public UnitIndexGroup UpdateUnitIndexGroup(AbstractUnitIndexId unitIndexGroupId, AbstractUnitIndexId parentId,
            string name,
            string dictionaryName)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    UnitIndexGroup parent = null;
                    if (parentId != null)
                        parent = unitIndexRep.GetUnitIndexGroupById(parentId);
                    var group = unitIndexRep.GetUnitIndexGroupById(unitIndexGroupId);
                    if (parentId != null && parent.PeriodId != group.PeriodId)
                        throw new ArgumentException("parentId is not valid");
                    group.Update(parent, name, dictionaryName);
                    tr.Complete();
                    return group;
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

        public UnitIndex UpdateUnitIndex(AbstractUnitIndexId unitIndexId, AbstractUnitIndexId groupId, IDictionary<SharedUnitIndexCustomFieldId, string> customFieldValues, bool isInquireable,int calculationOrder, long calculationLevel)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    if (groupId == null)
                        throw new ArgumentException("groupId is null");
                    var group = unitIndexRep.GetUnitIndexGroupById(groupId);
                    if (group == null)
                        throw new ArgumentException("group is null");
                    var unitIndex = unitIndexRep.GetUnitIndexById(unitIndexId);
                    if (group.PeriodId != unitIndex.PeriodId)
                        throw new ArgumentException("groupId is not valid");
                    var customFldValues = getSharedUnitIndexCustomFields(unitIndex.SharedUnitIndexId, customFieldValues);
                    unitIndex.Update(group, isInquireable, customFldValues, periodChecker, calculationOrder,
                                    calculationLevel);
                    unitIndexRep.Update(unitIndex);
                    tr.Complete();
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

        public void DeleteAbstractUnitIndex(AbstractUnitIndexId abstractUnitIndexId)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    unitIndexRep.Delete(abstractUnitIndexId);
                    tr.Complete();
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

        public IList<SharedUnitIndexCustomField> GetSharedUnitIndexCustomField(SharedUnitIndexId sharedUnitIndexId,
            List<SharedUnitIndexCustomFieldId> fieldIdList)
        {
            using (var tr = new TransactionScope())
            {
                var res = pmsAdminService.GetSharedCutomFieldListForUnitIndex(sharedUnitIndexId, fieldIdList);
                tr.Complete();
                return res;
            }
        }

        public IEnumerable<UnitIndex> FindUnitIndices(Expression<Func<UnitIndex, bool>> where)
        {
            using (var tr = new TransactionScope())
            {
                var res = unitIndexRep.FindUnitIndices(where);
                tr.Complete();
                return res;
            }
        }

        public List<AbstractUnitIndex> GetAllAbstractUnitIndexByParentId(AbstractUnitIndexId id)
        {
            using (var tr = new TransactionScope())
            {
                var res = unitIndexRep.GetAllAbstractUnitIndexByParentId(id);
                tr.Complete();
                return res;
            }
        }

        public List<UnitIndexGroup> GetAllParentUnitIndexGroup(Period period)
        {
            using (var tr = new TransactionScope())
            {
                var res = unitIndexRep.GetAllParentUnitIndexGroup(period);
                tr.Complete();
                return res;
            }
        }

        public SharedUnitIndexId GetSharedUnitIndexIdBy(AbstractUnitIndexId abstractUnitIndexId)
        {
            using (var tr = new TransactionScope())
            {
                var res = unitIndexRep.GetSharedUnitIndexIdBy(abstractUnitIndexId);
                tr.Complete();
                return res;
            }
        }

        public AbstractUnitIndexId GetUnitIndexIdBy(Period period, SharedUnitIndexId sharedUnitIndexId)
        {
            using (var tr = new TransactionScope())
            {
                var res = unitIndexRep.GetUnitIndexIdBy(period, sharedUnitIndexId);
                tr.Complete();
                return res;
            }
        }

        public AbstractUnitIndex GetUnitIndexById(AbstractUnitIndexId unitIndexId)
        {
            using (var tr = new TransactionScope())
            {
                var res = unitIndexRep.GetById(unitIndexId);
                tr.Complete();
                return res;
            }
        }

        //public bool IsValidCustomFieldIdList(SharedUnitIndexId unitIndexId, IList<SharedUnitIndexCustomFieldId> customFieldTypeIds)
        //{
        //    //var unitIndex = (UnitIndex)unitIndexRep.GetById(unitIndexId);
        //    //var customFieldList = customFieldRep.Find(c => customFieldTypeIds.Contains(c.Id));
        //    //return unitIndex.IsValidCustomFields(customFieldList);
        //    throw new System.NotImplementedException();
        //}
    }
}
