
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobIndexValueDTO
    {
        
        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }


        private long jobIndexId;
        public long JobIndexId
        {
            get { return jobIndexId; }
            set { this.SetField(p => p.JobIndexId, ref jobIndexId, value); }
        }

        private string jobIndexName;
        public string JobIndexName
        {
            get { return jobIndexName; }
            set { this.SetField(p => p.JobIndexName, ref jobIndexName, value); }
        }

        private string indexValue;
        [Range(0,9,ErrorMessage="برای فیلد مورد نظر مقدار درست وارد کنید")]
        public string IndexValue
        {
            get { return indexValue; }
            set { this.SetField(p => p.IndexValue, ref indexValue, value); }
        }


       
    }
}
