using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IUnitIndexFacadeService : IFacadeService
    {

        PageResultDTO<AbstractUnitIndexDTOWithActions> GetAllUnitIndicesWithPagination(int pageSize, int pageIndex, QueryStringConditions queryStringCondition);
        PageResultDTO<AbstractUnitIndexDTOWithActions> GetAllUnitIndexCategoriesWithPagination(int pageSize, int pageIndex, QueryStringConditions queryStringCondition);
        
        IEnumerable<AbstractUnitIndexDTOWithActions> GetAllAbstractUnitIndices();
        AbstractIndex GetAbstarctUnitIndexById(long id);
        AbstractIndex AddUnitIndex(UnitIndexDTO abstractunitIndex);
        AbstractIndex AddUnitIndexCategory(UnitIndexCategoryDTO unitIndexCategory);
        AbstractIndex UpdateUnitIndex(UnitIndexDTO abstractunitIndex);
        AbstractIndex UpdateUnitIndexCategory(UnitIndexCategoryDTO abstractunitIndex);
        string DeleteAbstractUnitIndex(long id);


        IList<AbstractIndex> GetAllUnitIndices();
        IList<AbstractIndex> GetAllUnitIndexCategories();
    }
}
