using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public interface IJobServiceWrapper
    {

        void GetJob(Action<JobDTO, Exception> action, long id);
        void GetJob(Action<JobDTO, Exception> action, string transferId);
        void AddJob(Action<JobDTO, Exception> action, JobDTO job);
        void UpdateJob(Action<JobDTO, Exception> action, JobDTO job);
        void GetAllJobs(Action<PageResultDTO<JobDTOWithActions>, Exception> action, int pageSize, int pageIndex, Dictionary<string, string> sortBy);
        void GetAllJobs(Action<IList<JobDTO>, Exception> action);
        void DeleteJob(Action<string, Exception> action, long id);
        void GetJobCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, long id);

    }
}
