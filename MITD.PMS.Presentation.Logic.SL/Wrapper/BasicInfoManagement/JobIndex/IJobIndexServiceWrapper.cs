using System.Collections.Generic;
using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;
using System;

namespace MITD.PMS.Presentation.Logic
{
    public partial interface IJobIndexServiceWrapper : IServiceWrapper
    { 
        void GetJobIndex(Action<JobIndexDTO, Exception> action, long id);
        void AddJobIndex(Action<JobIndexDTO, Exception> action, JobIndexDTO jobIndex);
        void UpdateJobIndex(Action<JobIndexDTO, Exception> action, JobIndexDTO jobIndex);
        void DeleteJobIndex(Action<string, Exception> action, long id);
        void GetAllAbstractJobIndex(Action<List<AbstractJobIndexDTOWithActions>, Exception> action);

        //void GetJobIndexCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, long id);
        //void GetJobIndexCustomFields(Action<List<AbstractCustomFieldDTO>, Exception> action, List<long> fieldIdList);
        //void GetJobIndexEntityCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action);
        //void GetCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, long jobIndexId);


        void GetAllJobIndex(Action<List<JobIndexDTO>, Exception> action);
    }
}
