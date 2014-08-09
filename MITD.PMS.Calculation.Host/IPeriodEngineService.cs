using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Calculation.Host
{
    [ServiceContract]
    public interface IPeriodEngineService:IService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorDetail))]
        void InitializeInquiry(long periodId);

        [OperationContract]
        [FaultContract(typeof(ErrorDetail))]
        PeriodEngineState GetIntializeInquiryState(long periodId);

        [OperationContract]
        [FaultContract(typeof(ErrorDetail))]
        void CopyBasicData(long sourcePeriodId, long destionationPeriodId);

        [OperationContract]
        [FaultContract(typeof(ErrorDetail))]
        PeriodEngineState GetPeriodCopyingStateProgress(long periodId);
    }
}
