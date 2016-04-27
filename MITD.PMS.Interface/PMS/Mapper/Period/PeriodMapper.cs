using System;
using MITD.Core;
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
                    ActiveStatus = entity.Active,
                    StateName = entity.State.Value
                };
            return res;

        }

        public override Period MapToEntity(PeriodDTO model)
        {
            throw new InvalidOperationException();

        }

    }

}
