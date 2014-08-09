using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.Units;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
   // [Interceptor(typeof(Interception))]
    public class UnitFacadeService : IUnitFacadeService
    { 
        private readonly IMapper<Unit, UnitDTOWithActions> unitWithActionMapper;
        private readonly IMapper<Unit, UnitDTO> unitMapper;
        private readonly IUnitService unitService;
        private readonly IUnitRepository unitRep;

        public UnitFacadeService(IMapper<Unit, UnitDTOWithActions> unitWithActionMapper,
                                        IMapper<Unit,UnitDTO> unitMapper, 
                                        IUnitService unitService,
                                        IUnitRepository unitRep)
        {
            this.unitWithActionMapper = unitWithActionMapper;
            this.unitMapper = unitMapper;
            this.unitService = unitService;
            this.unitRep = unitRep;
        }

        public PageResultDTO<UnitDTOWithActions> GetAllUnits(int pageSize, int pageIndex, QueryStringConditions queryStringConditions)
        {
            
            var fs = new ListFetchStrategy<Unit>(Enums.FetchInUnitOfWorkOption.NoTracking);
            var sortBy = QueryConditionHelper.GetSortByDictionary(queryStringConditions.SortBy);
            foreach (var item in sortBy)
            {
                fs.SortCriterias.Add(new StringSortCriteria<Unit>(item.Key,
                                                                             (item.Value.ToUpper() == "ASC")
                                                                                 ? Enums.SortOrder.Ascending
                                                                               : Enums.SortOrder.Descending));
            }
            fs.OrderBy(x => x.Id);
            fs.WithPaging(pageSize, pageIndex);
            unitRep.FindBy(fs);
            var res = new PageResultDTO<UnitDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result = fs.PageCriteria.PageResult.Result.Select(r => unitWithActionMapper.MapToModel(r)).ToList();
            return res;
        }

        public UnitDTO AddUnit(UnitDTO dto)
        {
            var res = unitService.AddUnit(dto.Name,dto.DictionaryName);
            return unitMapper.MapToModel(res);
        }

        public UnitDTO UpdateUnit(UnitDTO dto)
        {
            //var unit = unitMapper.MapToEntity(dto);
            var res = unitService.UppdateUnit(new UnitId(dto.Id),dto.Name,dto.DictionaryName);
            return unitMapper.MapToModel(res);
        }

        public UnitDTO GetUnitById(long id)
        {
            var unit = unitRep.GetById(new UnitId(id));
            return unitMapper.MapToModel(unit);
        }

        public string DeleteUnit(long unitId)
        {
            unitService.DeleteUnit(new UnitId(unitId));
            return "عملیات با موفقیت انجام شد";
        }

        public List<UnitDTO> GetAllUnits()
        {
            var units = unitRep.GetAll();
            return units.Select(u => unitMapper.MapToModel(u)).ToList();
        }
    }
}
