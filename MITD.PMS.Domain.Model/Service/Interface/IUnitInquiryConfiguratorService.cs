using System;
using System.Collections.Generic;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Units;

namespace MITD.PMS.Domain.Service
{
    public interface IUnitInquiryConfiguratorService
    {
        List<UnitInquiryConfigurationItem> Configure(Unit unit);
        List<UnitInquiryConfigurationItem> GetUnitInquiryConfigurationItemBy(Employee inquirer);
    }
}
