using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeJobPositionAssignmentDTO:IActionDTO
    {
        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }

        private string jobPositionName;    
        public string JobPositionName
        {
            get { return jobPositionName; }
            set { this.SetField(p => p.JobPositionName, ref jobPositionName, value); }
        }

        private DateTime fromDate;
        [Range(typeof(DateTime), "1900/1/1", "2100/1/1", ErrorMessage = "تاریخ  معتبر نمی باشد")]
        public DateTime FromDate
        {
            get { return fromDate; }
            set { this.SetField(p => p.FromDate, ref fromDate, value); }
        }

        private DateTime toDate;
          [Range(typeof(DateTime), "1900/1/1", "2100/1/1", ErrorMessage = "تاریخ  معتبر نمی باشد")]
        public DateTime ToDate
        {
            get { return toDate; }
            set { this.SetField(p => p.ToDate, ref toDate, value); }
        }

        private long jobPositionId;
         [Range(1, Int64.MaxValue, ErrorMessage = "انتخاب پست الزامی است")]
        public long JobPositionId
        {
            get { return jobPositionId; }
            set { this.SetField(p => p.JobPositionId, ref jobPositionId, value); }
        }

        private int workTimePercent;
        [Range(1,100,ErrorMessage="عدد ورودی بین 1 و100 باید باشد")]
        public int WorkTimePercent
        {
            get { return workTimePercent; }
            set { this.SetField(p => p.WorkTimePercent, ref workTimePercent, value); }
        }

        private int jobPositionWeight;
        [Range(0, 100, ErrorMessage = "عدد ورودی بین 0 و100 باید باشد")]
        public int JobPositionWeight
        {
            get { return jobPositionWeight; }
            set { this.SetField(p => p.JobPositionWeight, ref jobPositionWeight, value); }
        }

        public List<int> ActionCodes { get; set; }

        
    }

}
