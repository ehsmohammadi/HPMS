using MITD.PMS.Presentation.EmployeeManagement.Assets.Resources;
using MITD.PMS.Presentation.Logic;


namespace MITD.PMS.Presentation.EmployeeManagement
{
    public class EmployeeAppLocalizedResources : IEmployeeAppLocalizedResources
    {
        public EmployeeAppLocalizedResources()
        {
           
        }
        public string EmployeeListPageTitle
        {
            get { return EmployeeAppStrings.EmployeeListPageTitle; }
        }

        public string AddEmployeeCommandTitle
        {
            get { return EmployeeAppStrings.AddEmployeeCommandTitle; }
        }

        public string CancelTitle
        {
            get { return EmployeeAppStrings.CancelTitle; }
        }
        public string SaveTitle
        {
            get { return EmployeeAppStrings.SaveTitle; }
        }
        public string EmployeeViewTitle
        {
            get { return EmployeeAppStrings.EmployeeViewTitle; }
        }
    }
}
