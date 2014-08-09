using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Calculations
{
    public class CalculationException : PMSException
    {
        public CalculationException(string message):base(message)
        {
        }

        public CalculationException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public CalculationException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }

        public CalculationException(int code, string message)
            : base(code, message)
        {
        }
    }
}
