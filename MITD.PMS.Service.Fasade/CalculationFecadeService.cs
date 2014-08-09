using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Service.Fasade
{
    public class CalculationFecadeService : ICalculationFasadeService
    {
        private readonly IMapper<CalculationDTO, Calculation> calculationMapper;

        public CalculationFecadeService(IMapper<CalculationDTO, Calculation> calculationMapper)
        {
            this.calculationMapper = calculationMapper;
        }

        //public PeriodDTOsFecadeService(IMa)
        //{
            
        //}

        public CalculationDTO AddCalculation(CalculationDTO calculation)
        {
            var calc= calculationMapper.MapToModel(calculation);
            
        }

        public CalculationStatusDTO GetCalculation(long calculationId)
        {
            throw new NotImplementedException();
        }

        public void StopCalculate(long calculationId)
        {
            throw new NotImplementedException();
        }
    }
}
