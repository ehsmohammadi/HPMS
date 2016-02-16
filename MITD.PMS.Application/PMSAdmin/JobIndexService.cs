using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Repository;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.JobIndices;
using System.Transactions;
using MITD.PMSAdmin.Exceptions;


namespace MITD.PMSAdmin.Application
{
    public class JobIndexService : IJobIndexService
    {
        private readonly IJobIndexRepository jobIndexRep;
        private readonly ICustomFieldRepository customFieldRep;


        public JobIndexService(IJobIndexRepository jobIndexRep
            , ICustomFieldRepository customFieldRep)
        {
            this.jobIndexRep = jobIndexRep;
            this.customFieldRep = customFieldRep;
        }


        public JobIndexCategory AddJobIndexCategory(AbstractJobIndexId parentId, string name, string dictionaryName)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var id = jobIndexRep.GetNextId();
                    JobIndexCategory parent = null;
                    if (parentId != null)
                        parent = jobIndexRep.GetJobIndexCategory(new AbstractJobIndexId(parentId.Id));
                    var jobIndexCategory = new JobIndexCategory(id, parent, name, dictionaryName);
                    jobIndexRep.Add(jobIndexCategory);
                    scope.Complete();
                    return jobIndexCategory;

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


        public JobIndex AddJobIndex(AbstractJobIndexId categoryId, string name
            , string dictionaryName, IList<CustomFieldTypeId> customFieldTypeIdList, Guid transferId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var id = jobIndexRep.GetNextId();
                    var category = jobIndexRep.GetJobIndexCategory(categoryId);
                    var jobIndex = new JobIndex(id, category, name, dictionaryName);
                    assignJobIndexCustomField(jobIndex, customFieldTypeIdList);
                    jobIndex.TransferId = transferId;
                    jobIndexRep.Add(jobIndex);
                    scope.Complete();
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

        public JobIndexCategory UpdateJobIndexCategory(AbstractJobIndexId jobIndexCatId, AbstractJobIndexId parentId, string name, string dictionaryName)
        {
            //todo:(medium)Update not working 
            try
            {
                using (var scope = new TransactionScope())
                {
                    JobIndexCategory parent = null;
                    if (parentId != null)
                        parent = jobIndexRep.GetJobIndexCategory(parentId);
                    var jobIndexCategory = new JobIndexCategory(jobIndexCatId, parent, name, dictionaryName);
                    jobIndexRep.Update(jobIndexCategory);
                    scope.Complete();
                    return jobIndexCategory;

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

        public JobIndex UpdateJobIndex(AbstractJobIndexId jobIndexId, AbstractJobIndexId categoryId, string name
            , string dictionaryName, IList<CustomFieldTypeId> customFieldTypeIdList)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    if (categoryId == null)
                        throw new ArgumentException("CategoryId is null");
                    var category = jobIndexRep.GetJobIndexCategory(categoryId);
                    var jobIndex = jobIndexRep.GetJobIndex(jobIndexId);
                    var jobIndexCustomFields = customFieldRep.GetAll(EntityTypeEnum.JobIndex)
                            .Where(c => customFieldTypeIdList.Contains(c.Id)).ToList();
                    jobIndex.Update(name, dictionaryName, category, jobIndexCustomFields);
                    jobIndexRep.Update(jobIndex);
                    scope.Complete();
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

        private void assignJobIndexCustomField(JobIndex jobIndex, IList<CustomFieldTypeId> customFieldTypeIdList)
        {
            foreach (var customFieldId in customFieldTypeIdList)
            {
                var customField = customFieldRep.GetById(customFieldId);
                jobIndex.AssignCustomField(customField);
            }
        }

        public void DeleteAbstractJobIndex(AbstractJobIndexId jobIndexId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    jobIndexRep.Delete(jobIndexId);
                    scope.Complete();
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

        public bool IsValidCustomFieldIdList(AbstractJobIndexId jobIndexId, IList<CustomFieldTypeId> customFieldTypeIds)
        {
            var jobIndex = (JobIndex)jobIndexRep.GetById(jobIndexId);

            var customFieldList = customFieldRep.Find(customFieldTypeIds);
            return jobIndex.IsValidCustomFields(customFieldList);
        }

        public JobIndex GetBy(AbstractJobIndexId jobIndexId)
        {
            return jobIndexRep.GetJobIndex(jobIndexId);
        }
    }
}
