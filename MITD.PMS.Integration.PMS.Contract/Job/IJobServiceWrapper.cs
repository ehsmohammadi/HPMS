using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Integration.PMS.Contract
{
    public interface IJobServiceWrapper : IServiceWrapper
    {

        JobDTO GetJob(long id);
        JobDTO GetByTransferId(Guid transferId);
        JobDTO AddJob(JobDTO job);

        //void UpdateJob(Action<JobDTO, Exception> action, JobDTO job);
        //void GetJob(Action<JobDTO, Exception> action, string transferId);
        //void GetAllJobs(Action<PageResultDTO<JobDTOWithActions>, Exception> action, int pageSize, int pageIndex, Dictionary<string, string> sortBy);
        //void GetAllJobs(Action<IList<JobDTO>, Exception> action);
        //void DeleteJob(Action<string, Exception> action, long id);
        //void GetJobCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, long id);

    }
}
