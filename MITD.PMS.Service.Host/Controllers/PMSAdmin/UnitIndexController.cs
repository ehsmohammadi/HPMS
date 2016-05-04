using System;
using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Contracts.Fasade;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{

    [Authorize]
    public class UnitIndexController : ApiController
    {
        private const string unitIndexClassType = "UnitIndex";
        private const string unitIndexCategoryClassType = "UnitIndexCategory";

        private readonly IUnitIndexFacadeService unitIndexFacadeService;

       

        public UnitIndexController(IUnitIndexFacadeService unitIndexService)
        {
            unitIndexFacadeService = unitIndexService;
        }

        public PageResultDTO<AbstractUnitIndexDTOWithActions> GetAllUnitIndices(int pageSize, int pageIndex, string filter = "", string sortBy = "", string classType = unitIndexClassType)
        {
            var queryStringCondition = new QueryStringConditions { SortBy = sortBy, Filter = filter };
            if (classType.ToLower() == unitIndexClassType)
                return unitIndexFacadeService.GetAllUnitIndicesWithPagination(pageSize, pageIndex, queryStringCondition);
            else
                return unitIndexFacadeService.GetAllUnitIndexCategoriesWithPagination(pageSize, pageIndex, queryStringCondition);
           

        }


        public IList<AbstractIndex> GetAllUnitIndex(string typeOf)
        {
            if (typeOf.ToLower() == unitIndexClassType.ToLower())
                return unitIndexFacadeService.GetAllUnitIndices();
            else
                return unitIndexFacadeService.GetAllUnitIndexCategories();
        }


        public IEnumerable<AbstractUnitIndexDTOWithActions> GetAllAbstractUnitIndices()
        {
            return unitIndexFacadeService.GetAllAbstractUnitIndices();
        }

        public AbstractIndex GetUnitIndex(long id)
        {
           return unitIndexFacadeService.GetAbstarctUnitIndexById(id);
           
        }

        public AbstractIndex GetUnitIndex(Guid transferId)
        {
            return unitIndexFacadeService.GetAbstarctUnitIndexByTransferId(transferId);

        }

        public AbstractIndex PostUnitIndex(AbstractIndex abstractIndex)
        {
            if (abstractIndex is UnitIndexDTO)
                return unitIndexFacadeService.AddUnitIndex((UnitIndexDTO)abstractIndex);
            else
                return unitIndexFacadeService.AddUnitIndexCategory((UnitIndexCategoryDTO)abstractIndex);
        }

        public AbstractIndex PutUnitIndex(AbstractIndex abstractIndex)
        {
            if (abstractIndex is UnitIndexDTO)
                return unitIndexFacadeService.UpdateUnitIndex((UnitIndexDTO)abstractIndex);
            else
                return unitIndexFacadeService.UpdateUnitIndexCategory((UnitIndexCategoryDTO)abstractIndex);
        }

       

        public string DeleteAbstarctUnitIndex(long id)
        {
            return unitIndexFacadeService.DeleteAbstractUnitIndex(id);
        }

       
    }
}