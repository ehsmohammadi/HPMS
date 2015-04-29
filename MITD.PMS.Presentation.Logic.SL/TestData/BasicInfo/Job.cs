using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public static partial class TestData
    {
        private static readonly List<int> jobActionCode = new List<int> { 121, 122, 123 };

        public static List<JobDTOWithActions> jobList = new List<JobDTOWithActions>
            {
                new JobDTOWithActions {ActionCodes = jobActionCode, Name = "مهندسی کشتی", DictionaryName = "BoatEngineer",Id = 1},
                new JobDTOWithActions {ActionCodes = jobActionCode, Name = "مهندسی دریا", DictionaryName = "SeaEngineer",Id = 2}
            };

        public static List<AbstractCustomFieldDescriptionDTO> jobCustomFieldDescription =
            new List<AbstractCustomFieldDescriptionDTO>();

        public static List<CustomFieldDTO> jobCustomFields = new List<CustomFieldDTO>();

    }
}
