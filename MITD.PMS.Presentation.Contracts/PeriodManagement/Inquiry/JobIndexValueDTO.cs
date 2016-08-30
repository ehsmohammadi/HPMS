
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
        //[Range(20, 100, ErrorMessage = "برای فیلد مورد نظر مقدار درست وارد کنید")]
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

    public partial class EmployeeValueDTO
    {

        private string employeeNo;
        public string EmployeeNo
        {
            get { return employeeNo; }
            set { this.SetField(p => p.EmployeeNo, ref employeeNo, value); }
        }

        private long jobPositionId;
        public long JobPositionId
        {
            get { return jobPositionId; }
            set { this.SetField(p => p.JobPositionId, ref jobPositionId, value); }
        }

        private string jobPositionName;
        public string JobPositionName
        {
            get { return jobPositionName; }
            set { this.SetField(p => p.JobPositionName, ref jobPositionName, value); }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { this.SetField(p => p.FullName, ref fullName, value); }
        }


        private string inquireEmployeeNo;
        public string InquireEmployeeNo
        {
            get { return inquireEmployeeNo; }
            set { this.SetField(p => p.InquireEmployeeNo, ref inquireEmployeeNo, value); }
        }
        private long inquirerJobPositionId;
        public long InquirerJobPositionId
        {
            get { return inquirerJobPositionId; }
            set { this.SetField(p => p.InquirerJobPositionId, ref inquirerJobPositionId, value); }
        }

        private string inquirerJobPositionName;
        public string InquirerJobPositionName
        {
            get { return inquirerJobPositionName; }
            set { this.SetField(p => p.InquirerJobPositionName, ref inquirerJobPositionName, value); }
        }
        private string indexValue;
        [Range(20, 100, ErrorMessage = "برای فیلد مورد نظر مقدار درست وارد کنید")]
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
