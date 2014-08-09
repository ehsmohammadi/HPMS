using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Domain;
using System.Transactions;
using MITD.Domain.Model;
using MITD.Services;

namespace MITD.PMS.ACL.CalculationEngine
{
    public partial class CalculationEngineService : ICalculationEngineService
    {
        private IFaultExceptionAdapter errorAdapter;
        public CalculationEngineService(IFaultExceptionAdapter errorAdapter)
        {
            this.errorAdapter = errorAdapter;    
        }

        public void RunCalculation(Domain.Model.Calculations.CalculationId calculationId)
        {
            var client = new CalculationEngineRef.CalculationEngineServiceClient();
            WcfClientHelper.CallMethod((c, id) => c.Run(id.Id), client, calculationId, errorAdapter);
        }

        public CalculationEngineState GetCalculationState(Domain.Model.Calculations.CalculationId calculationId)
        {
            var client = new CalculationEngineRef.CalculationEngineServiceClient();
            var progress = WcfClientHelper.CallMethod((c, id) => c.CheckStatus(id.Id), client, calculationId, errorAdapter);
            return new CalculationEngineState
             {
                 MessageList = progress.MessageList.ToList(),
                 Percent = progress.Percent,
                 StateName = progress.StateName
             };
        }

        public void StopCalculation(Domain.Model.Calculations.CalculationId calculationId)
        {
            var client = new CalculationEngineRef.CalculationEngineServiceClient();
            WcfClientHelper.CallMethod((c, id) => c.Stop(id.Id), client, calculationId, errorAdapter);
        }

        public void PauseCalculation(Domain.Model.Calculations.CalculationId calculationId)
        {
            var client = new CalculationEngineRef.CalculationEngineServiceClient();
            WcfClientHelper.CallMethod((c, id) => c.Pause(id.Id), client, calculationId, errorAdapter);
        }

    }
}
