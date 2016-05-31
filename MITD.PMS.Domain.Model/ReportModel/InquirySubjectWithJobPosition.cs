using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobPositions;

namespace MITD.PMSReport.Domain.Model
{
    public class InquirySubjectWithJobPosition
    {
        public JobPosition InquirerJobPosition { get; set; }
        public Employee InquirySubject { get; set; }
        public JobPosition InquirySubjectJobPosition { get; set; }
        public bool IsCompleted { get; set; }
    }
}
