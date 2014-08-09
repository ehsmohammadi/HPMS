using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IJobInPeriodServiceWrapper:IServiceWrapper
    {
        //void GetJobInPeriod(Action<JobInPeriodAssignment, Exception> action, long id);
        //void AddJobInPeriod(Action<JobInPeriodAssignment, Exception> action, JobInPeriodAssignment jobInPeriod);
        //void UpdateJobInPeriod(Action<JobInPeriodAssignment, Exception> action, JobInPeriodAssignment jobInPeriod);
        
        //void GetAllJob(Action<ObservableCollection<JobDescription>, Exception> action);
        //void GetJobInPrdField(Action<JobInPrdField, Exception> action, long jobInPrdFieldId);
        ////void GetAllJobInPeriods(Action<ObservableCollection<JobInPeriod>, Exception> action);
        //void AddJobInPrdField(Action<JobInPrdField, Exception> action, JobInPrdField jobInPrdField);
        //void UpdateJobInPrdField(Action<JobInPrdField, Exception> action, JobInPrdField jobInPrdField);
        //void GetJobInPrdFields(Action<ObservableCollection<JobInPrdField>, Exception> action,long jobInPrdId);
        //void DeleteJobInPeriod(Action<bool, Exception> action, long id);

        void GetAllJobInPeriodWithPagination(Action<PageResultDTO<JobInPeriodDTOWithActions>, Exception> action, long periodId, int pageSize, int pageIndex,string selectedColumns);
        void GetAllJobInPeriodWithPagination(Action<PageResultDTO<JobInPeriodDTOWithActions>, Exception> action, long periodId, int pageSize, int pageIndex);
        void GetAllJobInPeriod(Action<IList<JobInPeriodDTO>, Exception> action, long periodId);
        void GetAllJobInPeriod(Action<IList<JobInPeriodDTO>, Exception> action, long periodId, string selectedColumns);
        void GetJobInPeriod(Action<JobInPeriodDTO, Exception> action, long periodId, long jobId);
        void GetJobInPeriod(Action<JobInPeriodDTO, Exception> action, long periodId, long jobId, string selectedColumns);
        void AddJobInPeriod(Action<JobInPeriodDTO, Exception> action, long periodId, JobInPeriodDTO jobInPeriod);
        void UpdateJobInPeriod(Action<JobInPeriodDTO, Exception> action, long periodId, JobInPeriodDTO jobInPeriod);
        void DeleteJobInPeriod(Action<string, Exception> action, long periodId, long jobId);
    }
}
