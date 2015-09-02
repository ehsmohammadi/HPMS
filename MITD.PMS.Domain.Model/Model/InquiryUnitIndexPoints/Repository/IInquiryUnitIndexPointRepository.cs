using System;
using System.Collections.Generic;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;

namespace MITD.PMS.Domain.Model.InquiryUnitIndexPoints
{
    public interface IInquiryUnitIndexPointRepository:IRepository
    {
       // List<InquiryUnitIndexPoint> GetAllBy(UnitInquiryConfigurationItemId unitInquiryConfigurationItemId);
        List<InquiryUnitIndexPoint> GetAllBy(EmployeeId employeeId, UnitId unitId);
        void Add(InquiryUnitIndexPoint inquiryUnitIndexPoint);
        long GetNextId();


        InquiryUnitIndexPoint GetBy(UnitInquiryConfigurationItemId configurationItemId);
        // InquiryUnitIndexPoint GetBy(UnitInquiryConfigurationItemId configurationItemId, AbstractUnitIndexId unitIndexId);
        bool IsAllInquiryUnitIndexPointsHasValue(Period period);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);

    }
}
