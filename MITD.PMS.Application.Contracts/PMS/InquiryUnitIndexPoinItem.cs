using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Units;
using System;

namespace MITD.PMS.Application.Contracts
{
    public class InquiryUnitIndexPoinItem
    {
        public InquiryUnitIndexPoinItem(UnitInquiryConfigurationItemId configurationItemId, AbstractUnitIndexId unitIndexId, string unitIndexValue)
        {
            ConfigurationItemId = configurationItemId;
            UnitIndexId = unitIndexId;
            UnitIndexValue = unitIndexValue;
        }

        public UnitInquiryConfigurationItemId ConfigurationItemId
        {
            get;
            set;
        }
        
        public AbstractUnitIndexId UnitIndexId
        {
            get;
            set;
        }


        public string UnitIndexValue
        {
            get;
            set;
        }

    }
}
