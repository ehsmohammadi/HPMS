using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.PMS.Integration.Data.Contract;
using MITD.PMS.Integration.Data.Contract.DTO;
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
        private readonly IEventPublisher publisher;
        private Object lockThis = new Object();

        #endregion

        #region Constructors

        public UnitIndexConverter(IUnitIndexDataProvider unitIndexDataProvider,
            IUnitIndexServiceWrapper unitIndexService, IUnitIndexInPeriodServiceWrapper unitIndexAssignmentService, IEventPublisher publisher)
        {
            this.unitIndexDataProvider = unitIndexDataProvider;
            this.unitIndexService = unitIndexService;
            this.unitIndexAssignmentService = unitIndexAssignmentService;
            this.publisher = publisher;
        }

        #endregion

        #region Public Methods

        public void ConvertUnitIndex(Period period)
        {
            var unitIndexInperiodList = new List<UnitIndexInPeriodDTO>();
            var sourceUnitIndexListId = unitIndexDataProvider.GetUnitIndexListId();
            foreach (var sourceUnitIndexId in sourceUnitIndexListId)
            {

                var sourceUnitIndexDTO = unitIndexDataProvider.GetBy(sourceUnitIndexId);
                var desUnitIndexDTO = createDestinationUnitIndex(sourceUnitIndexDTO);
                unitIndexService.AddUnitIndex((unitIndexWithOutCf, addUnitIndexExp) =>
                {
                    if (addUnitIndexExp != null)
                    {
                        handleException(addUnitIndexExp);
                    }
                    else
                    {
                        unitIndexService.GetUnitIndex((unitIndexWithCf, getUnitIndexExp) =>
                        {
                            if (getUnitIndexExp != null)
                                handleException(getUnitIndexExp);
                            else
                            {
                                var periodUnitIndexDTO = createPeriodUnitIndexDTO(unitIndexWithCf, period);
                                unitIndexAssignmentService.AddUnitIndexInPeriod((res, exp) =>
                                {
                                    if (exp != null)
                                    {
                                        handleException(exp);
                                    }
                                    else
                                    {
                                        unitIndexInperiodList.Add(res);
                                        if (unitIndexInperiodList.Count == sourceUnitIndexListId.Count)
                                            publisher.Publish(new UnitIndexConverted(unitIndexInperiodList));
                                    }

                                }, periodUnitIndexDTO);
                            }
                        }, unitIndexWithOutCf.Id);
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
                        Name = "sdfsdfsd",
                        DictionaryName = "ksdkfhjskdfjs",
                        EntityId = 1,
                        MaxValue = 10,
                        MinValue = 1,
                        TypeId = "string",
                    }
                },
                DictionaryName = sourceUnitIndex.ID.ToString(),
                TransferId = sourceUnitIndex.TransferId
            };
            return res;
        }

        private UnitIndexInPeriodDTO createPeriodUnitIndexDTO(UnitIndexDTO unitIndex, Period period)
        {
            var res = new UnitIndexInPeriodDTO
            {

                ParentId = PMSCostantData.UnitIndexGroup,
                CalculationLevel = 1,
                CalculationOrder = 1,
                IsInquireable = true,
                Name = unitIndex.Name,
                DictionaryName = unitIndex.DictionaryName,
                UnitIndexId = unitIndex.Id,
                PeriodId = period.Id,
                CustomFields = unitIndex.CustomFields.Select(c => new AbstractCustomFieldDescriptionDTO
                {
                    Id = c.Id,
                    Name = "fake",
                    Value = "1"
                }).ToList()

            };
            return res;
        }

        private void handleException(Exception exception)
        {
            throw new Exception("bad shod", exception);
        }
        #endregion

    }
}
