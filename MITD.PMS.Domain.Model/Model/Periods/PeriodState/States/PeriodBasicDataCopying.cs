using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Periods
{
    public class PeriodBasicDataCopying : PeriodState 
    {
        public PeriodBasicDataCopying()
            : base("2", "PeriodBasicDataCopying")
        {

        }


        internal override void CompleteCopyingBasicData(Period period, IPeriodManagerService periodManagerService, PeriodState preState)
        {
           // if(preState is 
            period.State = new PeriodInitState();
        }

        internal override BasicDataCopyingProgress GetCopyingStateProgress(Period period, IPeriodManagerService periodManagerService)
        {
            return periodManagerService.GetCopyingStateProgress(period);
        }

        internal override void RollBack(Period period, IPeriodManagerService periodManagerService)
        {
            periodManagerService.DeleteBasicData(period);
        }

        internal override  void CheckAssigningUnit()
        {
        }

        internal override  void CheckRemovingUnit()
        {
        }

        internal override  void CheckAssigningJobIndex()
        {
        }

        internal override  void CheckAssigningJob()
        {
        }

        internal override  void CheckAssigningJobPosition()
        {
        }
       
        
    }
}
