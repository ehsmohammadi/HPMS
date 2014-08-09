using System;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PeriodDTO :PeriodDescriptionDTO
    {
        private DateTime startDate;
        private DateTime endDate;
        private string stateName;

        public string PutActionName { get; set; }
        public bool ActiveStatus { get; set; }

        [Range(typeof(DateTime), "1900/1/1", "2100/1/1", ErrorMessage = "تاریخ شروع معتبر نمی باشد")]
        public DateTime StartDate
        {
            get { return startDate; }
            set { this.SetField(p => p.StartDate, ref startDate, value); }
        }

          [Range(typeof(DateTime), "1900/1/1", "2100/1/1", ErrorMessage = "تاریخ پایان معتبر نمی باشد")]
        public DateTime EndDate
        {
            get { return endDate; }
            set { this.SetField(p => p.EndDate, ref endDate, value); }
        }

        public string StateName
        {
            get { return stateName; }
            set { this.SetField(p => p.StateName, ref stateName, value); }
        }

        
    }
}
