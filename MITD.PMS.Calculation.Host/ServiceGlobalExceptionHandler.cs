using System;
using System.Data;
using System.IO;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using MITD.Core;
using MITD.PMSSecurity.Domain.Service;
using System.ServiceModel;
using MITD.Domain.Model;
using MITD.Services;

namespace MITD.PMS.Calculation.Host
{
    public class ServiceGlobalExceptionHandler : IErrorHandler
    {

        public bool HandleError(Exception error)
        {
            return true;

        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var con = ServiceLocator.Current.GetInstance<IFaultExceptionAdapter>();          
            var faultException = con.ConvertToFault(error);
            fault = Message.CreateMessage(version, faultException.CreateMessageFault(), faultException.Action);
        }
    }
}
