using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class EmployeeMgtWorkSpaceViewModel : WorkspaceViewModel
    {
        private IEmployeeAppLocalizedResources employeeAppLocalizedResources;
        public IEmployeeAppLocalizedResources EmployeeMgtAppLocalizedResources
        {
            get { return employeeAppLocalizedResources; }
            set { this.SetField(p => p.EmployeeMgtAppLocalizedResources, ref employeeAppLocalizedResources, value); }
        }
        
    }
}
