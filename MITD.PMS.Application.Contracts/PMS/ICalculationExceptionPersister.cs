using MITD.Core;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.JobIndexPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Application.Contracts
{
    public interface ICalculationExceptionPersister : IService, IEventHandler<CalculationExceptionReady>,
        IEventHandler<CalculationCompleted>
    {
    }
}
