using System;
using System.Collections.Generic;
using System.Net;

using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public static partial class TestData
    {
        private static List<int> periodActionCode = new List<int>
            {
                (int) ActionType.AddPeriod,
                (int) ActionType.ModifyPeriod,
                (int) ActionType.DeletePeriod,
                (int) ActionType.ManageUnitInPeriod,
                (int) ActionType.ManageJobPositionInPeriod,
                (int) ActionType.ManageJobIndexInPeriod,
                (int) ActionType.BeginCurrentPeriodInquiry,
                (int) ActionType.CloseCurrentPeriodInquiry,
                (int) ActionType.ManagePeriodCaculations
            };

        public static List<PeriodDTOWithAction> periodList = new List<PeriodDTOWithAction>()
            {
                new PeriodDTOWithAction()
                    {
                        Id = 1,
                        Name = "دوره اول",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        ActionCodes = periodActionCode
                    }
                    ,
                    new PeriodDTOWithAction()
                    {
                        Id = 2,
                        Name = "دوره دوم",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        ActionCodes = periodActionCode
                    },
                    new PeriodDTOWithAction()
                    {
                        Id = 3,
                        Name = "دوره سوم",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        ActionCodes = periodActionCode
                    }
                    ,
                    new PeriodDTOWithAction()
                    {
                        Id = 4,
                        Name = "دوره چهارم",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        ActionCodes = periodActionCode
                    }
            };

    }
}
