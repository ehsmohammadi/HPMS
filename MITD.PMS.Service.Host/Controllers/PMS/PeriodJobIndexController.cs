using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Contracts.Fasade;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{

    [Authorize]
    public class PeriodJobIndexController : ApiController
    {
        private const string jobIndexClassType = "JobIndex";
        private const string jobIndexCategoryClassType = "JobIndexGroup";

        private readonly IPeriodJobIndexServiceFacade periodJobIndexService;



        public PeriodJobIndexController(IPeriodJobIndexServiceFacade periodJobIndexService
            )
        {
            this.periodJobIndexService = periodJobIndexService;
        }


        public IEnumerable<AbstractIndexInPeriodDTOWithActions> GetAllAbstractJobIndices(long periodId)
        {
            return periodJobIndexService.GetAllAbstractJobIndices(periodId);
        }

        public IEnumerable<AbstractIndexInPeriodDTO> GetAllJobIndices(long periodId, string typeOf)
        {
            if (typeOf.ToLower() == jobIndexClassType.ToLower())
                return periodJobIndexService.GetAllJobIndices(periodId);
            else
                return periodJobIndexService.GetAllJobIndexGrroups(periodId);
        }

        public AbstractIndexInPeriodDTO GetJobIndex(long periodId,long abstractId,string SelectedColumns="")
        {
            return periodJobIndexService.GetAbstarctJobIndexById(abstractId);
           
        }

        public AbstractIndexInPeriodDTO PostJobIndex(AbstractIndexInPeriodDTO abstractIndex)
        {
            if (abstractIndex is JobIndexInPeriodDTO)
                return periodJobIndexService.AddJobIndex((JobIndexInPeriodDTO)abstractIndex);
                return periodJobIndexService.AddJobIndexGroup((JobIndexGroupInPeriodDTO)abstractIndex);
        }


        public AbstractIndexInPeriodDTO PutJobIndex(AbstractIndexInPeriodDTO abstractIndex)
        {
            if (abstractIndex is JobIndexInPeriodDTO)
                return periodJobIndexService.UpdateJobIndex((JobIndexInPeriodDTO)abstractIndex);
                return periodJobIndexService.UpdateJobIndexGroup((JobIndexGroupInPeriodDTO)abstractIndex);
        }

        public string DeleteAbstarctJobIndex(long abstractId)
        {
            return periodJobIndexService.DeleteAbstractJobIndex(abstractId);
        }

       
    }
}