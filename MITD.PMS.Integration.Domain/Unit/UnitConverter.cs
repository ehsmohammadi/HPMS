
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        //private UnitInPeriodServiceWrapper unitAssignmentService;
        //private UnitServiceWrapper unitService;
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

        #region Temprory
        public void ConvertUnit_T(long periodId)
        {
            int Root = 0;
            Root = unitDataProvider.GetRoot().ID;
            var Childs = unitDataProvider.GetChildIDs(Root);
            DFScroll(Root, periodId, null);

        }

        private void DFScroll(int NodeID, long periodId, long? ParentUnitId)
        {
            //Insert Unit (Visit Node)
            long NewUnitID = 0;
            var sourceUnit = unitDataProvider.GetUnitDetail(NodeID);
            UnitDTO destinationUnit = new UnitDTO();
            destinationUnit.Id = sourceUnit.ID;
            destinationUnit.Name = sourceUnit.UnitName;
            destinationUnit.DictionaryName = "Unit" + Guid.NewGuid();
            destinationUnit.CustomFields = new List<CustomFieldDTO>();

            unitService.AddUnit((unitResult, exp) =>
            {
                if (exp != null)
                    throw new Exception("Error in Add Unit!");

                UnitInPeriodAssignmentDTO UnitInPriodAssignment = new UnitInPeriodAssignmentDTO();

                UnitInPriodAssignment.PeriodId = periodId;
                UnitInPriodAssignment.UnitId = unitResult.Id;
                UnitInPriodAssignment.ParentUnitId = ParentUnitId;
                NewUnitID = unitResult.Id;

                unitInPeriodServiceWrapper.AddUnitInPeriod((assignmenteResult, assignmentExp) =>
                {
                    if (assignmentExp != null)
                    {
                        throw new Exception("Error in Assignment Unit!");

                    }
                }, UnitInPriodAssignment);
            }, destinationUnit);

            // Get Childs
            var Childs = unitDataProvider.GetChildIDs(NodeID);

            //Call the method for Child
            foreach (var child in Childs)
            {
                DFScroll(child, periodId, NewUnitID);
            }
        }

        private async Task<int> RunConvertUnitSync(long PeriodId)
        {
            int Root = 0;
            Root = unitDataProvider.GetRoot().ID;
            var Childs = unitDataProvider.GetChildIDs(Root);
            //DFScroll(Root, PeriodId, null);
            Task<int> ConUnit = RunConvertUnitDFSAsync(Root, PeriodId, null);

            int ConvertedUnitCount = await ConUnit;
            return ConvertedUnitCount;
        }

        private async Task<int> RunConvertUnitDFSAsync(int NodeID, long periodId, long? ParentUnitId)
        {
            int Result = 0;
            //Insert Unit (Visit Node)
            long NewUnitID = 0;
            var sourceUnit = unitDataProvider.GetUnitDetail(NodeID);
            UnitDTO destinationUnit = new UnitDTO();
            destinationUnit.Id = sourceUnit.ID;
            destinationUnit.Name = sourceUnit.UnitName;
            destinationUnit.DictionaryName = "Unit" + Guid.NewGuid();
            destinationUnit.CustomFields = new List<CustomFieldDTO>();


            // Get Childs
            var Childs = unitDataProvider.GetChildIDs(NodeID);

            //Call the method for Child
            foreach (var child in Childs)
            {
                Task<int> ConUnit = RunConvertUnitDFSAsync(child, periodId, NewUnitID);
                //DFScroll(child, periodId, NewUnitID);
            }
            return Result;
        }


        #endregion

        #region public methods
        public void ConvertUnits(Period period, List<UnitIndexInPeriodDTO> unitIndexInperiodListParam)
        {
            unitIndexInperiodList = unitIndexInperiodListParam;
            root = unitDataProvider.GetRoot();
            totalUnitsCount = unitDataProvider.GetCount();
            convertUnit_Rec(root, period.Id, null);
        }

      
        #endregion

        #region Private methods


        private void convertUnit_Rec(UnitIntegrationDTO sourceUnitDTO, long periodId, long? unitParentIdParam)
        {
            //var unitInPeriodList = new List<UnitInPeriodDTO>();
            var desUnitDTO = createDestinationUnit(sourceUnitDTO);
            unitService.AddUnit((unit, addUnitExp) =>
            {
                if (addUnitExp != null)
                    handleException(addUnitExp);

                var unitInPriodAssignment = createDestinationUnitInPeriod(unit, unitParentIdParam);

                unitInPeriodServiceWrapper.AddUnitInPeriod((res, exp) =>
                {
                    if (exp != null)
                        handleException(exp);
                    unitInPeriodList.Add(res);
                    var unitDataChildIdList = unitDataProvider.GetChildIDs(sourceUnitDTO.ID);
                    foreach (var unitDataChildId in unitDataChildIdList)
                    {
                        var unitdataChid = unitDataProvider.GetUnitDetail(unitDataChildId);
                        convertUnit_Rec(unitdataChid, periodId, res.UnitId);
                    }
                    if (unitInPeriodList.Count==totalUnitsCount)
                    {
                        publisher.Publish(new UnitConverted(unitInPeriodList));
                    }
                }, periodId, unitInPriodAssignment);
            }, desUnitDTO);

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


