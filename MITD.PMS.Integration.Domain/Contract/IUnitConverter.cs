using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.Domain.Contract
{
    public interface IUnitConverter
    {
        void ConvertUnits(long PeriodId);
    }
}
