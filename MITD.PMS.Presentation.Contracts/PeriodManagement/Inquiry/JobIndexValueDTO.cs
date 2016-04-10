
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
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
        [Range(0, 10, ErrorMessage = "برای فیلد مورد نظر مقدار درست وارد کنید")]
        public string IndexValue
        {
            get { return indexValue; }
            set { this.SetField(p => p.IndexValue, ref indexValue, value); }
        }

        private List<Grade> grades;
        public List<Grade> Grades
        {
            get { return grades; }
            set { this.SetField(vm => vm.Grades, ref grades, value); }
        }

    }

    public class Grade
    {
        public Grade(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
