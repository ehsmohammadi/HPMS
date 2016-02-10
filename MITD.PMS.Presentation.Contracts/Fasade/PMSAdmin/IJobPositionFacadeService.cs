using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IJobPositionFacadeService:IFacadeService
    {       
        PageResultDTO<JobPositionDTOWithActions> GetAllJobPositions(int pageSize, int pageIndex, QueryStringConditions queryStringConditions);
        JobPositionDTO AddJobPosition(JobPositionDTO customField);
        JobPositionDTO UpdateJobPosition(JobPositionDTO customField);
        JobPositionDTO GetJobPositionById(long id);
        string DeleteJob(long id);
        List<JobPositionDTO> GetAllJobPositions();
       
    }
}
