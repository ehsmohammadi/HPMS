using System;
using System.Collections.Generic;
using MITD.PMS.Integration.Data.Contract;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.Domain.Contract;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class UnitIndexConverter : IUnitIndexConverter
    {

        #region Fields

        private readonly IUnitIndexDataProvider unitIndexDataProvider;
        private readonly IUnitIndexServiceWrapper unitIndexService;
        private readonly IUnitIndexInPeriodServiceWrapper unitIndexAssignmentService;

        #endregion

        #region Constructors

        public UnitIndexConverter(IUnitIndexDataProvider unitIndexDataProvider,
            IUnitIndexServiceWrapper unitIndexService, IUnitIndexInPeriodServiceWrapper unitIndexAssignmentService)
        {
            this.unitIndexDataProvider = unitIndexDataProvider;
            this.unitIndexService = unitIndexService;
            this.unitIndexAssignmentService = unitIndexAssignmentService;
        }

        #endregion

        #region Public Methods

        public void ConvertUnitIndex(Period period)
        {
            var sourceUnitIndexListId = unitIndexDataProvider.GetUnitIndexListId();
            foreach (var sourceUnitIndexId in sourceUnitIndexListId)
            {
                var sourceUnitIndexDTO = unitIndexDataProvider.GetBy(sourceUnitIndexId);
                var desUnitIndexDTO = createDestinationUnitIndex(sourceUnitIndexDTO);
                unitIndexService.AddUnitIndex((unitIndex, addUnitIndexExp) =>
                {
                    if (addUnitIndexExp != null)
                    {
                        handleException(addUnitIndexExp);
                    }
                    else
                    {
                        var periodUnitIndexDTO = createPeriodUnitIndexDTO(unitIndex);
                        unitIndexAssignmentService.AddUnitIndexInPeriod((res, exp) =>
                        {
                            if (exp != null)
                            {
                                handleException(exp);
                            }
                            else
                            {

                            }

                        }, periodUnitIndexDTO);

                    }

                }, desUnitIndexDTO);
            }
        }

        #endregion

        #region Private Methods

        private UnitIndexDTO createDestinationUnitIndex(UnitIndexIntegrationDTO sourceUnitIndex)
        {
            var res = new UnitIndexDTO
            {
                Name = sourceUnitIndex.Title,
                ParentId = PMSCostantData.UnitIndexCategoryId,
                CustomFields = new List<CustomFieldDTO>
                {
                    new CustomFieldDTO
                    {
                        Id = PMSCostantData.UnitIndexFieldId,
                    }
                },
                DictionaryName = "CUI" + Guid.NewGuid(),
                TransferId = sourceUnitIndex.TransferId
            };
            return res;
        }

        private UnitIndexInPeriodDTO createPeriodUnitIndexDTO(UnitIndexDTO unitIndex)
        {
            var res = new UnitIndexInPeriodDTO();
            return res;
        }

        private void handleException(Exception exception)
        {
            throw exception;
        }



        #endregion

    }
}
