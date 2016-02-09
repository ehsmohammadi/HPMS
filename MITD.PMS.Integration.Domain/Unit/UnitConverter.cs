
using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;

using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class UnitConverter : IUnitConverter
    {
        #region Fields
        private readonly IUnitDataProvider unitDataProvider;
        private readonly IUnitServiceWrapper unitService;
        private readonly IUnitInPeriodServiceWrapper unitInPeriodServiceWrapper;
        private List<UnitIndexInPeriodDTO> unitIndexInperiodList;
        private UnitIntegrationDTO root;
        private List<UnitInPeriodDTO> unitInPeriodList = new List<UnitInPeriodDTO>();
        private int totalUnitsCount;
        private readonly IEventPublisher publisher;
        #endregion

        #region Constructors
        public UnitConverter(IUnitDataProvider unitDataProvider, IUnitServiceWrapper unitService, IUnitInPeriodServiceWrapper unitInPeriodServiceWrapper, IEventPublisher publisher)
        {
            this.unitDataProvider = unitDataProvider;
            this.unitService = unitService;
            this.unitInPeriodServiceWrapper = unitInPeriodServiceWrapper;
            this.publisher = publisher;
        }

        #endregion

        #region public methods
        public void ConvertUnits(Period period, List<UnitIndexInPeriodDTO> unitIndexInperiodListParam)
        {
            Console.WriteLine("Starting units Convert progress...");
            unitIndexInperiodList = unitIndexInperiodListParam;
            root = unitDataProvider.GetRoot();
            totalUnitsCount = unitDataProvider.GetCount();
            convertUnit_Rec(root, period.Id, null);
            publisher.Publish(new UnitConverted(unitInPeriodList));
        }

      
        #endregion

        #region Private methods


        private void convertUnit_Rec(UnitIntegrationDTO sourceUnitDTO, long periodId, long? unitParentIdParam)
        {
            var desUnitDTO = createDestinationUnit(sourceUnitDTO);
            var unit = unitService.AddUnit(desUnitDTO);
            var unitInPriodAssignment = createDestinationUnitInPeriod(unit, unitParentIdParam);
            var res = unitInPeriodServiceWrapper.AddUnitInPeriod(periodId, unitInPriodAssignment);
            unitInPeriodList.Add(res);
            var unitDataChildIdList = unitDataProvider.GetChildIDs(sourceUnitDTO.ID);
            foreach (var unitDataChildId in unitDataChildIdList)
            {
                var unitdataChid = unitDataProvider.GetUnitDetail(unitDataChildId);
                convertUnit_Rec(unitdataChid, periodId, res.UnitId);
            }
            Console.WriteLine("Unit convert progress state: " + unitInPeriodList.Count + " From " + totalUnitsCount.ToString());

        }

        private void handleException(Exception exception)
        {
            throw new Exception("Error In Add Unit", exception);
        }


        private UnitDTO createDestinationUnit(UnitIntegrationDTO sourceUnit)
        {
            var res = new UnitDTO
            {
                Name = sourceUnit.UnitName,

                CustomFields = new List<CustomFieldDTO>(),
                DictionaryName = sourceUnit.ID.ToString(),
                TransferId = sourceUnit.TransferId
                
            };
            return res;
        }


        private UnitInPeriodDTO createDestinationUnitInPeriod(UnitDTO unit, long? parentId)
        {
            var res = new UnitInPeriodDTO
            {
                Name = unit.Name,
                CustomFields = new List<CustomFieldDTO>(),
                UnitId = unit.Id,
                ParentId = parentId,
                UnitIndices = unitIndexInperiodList.Select(c => new UnitInPeriodUnitIndexDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsInquireable = c.IsInquireable,
                    ShowforLowLevel =true,
                    ShowforSameLevel = true,
                    ShowforTopLevel = true
                }).ToList()
            };
            return res;
        }


        #endregion
    }


}


