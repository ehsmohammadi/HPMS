using System;
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobInPeriodDTO 
    {
        private string name;
        private string dictionaryName;
        private long jobId;


        public long JobId
        {
            get { return jobId; }
            set { this.SetField(p => p.JobId, ref jobId, value); }
        }

        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        public string DictionaryName
        {
            get { return dictionaryName; }
            set { this.SetField(p => p.DictionaryName, ref dictionaryName, value); }
        }

        private List<CustomFieldDTO> customFields = new List<CustomFieldDTO>();
        public List<CustomFieldDTO> CustomFields
        {
            get { return customFields; }
            set { this.SetField(p => p.CustomFields, ref customFields, value); }
        }

        private List<JobIndexInPeriodDTO> jobIndices = new List<JobIndexInPeriodDTO>();
        public List<JobIndexInPeriodDTO> JobIndices
        {
            get { return jobIndices; }
            set { this.SetField(p => p.JobIndices, ref jobIndices, value); }
        }



    }
}
