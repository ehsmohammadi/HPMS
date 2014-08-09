using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class PeriodMgtWorkSpaceViewModel : WorkspaceViewModel
    {
        private IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources;
        public IPeriodMgtAppLocalizedResources PeriodMgtAppLocalizedResources
        {
            get { return periodMgtAppLocalizedResources; }
            set { this.SetField(p => p.PeriodMgtAppLocalizedResources, ref periodMgtAppLocalizedResources, value); }
        }
        
    }
}
