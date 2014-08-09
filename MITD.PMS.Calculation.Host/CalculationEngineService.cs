using MITD.Core;
using MITD.Data.NH;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MITD.PMS.Calculation.Host
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalculationEngineService" in both code and config file together.
    [ServiceBehavior(
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.PerCall)]
    public partial class CalculationEngineService : ICalculationEngineService
    {
        private ICalculationService service;
        private IPeriodService periodService;
        public CalculationEngineService(ICalculationService service, IPeriodService periodService, IPeriodRepository p)
        {
            this.service = service;
            this.periodService = periodService;
        }
        public void Run(long calculationId)
        {
            service.RunCalculation(new CalculationId(calculationId));
        }

        public void Stop(long calculationId)
        {
            service.RunCalculation(new CalculationId(calculationId));
        }

        public void Pause(long calculationId)
        {
            service.RunCalculation(new CalculationId(calculationId));
        }

        public CalculationState CheckStatus(long calculationId)
        {
            var progress = service.GetCalculationState(new CalculationId(calculationId));
            return new CalculationState
            {
                Percent = progress.Progress.Percent,
                MessageList = progress.Messages.ToList(),
                StateName = progress.Calculation.State.DisplayName

            };
        }
    }
}
