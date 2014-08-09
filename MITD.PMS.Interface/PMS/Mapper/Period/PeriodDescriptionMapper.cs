using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    public class PeriodDescriptionMapper : BaseMapper<Period, PeriodDescriptionDTO>, IMapper<Period, PeriodDescriptionDTO>
    {

        public override PeriodDescriptionDTO MapToModel(Period entity)
        {
            var res = new PeriodDescriptionDTO
            {
                Id = entity.Id.Id,
                Name = entity.Name,
                //StateCode = 
            };
            return res;

        }

        public override Period MapToEntity(PeriodDescriptionDTO model)
        {
            throw new InvalidOperationException();
        }

    }

}
