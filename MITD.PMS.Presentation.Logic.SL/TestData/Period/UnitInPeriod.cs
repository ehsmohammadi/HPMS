using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public static partial class TestData
    {
        private static List<int> UnitInPeriodActionCode = new List<int> { 21, 22 };

        public static List<UnitInPeriodDTOWithActions> UnitInPeriodList = new List<UnitInPeriodDTOWithActions>
            {
                new UnitInPeriodDTOWithActions
                    {
                        ActionCodes = UnitInPeriodActionCode,
                        Name = unitListWithAction.Single(u=>u.Id==1).Name,
                        UnitId = 1,
                        ParentId = null
                    },
                new UnitInPeriodDTOWithActions
                    {
                        ActionCodes = UnitInPeriodActionCode,
                        Name = unitListWithAction.Single(u=>u.Id==2).Name,
                        UnitId = 2,
                        ParentId = 1
                    },
                new UnitInPeriodDTOWithActions
                    {
                        ActionCodes = UnitInPeriodActionCode,
                        Name = unitListWithAction.Single(u=>u.Id==3).Name,
                        UnitId = 3,
                        ParentId = 1
                    },
                new UnitInPeriodDTOWithActions
                    {
                        ActionCodes = UnitInPeriodActionCode,
                        Name = unitListWithAction.Single(u=>u.Id==4).Name,
                        UnitId = 4,
                        ParentId = 2
                    },
            };

        public static List<UnitDTO> unitList = new List<UnitDTO>
            {
                new UnitDTO
                    {
                        Id = unitListWithAction.Single(u=>u.Id==1).Id,
                        Name = unitListWithAction.Single(u=>u.Id==1).Name,
                        DictionaryName = unitListWithAction.Single(u=>u.Id==1).DictionaryName
                    },
                    new UnitDTO
                    {
                        Id = unitListWithAction.Single(u=>u.Id==2).Id,
                        Name = unitListWithAction.Single(u=>u.Id==2).Name,
                        DictionaryName = unitListWithAction.Single(u=>u.Id==2).DictionaryName
                    },
                    new UnitDTO
                    {
                        Id = unitListWithAction.Single(u=>u.Id==3).Id,
                        Name = unitListWithAction.Single(u=>u.Id==3).Name,
                        DictionaryName = unitListWithAction.Single(u=>u.Id==3).DictionaryName
                    },
                    new UnitDTO
                    {
                        Id = unitListWithAction.Single(u=>u.Id==4).Id,
                        Name = unitListWithAction.Single(u=>u.Id==4).Name,
                        DictionaryName = unitListWithAction.Single(u=>u.Id==4).DictionaryName
                    },
            };

        public static List<UnitInPeriodDTO> UnitInPeriodDTODescriptionList=new List<UnitInPeriodDTO>
            {
                new UnitInPeriodDTO
                    {
                        
                        Name = UnitInPeriodList.Single(u=>u.Id==1).Name,
                        UnitId = UnitInPeriodList.Single(u=>u.Id==1).Id,
                      
                    },
                    new UnitInPeriodDTO
                    {
                        
                        Name = UnitInPeriodList.Single(u=>u.Id==2).Name,
                        UnitId = UnitInPeriodList.Single(u=>u.Id==2).Id,
                      
                    },
                    new UnitInPeriodDTO
                    {
                        
                        Name = UnitInPeriodList.Single(u=>u.Id==3).Name,
                        UnitId = UnitInPeriodList.Single(u=>u.Id==3).Id,
                      
                    },
                    new UnitInPeriodDTO
                    {
                        
                        Name = UnitInPeriodList.Single(u=>u.Id==4).Name,
                        UnitId = UnitInPeriodList.Single(u=>u.Id==4).Id,
                      
                    },
            };
    }
}
