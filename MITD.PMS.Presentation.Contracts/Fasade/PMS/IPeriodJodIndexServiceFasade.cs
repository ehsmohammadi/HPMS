using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IPeriodJobIndexServiceFacade : IFacadeService
    {
        IEnumerable<AbstractIndexInPeriodDTOWithActions> GetAllAbstractJobIndices(long periodId);
        AbstractIndexInPeriodDTO GetAbstarctJobIndexById(long id);
        AbstractIndexInPeriodDTO AddJobIndex(JobIndexInPeriodDTO abstractIndex);
        AbstractIndexInPeriodDTO AddJobIndexGroup(JobIndexGroupInPeriodDTO abstractIndex);
        AbstractIndexInPeriodDTO UpdateJobIndex(JobIndexInPeriodDTO abstractIndex);
        AbstractIndexInPeriodDTO UpdateJobIndexGroup(JobIndexGroupInPeriodDTO abstractIndex);
        string DeleteAbstractJobIndex(long id);
        IEnumerable<AbstractIndexInPeriodDTO> GetAllJobIndices(long periodId);
        IEnumerable<AbstractIndexInPeriodDTO> GetAllJobIndexGrroups(long periodId);
    }
}
