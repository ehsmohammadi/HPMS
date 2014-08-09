using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public static partial class TestData
    {
        private static readonly List<int> JobPositionInPeriodActionCode = new List<int>
            {
                (int) ActionType.AddJobPositionInPeriod,
                (int) ActionType.DeleteJobPositionInPeriod,
                (int) ActionType.ManageJobPositionInPeriodInquiry
            };

        public static List<JobPositionInPeriodDTOWithActions> jobPositionInPeriodWithActionList = new List<JobPositionInPeriodDTOWithActions>
            {
                //new JobPositionInPeriodDTOWithActions
                //    {
                //        ActionCodes = JobPositionInPeriodActionCode,
                //        Name = jobPositionListWithAction.Single(j=>j.Id==1).Name,
                //        JobPositionId = 1,
                //        ParentId = null
                //    },
                //new JobPositionInPeriodDTOWithActions
                //    {
                //        ActionCodes = JobPositionInPeriodActionCode,
                //        Name =jobPositionListWithAction.Single(j=>j.Id==2).Name,
                //        Id = 2,
                //        ParentId = 1
                //    },
                //new JobPositionInPeriodDTOWithActions
                //    {
                //        ActionCodes = JobPositionInPeriodActionCode,
                //        Name = jobPositionListWithAction.Single(j=>j.Id==3).Name,
                //        Id = 3,
                //        ParentId = 1
                //    },
                //new JobPositionInPeriodDTOWithActions
                //    {
                //        ActionCodes = JobPositionInPeriodActionCode,
                //        Name =jobPositionListWithAction.Single(j=>j.Id==4).Name,
                //        Id = 4,
                //        ParentId = 2
                //    },

            };

    }
}
