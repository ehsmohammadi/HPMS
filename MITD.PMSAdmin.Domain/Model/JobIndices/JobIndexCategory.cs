using System;
using MITD.Domain.Model;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.JobIndices
{
    public class JobIndexCategory:AbstractJobIndex
    {

        #region Fields

        #endregion

        #region Properties

       
        private JobIndexCategory parent;
        public virtual JobIndexCategory Parent { get { return parent; } }

       
        #endregion

        #region Constructors

        protected JobIndexCategory()
        {

        }

        public JobIndexCategory(AbstractJobIndexId jobIndexCategoryId, JobIndexCategory parent, string name,
                                string dictionaryName)
            : base(jobIndexCategoryId,  name, dictionaryName)
        {
            this.parent = parent;
        }

       

        #endregion

        #region Public Methods
        public virtual void Update(JobIndexCategory parent, string name, string dictionaryName)
        {
            this.parent = parent;

            if (string.IsNullOrWhiteSpace(name))
                throw new JobIndexArgumentException("JobIndex", "Name");
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new JobIndexArgumentException("JobIndex", "DictionaryName");
            this.dictionaryName = dictionaryName;
        }

        #endregion

    }
}
