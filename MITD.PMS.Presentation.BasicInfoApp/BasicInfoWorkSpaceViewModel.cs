using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class BasicInfoWorkSpaceViewModel : WorkspaceViewModel
    {
        private IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources;
        public IBasicInfoAppLocalizedResources BasicInfoAppLocalizedResources
        {
            get { return basicInfoAppLocalizedResources; }
            set { this.SetField(p=>p.BasicInfoAppLocalizedResources , ref basicInfoAppLocalizedResources,value); }
        }

    }
}
