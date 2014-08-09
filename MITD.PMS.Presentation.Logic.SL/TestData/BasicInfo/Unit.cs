using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public static partial class TestData
    {
        private static readonly List<int> unitActionCode = new List<int> { 401, 402, 403 };

        public static List<UnitDTOWithActions> unitListWithAction = new List<UnitDTOWithActions>
            {
                new UnitDTOWithActions
                    {
                        Id = 1,
                        ActionCodes = unitActionCode,
                        Name = "واحد 1",
                        DictionaryName = "Unit1"
                    },
                    new UnitDTOWithActions
                    {
                        Id = 2,
                        ActionCodes = unitActionCode,
                        Name = "واحد 2",
                        DictionaryName = "Unit2"
                    },
                    new UnitDTOWithActions
                    {
                        Id = 3,
                        ActionCodes = unitActionCode,
                        Name = "واحد 3",
                        DictionaryName = "Unit3"
                    },
                    new UnitDTOWithActions
                    {
                        Id = 4,
                        ActionCodes = unitActionCode,
                        Name = "واحد 4",
                        DictionaryName = "Unit4"
                    },
            };
    }
}
