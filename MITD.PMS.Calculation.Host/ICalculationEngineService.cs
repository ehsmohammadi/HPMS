using MITD.Core;
using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MITD.PMS.Calculation.Host
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICalculationEngineService" in both code and config file together.
    [ServiceContract]
    public interface ICalculationEngineService:IService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorDetail))]
        void Run(long calculationId);
        [OperationContract]
        [FaultContract(typeof(ErrorDetail))]
        void Stop(long calculationId);
        [OperationContract]
        [FaultContract(typeof(ErrorDetail))]
        void Pause(long calculationId);
        [OperationContract]
        [FaultContract(typeof(ErrorDetail))]
        CalculationState CheckStatus(long calculationId);
    }


}
