using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
 
namespace MITD.PMS.Presentation.Logic
{
    public static partial class TestData
    {
        private static readonly List<int> PolicyActionCode = new List<int> { 501, 502, 503, 504, 505 };

        public static List<PolicyDTOWithActions> policyList = new List<PolicyDTOWithActions>
            {
                new PolicyDTOWithActions
                    {
                        Id = 1,
                        DictionaryName = "Porter",
                        ActionCodes = PolicyActionCode,
                        Name = "خط کش پرتر"
                    },
                    new PolicyDTOWithActions
                    {
                        Id = 2,
                        DictionaryName = "Policy2",
                        ActionCodes = PolicyActionCode,
                        Name = "نظام محاسبه عملکرد2"
                    },
                    new PolicyDTOWithActions
                    {
                        Id = 3,
                        DictionaryName = "Policy3",
                        ActionCodes = PolicyActionCode,
                        Name = "نظام محاسبه عملکرد3"
                    },
                    new PolicyDTOWithActions
                    {
                        Id = 4,
                        DictionaryName = "Policy4",
                        ActionCodes = PolicyActionCode,
                        Name = "نظام محاسبه عملکرد4"
                    }

            };

        
    }
}
