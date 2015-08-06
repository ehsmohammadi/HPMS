using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.Contract.Period
{
    public interface IPeriodDataProvider
    {        
        List<PeriodProperties> GetPeriodList();

        PeriodProperties GetPeriodInformation(long id);

    }
}
