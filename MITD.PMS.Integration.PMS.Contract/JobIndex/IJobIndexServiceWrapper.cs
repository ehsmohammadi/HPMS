using System;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Integration.PMS.Contract
{
    public partial interface IJobIndexServiceWrapper : IServiceWrapper
    {
        JobIndexDTO GetJobIndex(long id);
        JobIndexDTO AddJobIndex(JobIndexDTO jobIndex);

        #region Not Used
        //void UpdateJobIndex(Action<JobIndexDTO, Exception> action, JobIndexDTO jobIndex);
        //void DeleteJobIndex(Action<string, Exception> action, long id);
        //void GetAllAbstractJobIndex(Action<List<AbstractJobIndexDTOWithActions>, Exception> action);

        //void GetJobIndexCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, long id);
        //void GetJobIndexCustomFields(Action<List<AbstractCustomFieldDTO>, Exception> action, List<long> fieldIdList);
        //void GetJobIndexEntityCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action);
        //void GetCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, long jobIndexId);
        //void GetAllJobIndex(Action<List<JobIndexDTO>, Exception> action); 
        #endregion
    }
}
