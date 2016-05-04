using System;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Integration.PMS.Contract
{
    public interface IJobPositionServiceWrapper : IServiceWrapper
    {

        JobPositionDTO GetJobPosition(long id);
        JobPositionDTO GetByTransferId(Guid transferId);
        JobPositionDTO AddJobPosition(JobPositionDTO jobPosition);

        //void UpdateJobPosition(Action<JobPositionDTO, Exception> action, JobPositionDTO jobPosition);
        //void GetAllJobPositions(Action<PageResultDTO<JobPositionDTOWithActions>, Exception> action, int pageSize, int pagePost);
        //void DeleteJobPosition(Action<string, Exception> action, long id);
        //void GetAllJobPositions(Action<List<JobPositionDTO>, Exception> action);

        
    }
}
