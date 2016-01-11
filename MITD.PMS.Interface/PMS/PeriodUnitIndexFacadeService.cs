using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Contracts.Fasade;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMSSecurity.Domain;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
    //  [Interceptor(typeof(Interception))]
    public class PeriodUnitIndexFacadeService : IPeriodUnitIndexServiceFasade
    { 
        private readonly IUnitIndexService UnitIndexService;
        private readonly IMapper<AbstractUnitIndex, AbstractUnitIndexInPeriodDTO> UnitIndexMapper;
        private readonly IMapper<AbstractUnitIndex, AbstractUnitIndexInPeriodDTOWithActions> UnitIndexWithActionsMapper;
        private readonly IMapper<SharedUnitIndexCustomField, AbstractCustomFieldDescriptionDTO> abstractCustomFieldDtoMapper;
        private readonly IUnitIndexRepository UnitIndexRep;

        public PeriodUnitIndexFacadeService(
            IUnitIndexService UnitIndexService,
            IMapper<AbstractUnitIndex, AbstractUnitIndexInPeriodDTO> UnitIndexMapper,
            IMapper<AbstractUnitIndex, AbstractUnitIndexInPeriodDTOWithActions> UnitIndexWithActionsMapper,
            IMapper<SharedUnitIndexCustomField, AbstractCustomFieldDescriptionDTO> abstractCustomFieldDtoMapper,
            IUnitIndexRepository UnitIndexRep
            )
        {
            this.UnitIndexService = UnitIndexService;
            this.UnitIndexMapper = UnitIndexMapper;
            this.UnitIndexWithActionsMapper = UnitIndexWithActionsMapper;
            this.abstractCustomFieldDtoMapper = abstractCustomFieldDtoMapper;
            this.UnitIndexRep = UnitIndexRep;
        }


        public IEnumerable<AbstractUnitIndexInPeriodDTOWithActions> GetAllAbstractUnitIndices(long periodId)
        {                  
            var abstractList = UnitIndexRep.GetAll(new PeriodId(periodId));
                return abstractList.Select(r => UnitIndexWithActionsMapper.MapToModel(r));
        }

        public AbstractUnitIndexInPeriodDTO GetAbstarctUnitIndexById(long id)
        {
            var abstractUnitIndex = UnitIndexRep.GetById(new AbstractUnitIndexId(id));
            var abstractIndexDto = UnitIndexMapper.MapToModel(abstractUnitIndex);

           
            if (abstractIndexDto is UnitIndexInPeriodDTO)
            {
                var UnitIndex = (UnitIndex)abstractUnitIndex;
                var sharedUnitIndexCustomFlds = UnitIndexService.GetSharedUnitIndexCustomField(UnitIndex.SharedUnitIndexId,
                                                                     UnitIndex.CustomFieldValues.Keys.ToList());
                foreach (var customFld in sharedUnitIndexCustomFlds)
                {
                    var abstractDescCustomFld = abstractCustomFieldDtoMapper.MapToModel(customFld);
                    abstractDescCustomFld.Value = UnitIndex.CustomFieldValues[customFld.Id];
                    ((UnitIndexInPeriodDTO)abstractIndexDto).CustomFields.Add(abstractDescCustomFld); 
                }

            }

            return abstractIndexDto;
        }

        public AbstractUnitIndexInPeriodDTO AddUnitIndex(UnitIndexInPeriodDTO abstractIndex)
        {
            //var UnitIndex = UnitIndexService.AddUnitIndex(new PeriodId(abstractIndex.PeriodId), new AbstractUnitIndexId(abstractIndex.ParentId.Value),
            //                                    new SharedUnitIndexId(abstractIndex.UnitIndexId)
            //                                    , abstractIndex.CustomFields.ToDictionary(itm => new SharedUnitIndexCustomFieldId(itm.Key.Id), itm => itm.Value)
            //                                    , abstractIndex.IsInquireable);

            var UnitIndex = UnitIndexService.AddUnitIndex(new PeriodId(abstractIndex.PeriodId), new AbstractUnitIndexId(abstractIndex.ParentId.Value),
                                                new SharedUnitIndexId(abstractIndex.UnitIndexId)
                                                , abstractIndex.CustomFields.ToDictionary(itm => new SharedUnitIndexCustomFieldId(itm.Id), itm => itm.Value)
                                                , abstractIndex.IsInquireable, abstractIndex.CalculationOrder, abstractIndex.CalculationLevel);

            return UnitIndexMapper.MapToModel(UnitIndex);

        }

        public AbstractUnitIndexInPeriodDTO AddUnitIndexGroup(UnitIndexGroupInPeriodDTO abstractIndex)
        {
            var UnitIndexGroup = UnitIndexService.AddUnitIndexGroup(new PeriodId(abstractIndex.PeriodId), 
                (abstractIndex.ParentId == null) ? null : new AbstractUnitIndexId(abstractIndex.ParentId.Value),
                                                abstractIndex.Name, abstractIndex.DictionaryName);
            return UnitIndexMapper.MapToModel(UnitIndexGroup);
        }

        public AbstractUnitIndexInPeriodDTO UpdateUnitIndex(UnitIndexInPeriodDTO abstractIndex)
        {
            var UnitIndex = UnitIndexService.UpdateUnitIndex(new AbstractUnitIndexId(abstractIndex.Id)
                , new AbstractUnitIndexId(abstractIndex.ParentId.Value)
                , abstractIndex.CustomFields.ToDictionary(itm=>new SharedUnitIndexCustomFieldId(itm.Id),itm=>itm.Value),
                abstractIndex.IsInquireable, abstractIndex.CalculationOrder, abstractIndex.CalculationLevel
                );
            return UnitIndexMapper.MapToModel(UnitIndex);
           
        }

        public AbstractUnitIndexInPeriodDTO UpdateUnitIndexGroup(UnitIndexGroupInPeriodDTO abstractIndex)
        {
            var UnitIndexGroup = UnitIndexService.UpdateUnitIndexGroup(new AbstractUnitIndexId(abstractIndex.Id)
               , (abstractIndex.ParentId.HasValue) ? new AbstractUnitIndexId(abstractIndex.ParentId.Value) : null
               , abstractIndex.Name, abstractIndex.DictionaryName);
            return UnitIndexMapper.MapToModel(UnitIndexGroup);
        }

        public string  DeleteAbstractUnitIndex(long id)
        {
            UnitIndexService.DeleteAbstractUnitIndex(new AbstractUnitIndexId(id));
                return "UnitIndex deleted successfully";
        }

        [RequiredPermission(ActionType.ShowUnitIndexInPeriod)] 
        public IEnumerable<AbstractUnitIndexInPeriodDTO> GetAllUnitIndices(long periodId)
        {
            var abstractList = UnitIndexRep.GetAllUnitIndex(new PeriodId(periodId));
            return abstractList.Select(r => UnitIndexMapper.MapToModel(r));
        }

        public IEnumerable<AbstractUnitIndexInPeriodDTO> GetAllUnitIndexGrroups(long periodId)
        {
            var abstractList = UnitIndexRep.GetAllUnitIndexGroup(new PeriodId(periodId));
            return abstractList.Select(r => UnitIndexMapper.MapToModel(r));
        }
    }
}
