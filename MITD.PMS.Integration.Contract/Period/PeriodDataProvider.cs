using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.Contract.Period
{
    public class PeriodDataProvider : IPeriodDataProvider
    {
        public List<PeriodProperties> GetPeriodList()
        {
            List<PeriodProperties> Result = new List<PeriodProperties>();
            for (int i = 0; i < 10; i++)
            {
                PeriodProperties TempItem = new PeriodProperties();
                
                TempItem.PeriodID = i*i;
                TempItem.PeriodName = "PeriodName " + i.ToString();
                TempItem.PeriodStateID = i;
                TempItem.PeriodStateName = "PeriodStateName " + i.ToString();

                Result.Add(TempItem);
            }
            return Result;
        }


        public PeriodProperties GetPeriodInformation(long id)
        {
            List<PeriodProperties> Periods = new List<PeriodProperties>();
            for (int i = 0; i < 10; i++)
            {
                PeriodProperties TempItem = new PeriodProperties();
                
                TempItem.PeriodID = i*i;
                TempItem.PeriodName = "PeriodName " + i.ToString();
                TempItem.PeriodStateID = i;
                TempItem.PeriodStateName = "PeriodStateName " + i.ToString();

                Periods.Add(TempItem);
            }




            PeriodProperties Result = new PeriodProperties();

            Result = (from c in Periods where c.PeriodID == id select c).First();

            return Result;


        }

    }
}
