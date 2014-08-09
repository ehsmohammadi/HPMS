using MITD.Presentation;


namespace MITD.PMS.Presentation.Logic

{
    public interface IEmployeeAppLocalizedResources:ILocalizedResources
    {

        string EmployeeListPageTitle { get; }
        string AddEmployeeCommandTitle { get; }
        string CancelTitle { get; }
        string SaveTitle { get; }
        string EmployeeViewTitle { get;}
    }
}
