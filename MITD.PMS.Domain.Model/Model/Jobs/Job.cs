using System.Linq;
using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Domain.Model.Jobs
{
    public class Job : EntityWithDbId<long, JobId>, IEntity<Job>
    {
        #region Fields

        private readonly SharedJob sharedJob;
        private readonly byte[] rowVersion;

        #endregion

        #region Properties

        public virtual JobId Id
        {
            get { return id; }

        }



        public virtual SharedJob SharedJob { get { return sharedJob; } }

        public virtual string Name { get { return sharedJob.Name; } }

        public virtual string DictionaryName { get { return sharedJob.DictionaryName; } }

        private readonly IList<JobCustomField> customFields;
        public virtual IReadOnlyList<JobCustomField> CustomFields
        {
            get { return customFields.ToList().AsReadOnly(); }
        }

        private readonly IList<AbstractJobIndexId> jobIndexIdList;
        public virtual IReadOnlyList<AbstractJobIndexId> JobIndexIdList
        {
            get { return jobIndexIdList.ToList().AsReadOnly(); }
        }

        #endregion

        #region Constructors
        protected Job()
        {
            //For OR mapper
        }

        public Job(Period period, SharedJob sharedJob)
        {
            if (period == null)
                throw new ArgumentNullException("period");
            period.CheckAssigningJob();
            if (sharedJob == null)
                throw new ArgumentNullException("sharedJob");
            id = new JobId(period.Id, sharedJob.Id);

            this.sharedJob = sharedJob;
            customFields = new List<JobCustomField>();
            jobIndexIdList = new List<AbstractJobIndexId>();
        }

        public Job(Period period, SharedJob sharedJob, IList<JobCustomField> customFieldList,
            IList<JobIndex> jobIndexList):this(period,sharedJob)
        {
            assignCustomFields(customFieldList);
            assignJobIndices(jobIndexList);

        }



        #endregion

        #region Public Methods

        private void assignCustomFields(IList<JobCustomField> customFieldList)
        {
           
            foreach (var sharedJobCustomField in customFieldList)
            {
                    AssignSharedCustomField(sharedJobCustomField);
            }

        }

        public virtual void UpdateCustomFields(IList<JobCustomField> customFieldList, IPeriodManagerService periodChecker)
        {
            periodChecker.CheckModifyingJobCustomFields(this);
            foreach (var sharedJobCustomField in customFieldList)
            {
                if (!this.customFields.Contains(sharedJobCustomField))
                    AssignSharedCustomField(sharedJobCustomField);
            }

            IList<JobCustomField> copyOfCustomFields = new List<JobCustomField>(customFields);
            foreach (var itm in copyOfCustomFields)
            {
                if (!customFieldList.Contains(itm))
                    RemoveSharedCustomField(itm);
            }

        }

        public virtual void RemoveSharedCustomField(JobCustomField sharedJobCustomField)
        {
            customFields.Remove(sharedJobCustomField);
        }

        public virtual void AssignSharedCustomField(JobCustomField sharedJobCustomField)
        {
            customFields.Add(sharedJobCustomField);
        }

        private void assignJobIndices(IList<JobIndex> jobIndexList)
        {

                foreach (var itm in jobIndexList)
                {
                        assignJobIndex(itm.Id);
                }


        }


        public virtual void UpdateJobIndices(IList<JobIndex> jobIndexList, IPeriodManagerService periodChecker)
        {
            if (isJobIndicesHaveChanged(jobIndexList))
            {
                
                foreach (var itm in jobIndexList)
                {
                    if (!this.JobIndexIdList.Contains(itm.Id))
                        assignJobIndex(itm.Id);
                }

                IList<AbstractJobIndexId> copyOfJobIndexIdList = new List<AbstractJobIndexId>(jobIndexIdList);
                foreach (var itm in copyOfJobIndexIdList)
                {
                    if (!jobIndexList.Select(index => index.Id).Contains(itm))
                        removeJobIndex(itm);
                }
                periodChecker.CheckModifyingJobIndices(this);
            }

        }

        private void removeJobIndex(AbstractJobIndexId jobIndexId)
        {
            jobIndexIdList.Remove(jobIndexId);
        }

        private void assignJobIndex(AbstractJobIndexId jobIndexId)
        {
            jobIndexIdList.Add(jobIndexId);
        }

        private bool isJobIndicesHaveChanged(IList<JobIndex> jobIndexList)
        {
            if (jobIndexIdList != null && jobIndexIdList.Count > 0 &&
                (!jobIndexList.All(j => jobIndexIdList.Contains(j.Id)) ||
                 !jobIndexIdList.All(j => jobIndexList.Select(ji => ji.Id).Contains(j))))
                return true;
            return false;
        }

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(Job other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


        //public virtual void AssignCustomField(CustomFieldType customFieldType)
        //{
        //    if (customFieldTypeIdList == null)
        //        customFieldTypeIdList = new List<CustomFieldTypeId>();

        //    if (customFieldType != null && customFieldType.EntityId == EntityTypeEnum.JobInPeriod)
        //        customFieldTypeIdList.Add(customFieldType.CustomFieldTypeId);
        //}



        //public virtual IList<CustomFieldTypeId> GetAssignedCustomFieldId()
        //{
        //    return customFieldTypeIdList;
        //}

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Job)obj;
            return SameIdentityAs(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        //public virtual bool ValidateCustomField(ICustomFieldValidateService<JobInPeriod> customFieldValidateService, List<CustomFieldTypeId> customFieldTypeIds)
        //{
        //    return customFieldValidateService.Validate(customFieldTypeIds);
        //}

        public override string ToString()
        {
            return Id.ToString();
        }

        #endregion


    }
}
