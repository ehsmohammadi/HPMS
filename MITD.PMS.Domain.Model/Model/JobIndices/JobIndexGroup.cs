using System;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.JobIndices
{
    public class JobIndexGroup:AbstractJobIndex
    {


       

        #region Fields


        #endregion

        #region Properties

        private string name;
        public virtual string Name { get { return name; } }

        private string dictionaryName;
        public virtual string DictionaryName { get { return dictionaryName; } }
      
       
        private  JobIndexGroup parent;
        public virtual JobIndexGroup Parent { get { return parent; } }

       
        #endregion

        #region Constructors

        protected JobIndexGroup()
        {

        }

        public JobIndexGroup(AbstractJobIndexId jobIndexGroupId,Period period, JobIndexGroup parent, string name,
                                string dictionaryName)
        {
            if (jobIndexGroupId == null)
                throw new ArgumentNullException("jobIndexGroupId");
            if (period == null)
                throw new ArgumentNullException("period");

            this.parent = parent;
            periodId = period.Id;
            id = jobIndexGroupId;
            this.name = name;
            this.dictionaryName = dictionaryName;
        }

        public virtual void Update(JobIndexGroup parent, string name, string dictionaryName)
        {
            this.name = name;
            this.dictionaryName = dictionaryName;
            this.parent = parent;
        }
       

        #endregion

        #region Public Methods
      
        #endregion

    }
}
