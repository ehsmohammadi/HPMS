using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Repository;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.Jobs;
using System.Transactions;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Application
{
    public class JobService : IJobService
    {
        private readonly IJobRepository jobRep;
        private readonly ICustomFieldRepository customFieldRep;

        public JobService(IJobRepository jobRep, ICustomFieldRepository customFieldRep)
        {
            this.jobRep = jobRep;
            this.customFieldRep = customFieldRep;
        }


        public Job AddJob(string jobName, string dictionaryName, List<CustomFieldTypeId> customFieldTypeIdList, Guid transferId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    JobId jobId = jobRep.GetNextId();
                    var job = new Job(jobId, jobName, dictionaryName);
                    job.TransferId = transferId;
                    assignJobCustomField(job, customFieldTypeIdList);
                    jobRep.AddJob(job);
                    scope.Complete();
                    return job;
                }
            }
            catch (Exception exp)
            {
                var res = jobRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Job UppdateJob(JobId jobId, string name, string dictionaryName, IList<CustomFieldTypeId> customFieldTypeIdList)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var job = jobRep.GetById(jobId);
                    var jobCustomFields = customFieldRep.GetAll(EntityTypeEnum.Job)
                            .Where(c => customFieldTypeIdList.Contains(c.Id)).ToList();
                    job.Update(name, dictionaryName, jobCustomFields);
                    jobRep.UpdateJob(job);
                    scope.Complete();
                    return job;

                }
            }
            catch (Exception exp)
            {
                var res = jobRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        private void assignJobCustomField(Job job, IList<CustomFieldTypeId> customFieldTypeIdList)
        {
            foreach (var customFieldId in customFieldTypeIdList)
            {
                var customField = customFieldRep.GetById(customFieldId);
                job.AssignCustomField(customField);
            }
        }


        public void DeleteJob(JobId jobId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var job = jobRep.GetById(jobId);
                    jobRep.DeleteJob(job);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = jobRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Job GetBy(JobId jobId)
        {
            return jobRep.GetById(jobId);
        }

        public bool IsValidCustomFieldIdList(JobId jobId, IList<CustomFieldTypeId> customFieldTypeIds)
        {
            var job = jobRep.GetById(jobId);
            var customFieldList = customFieldRep.Find(customFieldTypeIds);
            return job.IsValidCustomFields(customFieldList);
        }
    }
}
