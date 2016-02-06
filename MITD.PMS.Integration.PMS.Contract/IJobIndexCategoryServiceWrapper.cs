using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Integration.PMS.API
{
    public partial interface IJobIndexServiceWrapper : IServiceWrapper
    {
        void GetJobIndexCategory(Action<JobIndexCategoryDTO, Exception> action, long id);
        void AddJobIndexCategory(Action<JobIndexCategoryDTO, Exception> action, JobIndexCategoryDTO job);
        void UpdateJobIndexCategory(Action<JobIndexCategoryDTO, Exception> action, JobIndexCategoryDTO job);
        void GetAllJobIndexCategorys(Action<PageResultDTO<JobIndexCategoryDTOWithActions>, Exception> action, int pageSize, int pageIndex);
        void DeleteJobIndexCategory(Action<string, Exception> action, long id);
    }
}
