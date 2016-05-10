using MITD.Presentation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeDTO
    {
        private string firstName;
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام را وارد نمایید")]
        public string FirstName
        {
            get { return firstName; }
            set { this.SetField(p => p.FirstName, ref firstName, value); }
        }

        private string lastName;
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام خانوادگی را وارد نمایید")]
        public string LastName
        {
            get { return lastName; }
            set { this.SetField(p => p.LastName, ref lastName, value); }
        }

        private string personnelNo;
        [Required(AllowEmptyStrings = false, ErrorMessage = "کد پرسنلی را وارد نمایید")]
        public string PersonnelNo
        {
            get { return personnelNo; }
            set { this.SetField(p => p.PersonnelNo, ref personnelNo, value); }
        }

        private decimal finalPoint;
        public decimal FinalPoint
        {
            get { return finalPoint; }
            set { this.SetField(p => p.FinalPoint, ref finalPoint, value); }
        }

        private long periodId;
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }
 
        private List<CustomFieldValueDTO> customFields;
        public List<CustomFieldValueDTO> CustomFields
        {
            get { return customFields; }
            set { this.SetField(p => p.CustomFields, ref customFields, value); }
        }
        
    }

}
