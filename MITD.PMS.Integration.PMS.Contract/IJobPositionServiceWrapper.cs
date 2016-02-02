using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Integration.PMS.API
{
    public interface IJobPositionServiceWrapper : IServiceWrapper
    {

        void GetJobPosition(Action<JobPositionDTO, Exception> action, long id);
        void AddJobPosition(Action<JobPositionDTO, Exception> action, JobPositionDTO jobPosition);
        void UpdateJobPosition(Action<JobPositionDTO, Exception> action, JobPositionDTO jobPosition);
        void GetAllJobPositions(Action<PageResultDTO<JobPositionDTOWithActions>, Exception> action, int pageSize, int pagePost);
        void DeleteJobPosition(Action<string, Exception> action, long id);
        void GetAllJobPositions(Action<List<JobPositionDTO>, Exception> action);

    }
}
