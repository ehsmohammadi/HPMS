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

        private readonly IList<JobJobIndex> jobIndexList;
        public virtual IReadOnlyList<JobJobIndex> JobIndexList
        {
            get { return jobIndexList.ToList().AsReadOnly(); }
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
            jobIndexList = new List<JobJobIndex>();
        }

        public Job(Period period, SharedJob sharedJob, IList<JobCustomField> customFieldList,
            IList<JobJobIndex> jobIndexList):this(period,sharedJob)
        {
            assignCustomFields(customFieldList);
            assignJobIndices(jobIndexList);

        }



        #endregion

        #region Public Methods

        private void assignJobIndices(IList<JobJobIndex> jobIndexList)
        {
            foreach (var itm in jobIndexList)
            {
                assignJobIndex(itm);
            }
        }

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

        public virtual void UpdateJobIndices(IList<JobJobIndex> jobIndexList, IPeriodManagerService periodChecker)
        {
            if (isJobIndicesHaveChanged(jobIndexList))
            {
                
                foreach (var itm in jobIndexList)
                {
                    if (!this.JobIndexList.Contains(itm))
                        assignJobIndex(itm);
                }

                IList<JobJobIndex> copyOfJobIndexIdList = new List<JobJobIndex>(this.jobIndexList);
                foreach (var itm in copyOfJobIndexIdList)
                {
                    if (!jobIndexList.Contains(itm))
                        removeJobIndex(itm);
                }
                periodChecker.CheckModifyingJobIndices(this);
            }

        }

        private void removeJobIndex(JobJobIndex jobIndex)
        {
            jobIndexList.Remove(jobIndex);
        }

        private void assignJobIndex(JobJobIndex jobIndex)
        {
            jobIndexList.Add(jobIndex);
        }

        private bool isJobIndicesHaveChanged(IList<JobJobIndex> jobIndexList)
        {
            if (this.jobIndexList != null && this.jobIndexList.Count > 0 &&
                (!jobIndexList.All(j => this.jobIndexList.Contains(j)) ||
                 !this.jobIndexList.All(j => jobIndexList.Select(ji => ji).Contains(j))))
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
