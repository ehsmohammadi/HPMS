﻿using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.UnitIndices;
using MITD.PMSSecurity.Domain;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
    [Interceptor(typeof(Interception))]
    public class UnitIndexFacadeService : IUnitIndexFacadeService
    {
        #region Fields
        private readonly IUnitIndexService unitIndexService;
        private readonly ICustomFieldRepository customFieldRep;
        private readonly IMapper<AbstractUnitIndex, AbstractIndex> unitIndexMapper;
        private readonly IMapper<AbstractUnitIndex, AbstractUnitIndexDTOWithActions> unitIndexWithActionsMapper;
        private readonly IMapper<CustomFieldType, CustomFieldDTO> customFieldDtoMapper;
        private readonly IUnitIndexRepository unitIndexRep;
        #endregion

        #region Ctor

        public UnitIndexFacadeService(
            IUnitIndexService unitIndexService,
            ICustomFieldRepository customFieldRep,
            IMapper<AbstractUnitIndex, AbstractIndex> unitIndexMapper,
            IMapper<AbstractUnitIndex, AbstractUnitIndexDTOWithActions> unitIndexWithActionsMapper,
            IMapper<CustomFieldType, CustomFieldDTO> customFieldDtoMapper,
            IUnitIndexRepository unitIndexRep
            )
        {
            this.unitIndexService = unitIndexService;
            this.customFieldRep = customFieldRep;
            this.unitIndexMapper = unitIndexMapper;
            this.unitIndexWithActionsMapper = unitIndexWithActionsMapper;
            this.customFieldDtoMapper = customFieldDtoMapper;
            this.unitIndexRep = unitIndexRep;
        }

        #endregion

        #region Public Methods
        [RequiredPermission(ActionType.ManageUnitIndices)]
        public PageResultDTO<AbstractUnitIndexDTOWithActions> GetAllUnitIndicesWithPagination(int pageSize, int pageIndex, QueryStringConditions queryStringConditions)
        {
            var fs = new ListFetchStrategy<UnitIndex>(Enums.FetchInUnitOfWorkOption.NoTracking);
            var sortBy = QueryConditionHelper.GetSortByDictionary(queryStringConditions.SortBy);
            foreach (var item in sortBy)
            {
                fs.SortCriterias.Add(new StringSortCriteria<UnitIndex>(item.Key,
                                                                             (item.Value.ToUpper() == "ASC")
                                                                                 ? Enums.SortOrder.Ascending
                                                                               : Enums.SortOrder.Descending));
            }
            fs.OrderBy(x => x.Id);
            fs.WithPaging(pageSize, pageIndex);
            unitIndexRep.GetAllUnitIndex(fs);
            var res = new PageResultDTO<AbstractUnitIndexDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result = fs.PageCriteria.PageResult.Result.Select(r => unitIndexWithActionsMapper.MapToModel(r)).ToList();
            return res;
        }

        [RequiredPermission(ActionType.ManageUnitIndices)]
        public PageResultDTO<AbstractUnitIndexDTOWithActions> GetAllUnitIndexCategoriesWithPagination(int pageSize, int pageIndex, QueryStringConditions queryStringConditions)
        {
            var fs = new ListFetchStrategy<UnitIndexCategory>(Enums.FetchInUnitOfWorkOption.NoTracking);
            var sortBy = QueryConditionHelper.GetSortByDictionary(queryStringConditions.SortBy);
            foreach (var item in sortBy)
            {
                fs.SortCriterias.Add(new StringSortCriteria<UnitIndexCategory>(item.Key,
                                                                             (item.Value.ToUpper() == "ASC")
                                                                                 ? Enums.SortOrder.Ascending
                                                                               : Enums.SortOrder.Descending));
            }
            fs.OrderBy(x => x.Id);
            fs.WithPaging(pageSize, pageIndex);
            unitIndexRep.GetAllUnitIndexCategory(fs);
            var res = new PageResultDTO<AbstractUnitIndexDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result = fs.PageCriteria.PageResult.Result.Select(r => unitIndexWithActionsMapper.MapToModel(r));
            return res;
        }

        [RequiredPermission(ActionType.ManageUnitIndices)]
        public IList<AbstractIndex> GetAllUnitIndices()
        {
            var unitIndexList = unitIndexRep.GetAllUnitIndex();
            return unitIndexList.Select(j => unitIndexMapper.MapToModel(j)).ToList();
        }

        [RequiredPermission(ActionType.ManageUnitIndices)]
        public IList<AbstractIndex> GetAllUnitIndexCategories()
        {
            var unitIndexList = unitIndexRep.GetAllUnitIndexCategory();
            return unitIndexList.Select(j => unitIndexMapper.MapToModel(j)).ToList();
        }


        [RequiredPermission(ActionType.ManageUnitIndices)]
        public IEnumerable<AbstractUnitIndexDTOWithActions> GetAllAbstractUnitIndices()
        {
            var abstractList = unitIndexRep.GetAll();
            return abstractList.Select(r => unitIndexWithActionsMapper.MapToModel(r)).ToList();
        }

        [RequiredPermission(ActionType.AddUnitIndex)]
        public AbstractIndex AddUnitIndex(UnitIndexDTO unitIndexDto)
        {
            if (unitIndexDto.ParentId != null)
            {
                var unitIndex = unitIndexService.AddUnitIndex(new AbstractUnitIndexId(unitIndexDto.ParentId.Value),
                    unitIndexDto.Name, unitIndexDto.DictionaryName
                    , unitIndexDto.CustomFields.Select(c => new CustomFieldTypeId(c.Id)).ToList()
                    , unitIndexDto.TransferId);
                return unitIndexMapper.MapToModel(unitIndex);
            }
            throw new Exception("UnitIndex ParentId is null");
        }

        [RequiredPermission(ActionType.AddUnitIndexCategory)]
        public AbstractIndex AddUnitIndexCategory(UnitIndexCategoryDTO unitIndexCategoryDto)
        {
            var unitIndexCat = unitIndexService.AddUnitIndexCategory(
                (unitIndexCategoryDto.ParentId == null) ? null : new AbstractUnitIndexId(unitIndexCategoryDto.ParentId.Value),
                                                unitIndexCategoryDto.Name, unitIndexCategoryDto.DictionaryName);
            return unitIndexMapper.MapToModel(unitIndexCat);
        }

        [RequiredPermission(ActionType.ModifyUnitIndex)]
        public AbstractIndex UpdateUnitIndex(UnitIndexDTO unitIndexDto)
        {
            if (unitIndexDto.ParentId != null)
            {
                var unitIndex = unitIndexService.UpdateUnitIndex(new AbstractUnitIndexId(unitIndexDto.Id)
                    , new AbstractUnitIndexId(unitIndexDto.ParentId.Value), unitIndexDto.Name, unitIndexDto.DictionaryName
                    , unitIndexDto.CustomFields.Select(c => new CustomFieldTypeId(c.Id)).ToList()
                    );
                return unitIndexMapper.MapToModel(unitIndex);
            }
            throw new Exception("UnitIndex ParentId is null");
        }

        [RequiredPermission(ActionType.ModifyUnitIndexCategory)]
        public AbstractIndex UpdateUnitIndexCategory(UnitIndexCategoryDTO unitIndexCatDto)
        {
            var unitIndexCat = unitIndexService.UpdateUnitIndexCategory(new AbstractUnitIndexId(unitIndexCatDto.Id)
                , (unitIndexCatDto.ParentId.HasValue) ? new AbstractUnitIndexId(unitIndexCatDto.ParentId.Value) : null
                , unitIndexCatDto.Name, unitIndexCatDto.DictionaryName);
            return unitIndexMapper.MapToModel(unitIndexCat);
        }

        [RequiredPermission(ActionType.DeleteUnitIndex)]
        [RequiredPermission(ActionType.DeleteUnitIndexCategory)]
        public string DeleteAbstractUnitIndex(long id)
        {
            unitIndexService.DeleteAbstractUnitIndex(new AbstractUnitIndexId(id));
            return "UnitIndex deleted successfully";
        }

        public AbstractIndex GetAbstarctUnitIndexById(long id)
        {
            var unitIndex = unitIndexRep.GetById(new AbstractUnitIndexId(id));
            var abstractIndexDto = unitIndexMapper.MapToModel(unitIndex);
            if (abstractIndexDto is UnitIndexDTO)
            {
                var customFieldList = customFieldRep.GetAllCustomField(new AbstractUnitIndexId(id));
                ((UnitIndexDTO)abstractIndexDto).CustomFields = customFieldList.Select(c => customFieldDtoMapper.MapToModel(c)).ToList();
            }

            return abstractIndexDto;
        }

        public AbstractIndex GetAbstarctUnitIndexByTransferId(Guid transferId)
        {
            var unitIndex = unitIndexRep.GetByTransferId(transferId);
            if (unitIndex == null)
                return null;
            var abstractIndexDto = unitIndexMapper.MapToModel(unitIndex);
            if (abstractIndexDto is UnitIndexDTO)
            {
                var customFieldList = customFieldRep.GetAllCustomField(unitIndex.Id);
                ((UnitIndexDTO)abstractIndexDto).CustomFields = customFieldList.Select(c => customFieldDtoMapper.MapToModel(c)).ToList();
            }

            return abstractIndexDto;
        }

        #endregion

    }
}
