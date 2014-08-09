using System;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class ClaimDescriptionDTO
    {
        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }

        private long periodId;
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }

        private string employeeNo;
        public string EmployeeNo
        {
            get { return employeeNo; }
            set { this.SetField(p => p.EmployeeNo, ref employeeNo, value); }
        }

        private DateTime claimDate = DateTime.Now;
        [Required(AllowEmptyStrings = false, ErrorMessage = "تاریخ درخواست اعتراض الزامی می باشد")]
        public DateTime ClaimDate
        {
            get { return claimDate; }
            set { this.SetField(p => p.ClaimDate, ref claimDate, value); }
        }

        private DateTime? responseDate;
        public DateTime? ResponseDate
        {
            get { return responseDate; }
            set { this.SetField(p => p.ResponseDate, ref responseDate, value); }
        }


        private string title = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "عنوان درخواست اعتراض الزامی می باشد")]
        public string Title
        {
            get { return title; }
            set { this.SetField(p => p.Title, ref title, value); }
        }

        private ClaimStateDTO claimState;
        public ClaimStateDTO ClaimState
        {
            get { return claimState; }
            set { this.SetField(p => p.ClaimState, ref claimState, value); }
        }

        private int claimStateId;
        public int ClaimStateId
        {
            get { return claimStateId; }
            set { this.SetField(p => p.ClaimStateId, ref claimStateId, value); }
        }


        private ClaimTypeDTO claimType;
        public ClaimTypeDTO ClaimType
        {
            get { return claimType; }
            set { this.SetField(p => p.ClaimType, ref claimType, value); }
        }

        private int claimTypeId;
        [Range(1, 10, ErrorMessage = "نوع درخواست اعتراض الزامی می باشد")]
        public int ClaimTypeId
        {
            get { return claimTypeId; }
            set { this.SetField(p => p.ClaimTypeId, ref claimTypeId, value); }
        }

        private string claimTypeName;
        public string ClaimTypeName
        {
            get { return claimTypeName; }
            set { this.SetField(p => p.ClaimTypeName, ref claimTypeName, value); }
        }

        

    }
}
