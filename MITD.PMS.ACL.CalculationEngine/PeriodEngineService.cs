using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Domain;
using MITD.PMS.Domain.Model.Periods;
using System.Transactions;
using MITD.Services;

namespace MITD.PMS.ACL.CalculationEngine
{
    public partial class PeriodEngineService : IPeriodEngineService
    {

        private IFaultExceptionAdapter errorAdapter;
        public PeriodEngineService(IFaultExceptionAdapter errorAdapter)
        {
            this.errorAdapter = errorAdapter;    
        }
        public void InitializeInquiry(Domain.Model.Periods.PeriodId periodId)
        {
            var client = new CalculationEngineRef.PeriodEngineServiceClient();
            WcfClientHelper.CallMethod((c, id) => c.InitializeInquiry(id.Id), client, periodId, errorAdapter);
        }

        public PeriodEngineState GetIntializeInquiryState(Domain.Model.Periods.PeriodId periodId)
        {
            var client = new CalculationEngineRef.PeriodEngineServiceClient();
            return WcfClientHelper.CallMethod((c, id) => c.GetIntializeInquiryState(id.Id), client, periodId, errorAdapter);
        }

        public void CopyBasicData(Domain.Model.Periods.PeriodId sourcePeriodId, Domain.Model.Periods.PeriodId destionationPeriodId)
        {
            var client = new CalculationEngineRef.PeriodEngineServiceClient();
            WcfClientHelper.CallMethod((c, id1, id2) => c.CopyBasicData(id1.Id, id2.Id), client, sourcePeriodId, destionationPeriodId, errorAdapter);
        }

        public PeriodEngineState GetPeriodCopyingStateProgress(Domain.Model.Periods.PeriodId periodId)
        {
            var client = new CalculationEngineRef.PeriodEngineServiceClient();
            return WcfClientHelper.CallMethod((c, id) => c.GetPeriodCopyingStateProgress(id.Id), client, periodId, errorAdapter);
        }
    }

}
