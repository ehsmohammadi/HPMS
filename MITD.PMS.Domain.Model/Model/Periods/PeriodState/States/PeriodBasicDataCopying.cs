using MITD.PMS.Domain.Service;

namespace MITD.PMS.Domain.Model.Periods
{
    public class PeriodBasicDataCopying : PeriodState 
    {
        #region Constructors
        public PeriodBasicDataCopying()
            : base("2", "PeriodBasicDataCopying")
        {

        } 
        #endregion
        
        #region State methods
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
            period.State = new PeriodInitState();
        }
        
        #endregion

        #region Checker methods
        internal override void CheckAssigningUnit()
        {
        }

        internal override void CheckRemovingUnit()
        {
        }

        internal override void CheckAssigningJobIndex()
        {
        }
        internal override void CheckAssigningUnitIndex()
        {
        }
        internal override void CheckAssigningJob()
        {
        }

        internal override void CheckAssigningJobPosition()
        {
        }
        
        #endregion       
        
    }
}
