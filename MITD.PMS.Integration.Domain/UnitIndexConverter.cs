using System;
using System.Collections.Generic;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Integration.Domain.Contract;

namespace MITD.PMS.Integration.Domain
{
    class UnitIndexConverter : IUnitIndexConverter
    {

        #region Fields

        IUnitIndexServiceWrapper unitIndexService;
        IUnitIndexDataProvider unitIndexDataProvider;
        IUnitIndexInPeriodServiceWrapper unitIndexInPeriodService;
        #endregion



        public UnitIndexConverter(IUnitIndexDataProvider UnitIndexDataProvider, IUnitIndexServiceWrapper UnitIndexService, IUnitIndexInPeriodServiceWrapper UnitIndexInPeriodService)
        {
            this.unitIndexService = UnitIndexService;
            this.unitIndexDataProvider = UnitIndexDataProvider;
            this.unitIndexInPeriodService = UnitIndexInPeriodService;
        }

        #region Public Methods

        public void ConvertUnitIndex(Period period)
        {
            var UnitIndexList = unitIndexDataProvider.GetUnitIndexList();
            // Create Unit Index Category
            var PmsUnitIndexCategory = new UnitIndexCategoryDTO
            {
                Name = "گروه شاخص های سازمانی",
                DictionaryName = "UnitIndexCategoryDicName"
            };
            
            unitIndexService.AddUnitIndexCategory((unitIndexCategoryResult, exp) =>
            {
                if (exp != null)
                    throw new Exception("Error in Add UnitIndexCategory!");







                foreach (var UnitIndexItem in UnitIndexList)
                {
                    var PmsUnitIndex = new UnitIndexDTO
                    {
                        
                        Name = UnitIndexItem.Title,
                        ParentId = unitIndexCategoryResult.Id,
                        CustomFields=new  List<CustomFieldDTO>(),
                        DictionaryName="CUI"+Guid.NewGuid()
                    };
                    unitIndexService.AddUnitIndex((unitIndexResult, ueExp) =>
                    {
                        if (ueExp != null)
                        {
                            throw new Exception("Error in Assignment Unit!");
                        }
                        UnitIndexInPeriodDTO UnitIndexInPriodAssignment = new UnitIndexInPeriodDTO
                        {
                            
                        };

                        //UnitInPriodAssignment = periodId;
                        UnitIndexInPriodAssignment.Id = unitIndexCategoryResult.Id;
                        UnitIndexInPriodAssignment.UnitIndexId = unitIndexCategoryResult.Id;
                        //NewUnitID = unitResult.Id;



                    }, PmsUnitIndex);   
                }

                
            }, PmsUnitIndexCategory);
        }

        #endregion


        #region Private Methods



        #endregion


    }
}
