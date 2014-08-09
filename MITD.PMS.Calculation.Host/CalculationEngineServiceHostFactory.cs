using MITD.PMS.Calculation.Host;
using MITD.Services;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace MITD.PMS.Calculation.Host
{

    public class CalculationEngineServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            BootStrapper.Execute();
            var host = new ServiceHost(serviceType,baseAddresses);
            host.Description.Behaviors.Add(new IocServiceBehavior());
            host.Description.Behaviors.Add(new GlobalExceptionHandlerBehavior());
            return host;
        }

    }
}
