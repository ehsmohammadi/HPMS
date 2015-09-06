using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryUnitIndexPoints;
using MITD.PMS.Domain.Model.Units;
using MITD.PMSReport.Domain.Model;

namespace MITD.PMS.Application.Contracts
{
    public interface IUnitInquiryService : IService
    {
        List<InquirySubjectWithUnit> GetInquirySubjects(EmployeeId employeeId);
        List<InquiryUnitIndexPoint> GetAllInquiryUnitIndexPointBy(EmployeeId employeeId, UnitId id);
        //void UpdateInquiryUnitIndexPoints(InquiryUnitIndexPoinItem inquiryUnitIndexPoinItem);
        void UpdateInquiryUnitIndexPoints(List<InquiryUnitIndexPoinItem> inquiryUnitIndexPoinItems);
        void CreateAllInquiryUnitIndexPoint(UnitInquiryConfigurationItem itm);
    }
}
