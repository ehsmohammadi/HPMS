using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMSReport.Domain.Model;

namespace MITD.PMS.Application.Contracts
{
    public interface IInquiryService : IService
    {
        List<InquirySubjectWithJobPosition> GetInquirySubjects(EmployeeId employeeId);
        List<JobIndex> GetInquiryIndices(EmployeeId employeeId);
        
        List<InquiryJobIndexPoint> GetAllInquiryJobIndexPointBy(JobPositionInquiryConfigurationItemId configurationItemId);
        List<InquiryJobIndexPoint> GetAllInquiryJobIndexPointByIndex(PeriodId periodId, EmployeeId employeeId, AbstractJobIndexId abstractJobIndexId);
        


        void UpdateInquiryJobIndexPoints(IEnumerable<InquiryJobIndexPoinItem> inquiryJobIndexPoinItems);
        void CreateAllInquiryJobIndexPoint(JobPositionInquiryConfigurationItem itm);

        
    }
}
