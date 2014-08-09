using Castle.MicroKernel.Registration;
using MITD.Core;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;
using MITD.Presentation.Config;
using System;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.UI
{
    public class BootStrapper : IBootStrapper
    {
        public void Execute()
        {

            var container = ApplicationConfigHelper.Configure<MainWindowVM, MainPage, PMSController>(new Dictionary<string, List<Type>>
            {
                {
                    "MITD.PMS.Presentation.PeriodManagementApp.xap", new List<Type>
                    {
                        typeof (IPeriodListView),
                        typeof (IPeriodView),
                        typeof (IPeriodController)
                    }
                },
                {
                    "MITD.PMS.Presentation.BasicInfoApp.xap", new List<Type>
                    {
                        typeof (IJobListView),
                        typeof (IJobView),
                        typeof (IBasicInfoController)
                    }
                },
                {
                    "MITD.PMS.Presentation.EmployeeManagement.xap", new List<Type>
                    {
                        typeof (IEmployeeController)
                    }
                }
            });
            container.Register(Component.For<IUserProvider>().ImplementedBy<UserProvider>().LifestyleSingleton());
            ApplicationConfigHelper.Start();

            registerExceptionConvertors();

        }

        private void registerExceptionConvertors()
        {
            ExceptionConvertorService.RegisterExceptionConvertor(new ArgumentExceptionConvertor());
            ExceptionConvertorService.RegisterExceptionConvertor(new DuplicateExceptionConvertor());
            ExceptionConvertorService.RegisterExceptionConvertor(new ModifyExceptionConvertor());
            ExceptionConvertorService.RegisterExceptionConvertor(new DeleteExceptionConvertor());
            ExceptionConvertorService.RegisterExceptionConvertor(new InvalidStateOperationExceptionConvertor());
            ExceptionConvertorService.RegisterExceptionConvertor(new CompareExceptionConvertor());
        }
    }
}
