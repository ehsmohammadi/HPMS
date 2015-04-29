using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{ 
    public static partial class TestData
    {
        private static readonly List<int> customFieldsActionCode = new List<int>
            {
                (int) ActionType.AddCustomField,
                (int) ActionType.ModifyCustomField,
                (int) ActionType.DeleteCustomField
            };

        public static List<CustomFieldDTOWithActions> customFields = new List<CustomFieldDTOWithActions>
            {
                new CustomFieldDTOWithActions
                    {
                        ActionCodes = customFieldsActionCode,
                        Name = "فیلد دلخواه 1",
                        EntityId = 1,
                        EntityName = "Job",
                        DictionaryName = "Field1",
                        MaxValue = 2,
                        MinValue = 10,
                        TypeName = "عدد",
                        TypeId = "Int",
                        Id = 1
                    },
                    new CustomFieldDTOWithActions
                    {
                        ActionCodes = customFieldsActionCode,
                        Name = "فیلد دلخواه 2",
                        EntityId = 1,
                        EntityName = "Job",
                        DictionaryName = "Field2",
                        MaxValue = 2,
                        MinValue = 10,
                        TypeName = "عدد",
                        TypeId = "Int",
                        Id = 2
                    },
                    new CustomFieldDTOWithActions
                    {
                        ActionCodes = customFieldsActionCode,
                        Name = "فیلد دلخواه 3",
                        EntityId = 2,
                        EntityName = "Job",
                        DictionaryName = "Field1",
                        MaxValue = 2,
                        MinValue = 10,
                        TypeName = "عدد",
                        TypeId = "Int",
                        Id = 3
                    },new CustomFieldDTOWithActions
                    {
                        ActionCodes = customFieldsActionCode,
                        Name = "فیلد دلخواه 4",
                        EntityId = 4,
                        EntityName = "Employee",
                        DictionaryName = "Field1",
                        MaxValue = 2,
                        MinValue = 10,
                        TypeName = "عدد",
                        TypeId = "Int",
                        Id = 4
                    },
                    new CustomFieldDTOWithActions
                    {
                        ActionCodes = customFieldsActionCode,
                        Name = "فیلد دلخواه 5",
                        EntityId = 4,
                        EntityName = "Employee",
                        DictionaryName = "Field1",
                        MaxValue = 2,
                        MinValue = 10,
                        TypeName = "عدد",
                        TypeId = "Int",
                        Id = 5
                    },
                    new CustomFieldDTOWithActions
                    {
                        ActionCodes = customFieldsActionCode,
                        Name = "درجه اهمیت",
                        EntityId = 2,
                        EntityName = "JobIndex",
                        DictionaryName = "Importance",
                        MaxValue = 9,
                        MinValue = 1,
                        TypeName = "عدد",
                        TypeId = "Int",
                        Id = 6
                    },
                    new CustomFieldDTOWithActions
                    {
                        ActionCodes = customFieldsActionCode,
                        Name = "نمره تعدیل شده",
                        EntityId = 2,
                        EntityName = "JobIndex",
                        DictionaryName = "WeightedPoint",
                        MaxValue = 2,
                        MinValue = 10,
                        TypeName = "عدد",
                        TypeId = "Int",
                        Id = 7
                    },

            };

    }
}
