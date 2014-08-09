using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
 
namespace MITD.PMS.Presentation.Logic
{
    public static partial class TestData
    {
        private static readonly List<int> jobPositionActionCode = new List<int> { 300, 301, 302 };

        public static List<JobPositionDTOWithActions> jobPositionListWithAction = new List<JobPositionDTOWithActions>
            {
                new JobPositionDTOWithActions
                    {
                        Id = 1,
                        ActionCodes = jobPositionActionCode,
                        Name = "پست 1",
                        DictionaryName = "Post1"
                    },
                    new JobPositionDTOWithActions
                    {
                        Id = 2,
                        ActionCodes = jobPositionActionCode,
                        Name = "پست 2",
                        DictionaryName = "Post2"
                    },
                    new JobPositionDTOWithActions
                    {
                        Id = 3,
                        ActionCodes = jobPositionActionCode,
                        Name = "پست 3",
                        DictionaryName = "Post3"
                    },
                    new JobPositionDTOWithActions
                    {
                        Id = 4,
                        ActionCodes = jobPositionActionCode,
                        Name = "پست 4",
                        DictionaryName = "Post4"
                    },
            };

        public static List<JobPositionDTO> jobPositionList = new List<JobPositionDTO>
            {
                new JobPositionDTO
                    {
                        Id = jobPositionListWithAction.Single(j=>j.Id==1).Id,
                        Name = jobPositionListWithAction.Single(j=>j.Id==1).Name,
                        DictionaryName = jobPositionListWithAction.Single(j=>j.Id==1).DictionaryName,
                    },
                    new JobPositionDTO
                    {
                        Id = jobPositionListWithAction.Single(j=>j.Id==2).Id,
                        Name = jobPositionListWithAction.Single(j=>j.Id==2).Name,
                        DictionaryName = jobPositionListWithAction.Single(j=>j.Id==2).DictionaryName,
                    },
                    new JobPositionDTO
                    {
                        Id = jobPositionListWithAction.Single(j=>j.Id==3).Id,
                        Name = jobPositionListWithAction.Single(j=>j.Id==3).Name,
                        DictionaryName = jobPositionListWithAction.Single(j=>j.Id==3).DictionaryName,
                    },
                    new JobPositionDTO
                    {
                        Id = jobPositionListWithAction.Single(j=>j.Id==4).Id,
                        Name = jobPositionListWithAction.Single(j=>j.Id==4).Name,
                        DictionaryName = jobPositionListWithAction.Single(j=>j.Id==4).DictionaryName,
                    },
            };
    }
}
