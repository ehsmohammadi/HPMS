using MITD.Core;
using MITD.PMS.Domain.Model.Calculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Service
{
    public interface ICalculationEngineService:IService
    {
        void RunCalculation(CalculationId calculationId);

        CalculationEngineState GetCalculationState(CalculationId calculationId);

        void StopCalculation(CalculationId calculationId);

        void PauseCalculation(CalculationId calculationId);
    }
}
