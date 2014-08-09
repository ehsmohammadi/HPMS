using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Service
{
    public static class PeriodActivatorService
    {
        private static Object lockObject = new object(); 

        public static void Activate(IPeriodRepository periodRep,Period period)
        {
            lock (lockObject)
            {
               bool isActivePeriod = periodRep.GetAll().Any(p => p.Id != period.Id && p.Active);
                if (!isActivePeriod)
                    period.Activate();
                else
                    throw new PeriodException((int)ApiExceptionCode.CouldNotActivatePeriodWhileExistsAnotherActivePeriod
                        , ApiExceptionCode.CouldNotActivatePeriodWhileExistsAnotherActivePeriod.DisplayName); 
            }
        }
    }
}
