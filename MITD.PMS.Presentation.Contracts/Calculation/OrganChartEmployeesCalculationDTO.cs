using MITD.Presentation;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class OrganChartEmployeesCalculationDTO
    {

        private List<PolicyDescriptionDTO> policyDescriptions;
        public List<PolicyDescriptionDTO> PolicyDescriptions
        {
            get { return policyDescriptions; }
            set { this.SetField(p => p.PolicyDescriptions, ref policyDescriptions, value); }
        }

        private List<AbstractOrganChartDTOWithActions> abstractOrganCharts;
        public List<AbstractOrganChartDTOWithActions> AbstractOrganCharts
        {
            get { return abstractOrganCharts; }
            set { this.SetField(p => p.AbstractOrganCharts, ref abstractOrganCharts, value); }
        }

        private long periodId;
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }
    }
}
