using System;
using System.Collections.Generic;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation.Config;
using MITD.Presentation;

namespace MITD.PMS.Presentation.EmployeeManagement
{
    public class BootStrapper : IBootStrapper
    {
        public void Execute()
        {
            var resourceDic = Application.GetResourceStream(new Uri("MITD.PMS.Presentation.EmployeeManagement;component/Assets/LocalResource.xaml", UriKind.Relative));
            ApplicationConfigHelper.ConfigureModule<IEmployeeController, EmployeeController>(resourceDic);
            ModuleConfigHelper.ConfigureActionModule<IActionService>(new Dictionary<int, Type>
                {
                     {(int) ActionType.AddEmployee,typeof(AddEmployeeService)},
                    {(int) ActionType.ModifyEmployee,typeof(ModifyEmployeeService)},
                    {(int) ActionType.DeleteEmployee,typeof(DeleteEmployeeService)},
                    {(int) ActionType.ManageEmployeeJobPositions,typeof(ManageJobPostionSevice)},
                    {(int) ActionType.ModifyEmployeeJobCustomFields,typeof(ModifyEmployeeJobCustomFieldsSevice)},
                    
                });
            //var serviceLocator=ServiceLocator.Current.GetInstance<ICastleWindsor>()


        }
    }
}
