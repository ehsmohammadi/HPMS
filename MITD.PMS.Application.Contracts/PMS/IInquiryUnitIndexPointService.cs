﻿using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Units;
using MITD.PMSReport.Domain.Model;

namespace MITD.PMS.Application.Contracts
{
    public interface IInquiryUnitIndexPointService : IService
    {
        void Add(UnitInquiryConfigurationItem itm, UnitIndex unitIndex, string empty);
        void Update(UnitInquiryConfigurationItemId configurationItemId, AbstractUnitIndexId unitIndexId, string unitIndexValue);
    }
}
