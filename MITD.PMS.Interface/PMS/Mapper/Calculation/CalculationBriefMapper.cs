using MITD.Core;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.Policies;
using MITD.PMSReport.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class CalculationBriefMapper : BaseMapper<CalculationWithPolicyAndPeriod, CalculationBriefDTOWithAction>
        , IMapper<CalculationWithPolicyAndPeriod, CalculationBriefDTOWithAction>
    {
        public override CalculationBriefDTOWithAction MapToModel(CalculationWithPolicyAndPeriod entity)
        {
            var res = new CalculationBriefDTOWithAction 
            { 
                Id = entity.Calculation.Id.Id, 
                Name = entity.Calculation.Name, 
                PolicyName = entity.Policy.Name,
                StateName = entity.Calculation.State.DisplayName,
                EmployeeCount = entity.Calculation.EmployeeIdList.Count,
                DeterministicStatus = entity.Calculation.IsDeterministic ? "قطعی":"موقت"
            };
            res.ActionCodes = new List<int>
            {
                (int) ActionType.AddCalculation,
                (int) ActionType.DeleteCalculation,
                (int) ActionType.ShowCalculationState,
                (int) ActionType.SetDeterministicCalculation,
                (int) ActionType.UnsetDeterministicCalculation,
                (int) ActionType.ShowCalculationResult,
                (int) ActionType.RunCalculation,
                (int) ActionType.ShowAllCalculationException,
                
            };

            return res;
        }
        public override CalculationWithPolicyAndPeriod MapToEntity(CalculationBriefDTOWithAction model)
        {
            throw new NotImplementedException();
        }
    }
}
