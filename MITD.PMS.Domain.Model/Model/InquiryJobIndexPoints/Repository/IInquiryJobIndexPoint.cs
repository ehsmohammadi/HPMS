using System;
using System.Collections.Generic;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.InquiryJobIndexPoints
{
    public interface IInquiryJobIndexPointRepository:IRepository
    {
        List<InquiryJobIndexPoint> GetAllBy(JobPositionInquiryConfigurationItemId jobPositionInquiryConfigurationItemId);
        void Add(InquiryJobIndexPoint inquiryJobIndexPoint);
        long GetNextId();
        InquiryJobIndexPoint GetBy(JobPositionInquiryConfigurationItemId configurationItemId, AbstractJobIndexId jobIndexId);
        bool IsAllInquiryJobIndexPointsHasValue(Period period);

        List<AbstractJobIndexId> GetAllJobIndexIdByInquirer(EmployeeId inquirerEmployeeId);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
        
    }
}
