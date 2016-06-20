using System;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PeriodDTO : PeriodDescriptionDTO
    {
        public string PutActionName { get; set; }
        public bool ActiveStatus { get; set; }

        private DateTime startDate;
        //[Range(typeof(DateTime), "2014/1/1", "2020/1/1", ErrorMessage = "تاریخ شروع معتبر نمی باشد")]
        public DateTime StartDate
        {
            get { return startDate; }
            set { this.SetField(p => p.StartDate, ref startDate, value); }
        }

        private DateTime endDate;
        //[Range(typeof(DateTime), "2014/1/1", "2020/1/1", ErrorMessage = "تاریخ پایان معتبر نمی باشد")]
        public DateTime EndDate
        {
            get { return endDate; }
            set { this.SetField(p => p.EndDate, ref endDate, value); }
        }

        private string stateName;
        public string StateName
        {
            get { return stateName; }
            set { this.SetField(p => p.StateName, ref stateName, value); }
        }

        private decimal maxFinalPoint;
        public decimal MaxFinalPoint
        {
            get { return maxFinalPoint; }
            set { this.SetField(p => p.MaxFinalPoint, ref maxFinalPoint, value); }
        }
    }
}
