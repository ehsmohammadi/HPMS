using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    public class PeriodMapper : BaseMapper<Period, PeriodDTO>, IMapper<Period, PeriodDTO>
    {
         
        public override PeriodDTO MapToModel(Period entity)
        {
            var res = new PeriodDTO
                {
                    Id = entity.Id.Id,
                    Name = entity.Name,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    //StateCode = entity.StateCode,
                };
            return res;

        }

        public override Period MapToEntity(PeriodDTO model)
        {
            throw new InvalidOperationException();

        }

    }

}
