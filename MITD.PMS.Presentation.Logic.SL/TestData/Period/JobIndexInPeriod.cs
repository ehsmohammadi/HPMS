using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public static partial class TestData
    {
        private static List<int> jobIndexInPeriodActionCode = new List<int>
            {
                (int) ActionType.ModifyJobIndexInPeriod,
                (int) ActionType.DeleteJobIndexInPeriod
            };

        private static List<int> jobIndexGroupActionCode = new List<int>
            {
                (int) ActionType.AddJobIndexInPeriod,
                (int) ActionType.AddJobIndexGroupInPeriod,
                (int) ActionType.ModifyJobIndexGroupInPeriod,
                (int) ActionType.DeleteJobIndexGroupInPeriod
            };


      

        public static List<AbstractIndexInPeriodDTOWithActions> abstractIndexInPeriodDtoWithActionses = new List<AbstractIndexInPeriodDTOWithActions>
            {
                new JobIndexGroupInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexGroupActionCode,
                        Name = "شاخص های عمومی",
                        Id = 1,
                        ParentId = null
                    },
                new JobIndexGroupInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexGroupActionCode,
                        Name = "شاخص های تخصصی",
                        Id = 2,
                        ParentId = null
                    },
                    new JobIndexGroupInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexGroupActionCode,
                        Name = "شاخص های یکسان ساز حجم کار",
                        Id = 3,
                        ParentId = null
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==10).Name ,
                        Id = 4,
                        ParentId = 1,
                        JobIndexId = JobIndexList.Single(p=>p.Id==10).Id 
                        
                    },
                     new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==11).Name ,
                        Id = 5,
                        ParentId = 1,
                        JobIndexId = JobIndexList.Single(p=>p.Id==11).Id
                    },
                     new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==12).Name ,
                        Id = 6,
                        ParentId = 1,
                        JobIndexId = JobIndexList.Single(p=>p.Id==12).Id
                    },
                     new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==13).Name ,
                        Id = 7,
                        ParentId = 1,
                        JobIndexId = JobIndexList.Single(p=>p.Id==13).Id
                    },
                     new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==14).Name ,
                        Id = 8,
                        ParentId = 1,
                        JobIndexId = JobIndexList.Single(p=>p.Id==14).Id
                    },
                     new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==15).Name ,
                        Id = 9,
                        ParentId = 1,
                        JobIndexId = JobIndexList.Single(p=>p.Id==15).Id
                    },
                     new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==16).Name ,
                        Id = 10,
                        ParentId = 1,
                        JobIndexId = JobIndexList.Single(p=>p.Id==16).Id
                    },
                     new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==17).Name ,
                        Id = 11,
                        ParentId = 1,
                        JobIndexId = JobIndexList.Single(p=>p.Id==17).Id
                    },
                     new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==18).Name ,
                        Id = 12,
                        ParentId = 2,
                        JobIndexId = JobIndexList.Single(p=>p.Id==18).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==19).Name ,
                        Id = 13,
                        ParentId = 2,
                        JobIndexId = JobIndexList.Single(p=>p.Id==19).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==20).Name ,
                        Id = 14,
                        ParentId = 2,
                        JobIndexId = JobIndexList.Single(p=>p.Id==20).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==21).Name ,
                        Id = 15,
                        ParentId = 2,
                        JobIndexId = JobIndexList.Single(p=>p.Id==21).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==22).Name ,
                        Id = 16,
                        ParentId = 2,
                        JobIndexId = JobIndexList.Single(p=>p.Id==22).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==23).Name ,
                        Id = 17,
                        ParentId = 2,
                        JobIndexId = JobIndexList.Single(p=>p.Id==23).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==24).Name ,
                        Id = 18,
                        ParentId = 2,
                        JobIndexId = JobIndexList.Single(p=>p.Id==24).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==25).Name ,
                        Id = 19,
                        ParentId = 2,
                        JobIndexId = JobIndexList.Single(p=>p.Id==25).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==26).Name ,
                        Id = 20,
                        ParentId = 2,
                        JobIndexId = JobIndexList.Single(p=>p.Id==26).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==27).Name ,
                        Id = 21,
                        ParentId = 2,
                        JobIndexId = JobIndexList.Single(p=>p.Id==27).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==28).Name ,
                        Id = 22,
                        ParentId = 2,
                        JobIndexId = JobIndexList.Single(p=>p.Id==28).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==29).Name ,
                        Id = 23,
                        ParentId = 2,
                        JobIndexId = JobIndexList.Single(p=>p.Id==29).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==30).Name ,
                        Id = 24,
                        ParentId = 3,
                        JobIndexId = JobIndexList.Single(p=>p.Id==30).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==31).Name ,
                        Id = 25,
                        ParentId = 3,
                        JobIndexId = JobIndexList.Single(p=>p.Id==31).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==32).Name ,
                        Id = 26,
                        ParentId = 3,
                        JobIndexId = JobIndexList.Single(p=>p.Id==32).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==33).Name ,
                        Id = 27,
                        ParentId = 3,
                        JobIndexId = JobIndexList.Single(p=>p.Id==33).Id
                    },
                    new JobIndexInPeriodDTOWithActions
                    {
                        ActionCodes = jobIndexInPeriodActionCode,
                        Name =  JobIndexList.Single(p=>p.Id==34).Name ,
                        Id = 28,
                        ParentId = 3,
                        JobIndexId = JobIndexList.Single(p=>p.Id==34).Id
                    },

            };
    }
}
