using System.Collections.Generic;
using System.Data;
using System.Linq;
using MITD.Domain.Model;
using System;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;
using MITD.PMSReport.Domain.Model;

namespace MITD.PMS.Domain.Model.JobPositions
{
    public class JobPosition : EntityWithDbId<long,JobPositionId>, IEntity<JobPosition>
    {
        #region Fields

        private readonly byte[] rowVersion;
        private readonly SharedJobPosition sharedJobPosition;

        #endregion

        #region Properties

        public virtual JobPositionId Id
        {
            get { return id; }

        }

        public virtual SharedJobPosition SharedJobPosition { get { return sharedJobPosition; } }

        public virtual string Name { get { return sharedJobPosition.Name; } }

        public virtual string DictionaryName { get { return sharedJobPosition.DictionaryName; } }

        public virtual Guid TransferId { get { return sharedJobPosition.TransferId; } }

        private readonly UnitId unitId;
        public virtual UnitId UnitId
        {
            get { return unitId; }
        }

        private readonly JobId jobId;
        public virtual JobId JobId
        {
            get { return jobId; }
        }

        private readonly JobPosition parent;
        public virtual JobPosition Parent
        {
            get { return parent; }
        }

        private IList<JobPositionEmployee> employees = new List<JobPositionEmployee>();
        public virtual IReadOnlyList<JobPositionEmployee> Employees
        {
            get { return employees.ToList().AsReadOnly(); }
        }

        private IList<JobPositionInquiryConfigurationItem> configurationItemList = new List<JobPositionInquiryConfigurationItem>();

        public virtual IReadOnlyList<JobPositionInquiryConfigurationItem> ConfigurationItemList
        {
            get { return configurationItemList.ToList().AsReadOnly(); }
        }

        #endregion

        #region Constructors
        protected JobPosition()
        {
            //For OR mapper
        }

        public JobPosition(Period period, SharedJobPosition sharedJobPosition, JobPosition parent, Job job, Unit unit)
        {
            if (period == null || period.Id==null)
                throw new ArgumentNullException("period");
            period.CheckAssigningJobPosition();
            if (sharedJobPosition == null || sharedJobPosition.Id == null)
                throw new ArgumentNullException("sharedJobPosition");
            if (job == null || job.Id == null)
                throw new ArgumentNullException("job");
            if (unit == null || unit.Id == null)
                throw new ArgumentNullException("unit");
            
            
            if (!period.Id.Equals(job.Id.PeriodId))
                throw new JobPositionCompareException("JobPosition","Job","Period");
            
            if (!period.Id.Equals(unit.Id.PeriodId))
                throw new JobPositionCompareException("JobPosition", "Unit", "Period");

            id = new JobPositionId(period.Id, sharedJobPosition.Id);
            this.sharedJobPosition = sharedJobPosition;
            this.parent = parent;
            unitId = unit.Id;
            jobId = job.Id;
        }
        #endregion

        #region Public Methods

        public virtual void ConfigeInquirer(IJobPositionInquiryConfiguratorService inquiryConfiguratorService, bool forceConfigure)
        {
            if (!forceConfigure && configurationItemList.Count > 0)
                return;
            configurationItemList.Clear();

            var configurationItems = inquiryConfiguratorService.Configure(this);
            foreach (var itm in configurationItems)
            {
                configurationItemList.Add(itm);
            }

        }

        //public virtual void AddCustomInquirer(Employee inquirer,Employee inquirySubject)
        //{
        //    var inquirerJobPosition = inquirer.JobPositions.First();
        //        configurationItemList.Add(
        //            new JobPositionInquiryConfigurationItem(
        //                new JobPositionInquiryConfigurationItemId(inquirerJobPosition.JobPositionId,inquirer.Id, Id, inquirySubject.Id), this, false, true,JobPositionLevel.None));

        //}

        public virtual void AddCustomInquirer(EmployeeIdWithJobPositionId inquirer, Employee inquirySubject)
        {
            configurationItemList.Add(
                new JobPositionInquiryConfigurationItem(
                    new JobPositionInquiryConfigurationItemId(inquirer.JobPositionId, inquirer.EmployeeId, Id, inquirySubject.Id), this, false, true, JobPositionLevel.None));
        }

        public virtual void UpdateInquirersBy(Employee inquirySubject, List<EmployeeIdWithJobPositionId> inquirerList, IPeriodManagerService periodChecker)
        {
            periodChecker.CheckModifyingJobPositionInquirers(inquirySubject);
            configurationItemList.Where(c => c.IsAutoGenerated&&c.Id.InquirySubjectId.Equals(inquirySubject.Id)).ToList().ForEach(c=>c.SetPermitted(false));
            foreach (var inquirer in inquirerList)
            {
                var itm = configurationItemList.SingleOrDefault(c => c.Id.InquirySubjectId.Equals(inquirySubject.Id) &&
                    c.Id.InquirerId.Equals(inquirer.EmployeeId) && c.Id.InquirerJobPositionId.Equals(inquirer.JobPositionId));
                if (itm != null)
                    itm.SetPermitted(true);
            }
            //var customConfigurationItems = configurationItemList.Where(c => c.Id.InquirySubjectId.Equals(inquirySubject.Id)).ToList();
            var allConfigurationItems = configurationItemList.Where(c => c.Id.InquirySubjectId.Equals(inquirySubject.Id) ).ToList();
            foreach (var inquirer in inquirerList)
            {
                //if (!autoConfigurationItems.Select(c => c.Id.InquirerId).Contains(inquirer.EmployeeId))
                if (!allConfigurationItems.Any(c => c.Id.InquirerId == inquirer.EmployeeId && c.Id.InquirerJobPositionId == inquirer.JobPositionId))
                    AddCustomInquirer(inquirer, inquirySubject);
            }

            var copyOfItems = new List<JobPositionInquiryConfigurationItem>(configurationItemList.Where(c => c.Id.InquirySubjectId.Equals(inquirySubject.Id) && !c.IsAutoGenerated));
            foreach (var itm in copyOfItems)
            {
                //if (!inquirerList.Select(i => i.EmployeeId).Contains(itm.Id.InquirerId))
                if (!inquirerList.Any(i => i.EmployeeId == itm.Id.InquirerId && i.JobPositionId == itm.Id.InquirerJobPositionId))
                {
                    configurationItemList.Remove(itm);
                }
            }


        }

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(JobPosition other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (JobPosition)obj;
            return SameIdentityAs(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }



        #endregion


    }
}
