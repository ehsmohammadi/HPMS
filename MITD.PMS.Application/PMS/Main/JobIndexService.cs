using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Service;
using System.Transactions;
using MITD.PMS.Exceptions;


namespace MITD.PMS.Application
{
    public class JobIndexService : IJobIndexService
    {
        private readonly IPeriodRepository periodRep;
        private readonly IPMSAdminService pmsAdminService;
        private readonly IPeriodManagerService periodChecker;
        private readonly IJobIndexRepository jobIndexRep;

        public JobIndexService(

            IJobIndexRepository jobIndexRep,
            IPeriodRepository periodRep,
            IPMSAdminService ipmsAdminService,
            IPeriodManagerService periodChecker
            )
        {
            this.periodRep = periodRep;
            this.pmsAdminService = ipmsAdminService;
            this.periodChecker = periodChecker;
            this.jobIndexRep = jobIndexRep;
        }

        public JobIndexGroup AddJobIndexGroup(PeriodId periodId, AbstractJobIndexId parentId, string name,
            string dictionaryName)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var id = jobIndexRep.GetNextId();
                    var period = periodRep.GetById(periodId);
                    JobIndexGroup parent = null;
                    if (parentId != null)
                        parent = jobIndexRep.GetJobIndexGroupById(new AbstractJobIndexId(parentId.Id));
                    var jobIndexGroup = new JobIndexGroup(id, period, parent, name, dictionaryName);
                    jobIndexRep.Add(jobIndexGroup);
                    tr.Complete();
                    return jobIndexGroup;

                }
            }
            catch (Exception exp)
            {
                var res = jobIndexRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public JobIndex AddJobIndex(PeriodId periodId, AbstractJobIndexId groupId, SharedJobIndexId jobIndexId, IDictionary<SharedJobIndexCustomFieldId, string> customFieldValues, bool isInquireable, int calculationOrder, long calculationLevel)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var period = periodRep.GetById(periodId);
                    var sharedJob = pmsAdminService.GetSharedJobIndex(jobIndexId);
                    var id = jobIndexRep.GetNextId();
                    if (groupId == null)
                        throw new ArgumentException("groupId is null");
                    var group = jobIndexRep.GetJobIndexGroupById(groupId);
                    var jobIndex = new JobIndex(id, period, sharedJob, group, isInquireable, calculationLevel,
                                                calculationOrder);
                    var validateCustomFldValues = getSharedJobIndexCustomFields(jobIndexId, customFieldValues);
                    jobIndex.UpdateCustomFields(validateCustomFldValues);
                    jobIndexRep.Add(jobIndex);
                    tr.Complete();
                    return jobIndex;
                }
            }
            catch (Exception exp)
            {
                var res = jobIndexRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        private Dictionary<SharedJobIndexCustomField, string> getSharedJobIndexCustomFields(SharedJobIndexId jabIndexId,
            IDictionary<SharedJobIndexCustomFieldId, string> fieldIdValues)
        {
            var validateCustomFieldValues = new Dictionary<SharedJobIndexCustomField, string>();
            var validateCustomFields = pmsAdminService.GetSharedCutomFieldListForJobIndex(jabIndexId,
                new List<SharedJobIndexCustomFieldId>(fieldIdValues.Keys));
            foreach (var itm in validateCustomFields)
            {
                validateCustomFieldValues.Add(itm, fieldIdValues[itm.Id]);
            }
            return validateCustomFieldValues;
        }

        public JobIndexGroup UpdateJobIndexGroup(AbstractJobIndexId jobIndexGroupId, AbstractJobIndexId parentId,
            string name,
            string dictionaryName)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    JobIndexGroup parent = null;
                    if (parentId != null)
                        parent = jobIndexRep.GetJobIndexGroupById(parentId);
                    var group = jobIndexRep.GetJobIndexGroupById(jobIndexGroupId);
                    if (parentId != null && parent.PeriodId != group.PeriodId)
                        throw new ArgumentException("parentId is not valid");
                    group.Update(parent, name, dictionaryName);
                    tr.Complete();
                    return group;
                }
            }
            catch (Exception exp)
            {
                var res = jobIndexRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public JobIndex UpdateJobIndex(AbstractJobIndexId jobIndexId, AbstractJobIndexId groupId, IDictionary<SharedJobIndexCustomFieldId, string> customFieldValues, bool isInquireable,int calculationOrder, long calculationLevel)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    if (groupId == null)
                        throw new ArgumentException("groupId is null");
                    var group = jobIndexRep.GetJobIndexGroupById(groupId);
                    if (group == null)
                        throw new ArgumentException("group is null");
                    var jobIndex = jobIndexRep.GetJobIndexById(jobIndexId);
                    if (group.PeriodId != jobIndex.PeriodId)
                        throw new ArgumentException("groupId is not valid");
                    var customFldValues = getSharedJobIndexCustomFields(jobIndex.SharedJobIndexId, customFieldValues);
                    jobIndex.Update(group, isInquireable, customFldValues, periodChecker, calculationOrder,
                                    calculationLevel);
                    jobIndexRep.Update(jobIndex);
                    tr.Complete();
                    return jobIndex;
                }
            }
            catch (Exception exp)
            {
                var res = jobIndexRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public void DeleteAbstractJobIndex(AbstractJobIndexId abstractJobIndexId)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    jobIndexRep.Delete(abstractJobIndexId);
                    tr.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = jobIndexRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public IList<SharedJobIndexCustomField> GetSharedJobIndexCustomField(SharedJobIndexId sharedJobIndexId,
            List<SharedJobIndexCustomFieldId> fieldIdList)
        {
            using (var tr = new TransactionScope())
            {
                var res = pmsAdminService.GetSharedCutomFieldListForJobIndex(sharedJobIndexId, fieldIdList);
                tr.Complete();
                return res;
            }
        }

        public IEnumerable<JobIndex> FindJobIndices(Expression<Func<JobIndex, bool>> where)
        {
            using (var tr = new TransactionScope())
            {
                var res = jobIndexRep.FindJobIndices(where);
                tr.Complete();
                return res;
            }
        }

        public List<AbstractJobIndex> GetAllAbstractJobIndexByParentId(AbstractJobIndexId id)
        {
            using (var tr = new TransactionScope())
            {
                var res = jobIndexRep.GetAllAbstractJobIndexByParentId(id);
                tr.Complete();
                return res;
            }
        }

        public List<JobIndexGroup> GetAllParentJobIndexGroup(Period period)
        {
            using (var tr = new TransactionScope())
            {
                var res = jobIndexRep.GetAllParentJobIndexGroup(period);
                tr.Complete();
                return res;
            }
        }

        public SharedJobIndexId GetSharedJobIndexIdBy(AbstractJobIndexId abstractJobIndexId)
        {
            using (var tr = new TransactionScope())
            {
                var res = jobIndexRep.GetSharedJobIndexIdBy(abstractJobIndexId);
                tr.Complete();
                return res;
            }
        }

        public AbstractJobIndexId GetJobIndexIdBy(Period period, SharedJobIndexId sharedJobIndexId)
        {
            using (var tr = new TransactionScope())
            {
                var res = jobIndexRep.GetJobIndexIdBy(period, sharedJobIndexId);
                tr.Complete();
                return res;
            }
        }

        public AbstractJobIndex GetJobIndexById(AbstractJobIndexId jobIndexId)
        {
            using (var tr = new TransactionScope())
            {
                var res = jobIndexRep.GetById(jobIndexId);
                tr.Complete();
                return res;
            }
        }

        //public bool IsValidCustomFieldIdList(SharedJobIndexId jobIndexId, IList<SharedJobIndexCustomFieldId> customFieldTypeIds)
        //{
        //    //var jobIndex = (JobIndex)jobIndexRep.GetById(jobIndexId);
        //    //var customFieldList = customFieldRep.Find(c => customFieldTypeIds.Contains(c.Id));
        //    //return jobIndex.IsValidCustomFields(customFieldList);
        //    throw new System.NotImplementedException();
        //}
    }
}
