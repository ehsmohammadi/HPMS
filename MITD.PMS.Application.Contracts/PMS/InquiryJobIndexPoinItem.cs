using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using System;

namespace MITD.PMS.Application.Contracts
{
    public class InquiryJobIndexPoinItem
    {
        public InquiryJobIndexPoinItem(JobPositionInquiryConfigurationItemId configurationItemId, AbstractJobIndexId jobIndexId, string jobIndexValue)
        {
            ConfigurationItemId = configurationItemId;
            JobIndexId = jobIndexId;
            JobIndexValue = jobIndexValue;
        }

        public JobPositionInquiryConfigurationItemId ConfigurationItemId
        {
            get;
            set;
        }
        
        public AbstractJobIndexId JobIndexId
        {
            get;
            set;
        }


        public string JobIndexValue
        {
            get;
            set;
        }

    }
}
