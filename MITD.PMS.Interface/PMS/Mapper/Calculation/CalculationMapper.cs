using MITD.Core;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSReport.Domain.Model;

namespace MITD.PMS.Interface
{
    public class CalculationMapper : BaseMapper<CalculationWithPolicyAndPeriod, CalculationDTO>, 
        IMapper<CalculationWithPolicyAndPeriod, CalculationDTO>
    {
        public override CalculationWithPolicyAndPeriod MapToEntity(CalculationDTO model)
        {
            throw new System.NotImplementedException();
        }
        public override CalculationDTO MapToModel(CalculationWithPolicyAndPeriod entity)
        {
            return new CalculationDTO 
            { 
                Id = entity.Calculation.Id.Id, 
                Name = entity.Calculation.Name, 
                PeriodId = entity.Calculation.PeriodId.Id, 
                PeriodName = entity.Period.Name,
                PolicyId = entity.Calculation.PolicyId.Id,
                PolicyName = entity.Policy.Name,
                EmployeeCount = entity.Calculation.EmployeeCount,
                EmployeeCalculatedCount = (entity.Calculation.CalculationResult != null) ? entity.Calculation.CalculationResult.EmployeesCalculatedCount : 0
            };
        } 
    }
}
