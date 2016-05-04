using System;
using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IJobIndexFacadeService : IFacadeService
    {

        PageResultDTO<AbstractJobIndexDTOWithActions> GetAllJobIndicesWithPagination(int pageSize, int pageIndex, QueryStringConditions queryStringCondition);
        PageResultDTO<AbstractJobIndexDTOWithActions> GetAllJobIndexCategoriesWithPagination(int pageSize, int pageIndex, QueryStringConditions queryStringCondition);
        
        IEnumerable<AbstractJobIndexDTOWithActions> GetAllAbstractJobIndices();
        AbstractIndex GetAbstarctJobIndexById(long id);
        AbstractIndex GetAbstarctJobIndexByTransferId(Guid transferId);
        AbstractIndex AddJobIndex(JobIndexDTO abstractjobIndex);
        AbstractIndex AddJobIndexCategory(JobIndexCategoryDTO jobIndexCategory);
        AbstractIndex UpdateJobIndex(JobIndexDTO abstractjobIndex);
        AbstractIndex UpdateJobIndexCategory(JobIndexCategoryDTO abstractjobIndex);
        string DeleteAbstractJobIndex(long id);

        IList<AbstractIndex> GetAllJobIndices();
        IList<AbstractIndex> GetAllJobIndexCategories();
        
    }
}
