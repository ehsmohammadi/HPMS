using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class PeriodWithActionMapper : BaseMapper<Period, PeriodDTOWithAction>, IMapper<Period, PeriodDTOWithAction>
    {
 
        public override PeriodDTOWithAction MapToModel(Period entity)
        {
            var res = new PeriodDTOWithAction
            {
                Id = entity.Id.Id,
                Name = entity.Name,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                StateName = Enumeration.FromValue < PeriodStateEnum >(entity.State.Value).Description,//entity.State.DisplayName,
                ActiveStatus = entity.Active ? "فعال": "غیرفعال",
                ActionCodes = new List<int>
                {
                    (int) ActionType.AddPeriod,
                    (int) ActionType.ActivatePeriod,
                    (int) ActionType.InitializePeriodForInquiry,
                    (int) ActionType.GetPeriodInitializingInquiryStatus,
                    (int) ActionType.StartInquiry,
                    (int) ActionType.CompleteInquiry,
                    (int) ActionType.ClosePeriod,
                    (int) ActionType.ManageCalculations,
                    (int) ActionType.RollBackPeriodState,

                    (int) ActionType.ModifyPeriod,
                    (int) ActionType.DeletePeriod,
                    (int) ActionType.ManageEmployees,
                    (int) ActionType.ManageJobIndexInPeriod,
                    (int) ActionType.ManageJobPositionInPeriod,
                    (int) ActionType.ManageJobInPeriod,
                    (int) ActionType.ManageUnitInPeriod,
                    (int) ActionType.ManageUnitIndexInPeriod,

                    
                    //(int) ActionType.CopyPeriodBasicData, remove copy from last active period
                    //(int) ActionType.StartCliaming, for Remove claim
                    //(int) ActionType.FinishCliaming, for Remove claim
                }
            };
            return res;

        }

        public override Period MapToEntity(PeriodDTOWithAction model)
        {
            throw new InvalidOperationException();
        }

    }

}
