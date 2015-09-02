using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Units;

namespace MITD.PMSReport.Domain.Model
{
    public class InquirySubjectWithUnit
    {
        public Employee InquirerUnit { get; set; }
        public Unit InquirySubject { get; set; }
        
       public UnitUnitIndex UnitIndex { get; set; }
    }
}
