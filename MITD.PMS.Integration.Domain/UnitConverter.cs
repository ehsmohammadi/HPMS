
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Integration.Data.Contract;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Integration.Data.EF;
using MITD.PMS.Integration.Domain.Contract;
using Nito.AsyncEx;


namespace MITD.PMS.Integration.Domain
{
    public class UnitConverter : IUnitConverter
    {
        #region Fields
        private IUnitDataProvider unitDataProvider;
        //private IUnitService unitService;
        private UnitServiceWrapper unitService;

        //private IUnitAssignmentService unitAssignmentService;
        private UnitInPeriodServiceWrapper unitAssignmentService;
        #endregion

        #region Constructors
        public UnitConverter()//(IUnitDataProvider unitDataProvider, IUnitService unitService, IUnitAssignmentService unitAssignmentService)
        {
            //(new UnitDataPrivider(), new UnitServiceWrapper(new UserProvider()), new UnitAssignmentService());

            this.unitDataProvider = new UnitDataPrivider();
            this.unitService = new UnitServiceWrapper(new UserProvider());
            this.unitAssignmentService = new UnitInPeriodServiceWrapper(new UserProvider());

            //this.unitDataProvider = unitDataProvider;
            //this.unitService = unitService;
            //this.unitAssignmentService = unitAssignmentService;
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

                unitAssignmentService.AddUnitInPeriod((assignmenteResult, assignmentExp) =>
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
        public void ConvertUnits(long periodId)
        {
            var root = unitDataProvider.GetRoot();
            convertUnit_Rec(root, periodId, null);


            //RunConvertUnitSync(PeriodId);
        }

      
        #endregion

        #region Private methods

        #region convertUnit_Rec

        private void convertUnit_Rec(UnitNodeIntegrationDTO unitData, long periodId, long? unitParentIdParam)
        {
            var unit = new UnitDTO
            {
                //Id = unitData.ID,
                Name = unitData.UnitName,
                DictionaryName = "Unit" + Guid.NewGuid(),
                CustomFields = new List<CustomFieldDTO>()
            };
            unitService.AddUnit((unitResult, exp) =>
            {
                if (exp != null)
                    throw new Exception("Error in Add Unit!");

                UnitInPeriodDTO UnitInPriodAssignment = new UnitInPeriodDTO();

                //UnitInPriodAssignment = periodId;
                UnitInPriodAssignment.UnitId = unitResult.Id;
                UnitInPriodAssignment.ParentId = unitParentIdParam;
                //NewUnitID = unitResult.Id;

                unitAssignmentService.AddUnitInPeriod((assignmenteResult, assignmentExp) =>
                {
                    if (assignmentExp != null)
                    {
                        throw new Exception("Error in Assignment Unit!");

                    }
                    var unitDataChildIdList = unitDataProvider.GetChildIDs(unitData.ID);
                    foreach (var unitDataChildId in unitDataChildIdList)
                    {
                        var unitdataChid = unitDataProvider.GetUnitDetail(unitDataChildId);
                        convertUnit_Rec(unitdataChid, periodId, assignmenteResult.UnitId);
                    }

                },periodId, UnitInPriodAssignment);
            }, unit);

            //var unitParentId = AddUnit(unit, periodId, unitParentIdParam).Result;
        }

        #endregion

        #region Comment
        //private  UnitInPeriodDTO AddUnit(UnitDTO destinationUnit, long PeriodId, long? ParrentID)
        //{
        //    Task<UnitInPeriodDTO> ConUnit = AddUnitSAsync(destinationUnit, PeriodId, ParrentID);

        //    //var AddedUnit = await ConUnit;
        //    //return AddedUnit;
        //    return null;
        //}

        ////private  Task<UnitInPeriodDTO> AddUnitSAsync(UnitDTO destinationUnit, long PeriodId, long? ParrentID)
        private async Task<UnitInPeriodDTO> AddUnitSAsync(UnitDTO destinationUnit, long PeriodId, long? ParrentID)
        {
            UnitInPeriodDTO res = null;
            unitService.AddUnit((unitResult, exp) =>
            {
                if (exp != null)
                    throw new Exception("Error in Add Unit!");

                UnitInPeriodAssignmentDTO UnitInPriodAssignment = new UnitInPeriodAssignmentDTO();

                UnitInPriodAssignment.PeriodId = PeriodId;
                UnitInPriodAssignment.UnitId = unitResult.Id;
                UnitInPriodAssignment.ParentUnitId = ParrentID;
                //NewUnitID = unitResult.Id;

                unitAssignmentService.AddUnitInPeriod((assignmenteResult, assignmentExp) =>
                {
                    if (assignmentExp != null)
                    {
                        throw new Exception("Error in Assignment Unit!");

                    }
                    var x = new UnitInPeriodDTO();
                    x.UnitId = assignmenteResult.UnitId;
                    res = x;
                }, UnitInPriodAssignment);
            }, destinationUnit);
            return res;
        }

        #endregion

        #endregion
    }


}


