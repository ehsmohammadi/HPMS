using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Service.Host.Controllers
{

    [Authorize]
    public class JobIndexController : ApiController
    {
        private const string jobIndexClassType = "JobIndex";
        private const string jobIndexCategoryClassType = "JobIndexCategory";
        private readonly IJobIndexFacadeService jobIndexFacadeService;



        public JobIndexController(IJobIndexFacadeService jobIndexService)
        {
            jobIndexFacadeService = jobIndexService;
        }

        public PageResultDTO<AbstractJobIndexDTOWithActions> GetAllJobIndices(int pageSize, int pageIndex, string filter = "", string sortBy = "", string classType = jobIndexClassType)
        {
            var queryStringCondition = new QueryStringConditions { SortBy = sortBy, Filter = filter };
            if (classType.ToLower() == jobIndexClassType)
                return jobIndexFacadeService.GetAllJobIndicesWithPagination(pageSize, pageIndex, queryStringCondition);
            else
                return jobIndexFacadeService.GetAllJobIndexCategoriesWithPagination(pageSize, pageIndex, queryStringCondition);
           

        }

        public IList<AbstractIndex> GetAllJobIndex(string typeOf)
        {
            if (typeOf.ToLower() == jobIndexClassType.ToLower())
                return jobIndexFacadeService.GetAllJobIndices();
            else
                return jobIndexFacadeService.GetAllJobIndexCategories();
        }

        public IEnumerable<AbstractJobIndexDTOWithActions> GetAllAbstractJobIndices()
        {
            return jobIndexFacadeService.GetAllAbstractJobIndices();
        }

        public AbstractIndex GetJobIndex(long id)
        {
           return jobIndexFacadeService.GetAbstarctJobIndexById(id);
           
        }

        public AbstractIndex PostJobIndex(AbstractIndex abstractIndex)
        {
            if (abstractIndex is JobIndexDTO)
                return jobIndexFacadeService.AddJobIndex((JobIndexDTO)abstractIndex);
            else
                return jobIndexFacadeService.AddJobIndexCategory((JobIndexCategoryDTO)abstractIndex);
        }

        public AbstractIndex PutJobIndex(AbstractIndex abstractIndex)
        {
            if (abstractIndex is JobIndexDTO)
                return jobIndexFacadeService.UpdateJobIndex((JobIndexDTO)abstractIndex);
            else
                return jobIndexFacadeService.UpdateJobIndexCategory((JobIndexCategoryDTO)abstractIndex);
        }

        public string DeleteAbstarctJobIndex(long id)
        {
            return jobIndexFacadeService.DeleteAbstractJobIndex(id);
        }

       
    }
}