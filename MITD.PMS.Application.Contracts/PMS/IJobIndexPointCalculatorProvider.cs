using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Application.Contracts
{
    public interface IJobIndexPointCalculatorProvider
    {
        IJobIndexPointCalculator GetCalculator(CalculationId calculationId);
    }
}
