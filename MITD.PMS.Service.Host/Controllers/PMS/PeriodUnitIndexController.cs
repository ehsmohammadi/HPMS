using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Contracts.Fasade;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{

    [Authorize]
    public class PeriodUnitIndexController : ApiController
    {
        private const string UnitIndexClassType = "UnitIndex";
        private const string UnitIndexCategoryClassType = "UnitIndexGroup";

        private readonly IPeriodUnitIndexServiceFasade periodUnitIndexService;



        public PeriodUnitIndexController(IPeriodUnitIndexServiceFasade periodUnitIndexService
            )
        {
            this.periodUnitIndexService = periodUnitIndexService;
        }


        public IEnumerable<AbstractUnitIndexInPeriodDTOWithActions> GetAllAbstractUnitIndices(long periodId)
        {
            return periodUnitIndexService.GetAllAbstractUnitIndices(periodId);
        }

        public IEnumerable<AbstractUnitIndexInPeriodDTO> GetAllUnitIndices(long periodId, string typeOf)
        {
            if (typeOf.ToLower() == UnitIndexClassType.ToLower())
                return periodUnitIndexService.GetAllUnitIndices(periodId);
            else
                return periodUnitIndexService.GetAllUnitIndexGrroups(periodId);
        }

        public AbstractUnitIndexInPeriodDTO GetUnitIndex(long periodId, long abstractId, string SelectedColumns = "")
        {
            return periodUnitIndexService.GetAbstarctUnitIndexById(abstractId);
           
        }

        public AbstractUnitIndexInPeriodDTO PostUnitIndex(AbstractUnitIndexInPeriodDTO abstractIndex)
        {
            if (abstractIndex is UnitIndexInPeriodDTO)
                return periodUnitIndexService.AddUnitIndex((UnitIndexInPeriodDTO)abstractIndex);
                return periodUnitIndexService.AddUnitIndexGroup((UnitIndexGroupInPeriodDTO)abstractIndex);
        }


        public AbstractUnitIndexInPeriodDTO PutUnitIndex(AbstractUnitIndexInPeriodDTO abstractIndex)
        {
            if (abstractIndex is UnitIndexInPeriodDTO)
                return periodUnitIndexService.UpdateUnitIndex((UnitIndexInPeriodDTO)abstractIndex);
                return periodUnitIndexService.UpdateUnitIndexGroup((UnitIndexGroupInPeriodDTO)abstractIndex);
        }

        public string DeleteAbstarctUnitIndex(long abstractId)
        {
            return periodUnitIndexService.DeleteAbstractUnitIndex(abstractId);
        }

       
    }
}