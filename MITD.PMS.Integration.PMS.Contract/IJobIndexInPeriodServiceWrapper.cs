using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public partial interface IJobIndexInPeriodServiceWrapper
    {
        void GetJobIndexInPeriod(Action<JobIndexInPeriodDTO, Exception> action, long periodId, long abstractId);
        void AddJobIndexInPeriod(Action<JobIndexInPeriodDTO, Exception> action, JobIndexInPeriodDTO jobIndexInPeriod);
        void UpdateJobIndexInPeriod(Action<JobIndexInPeriodDTO, Exception> action, JobIndexInPeriodDTO jobIndexInPeriod);
        void DeleteJobIndexInPeriod(Action<string, Exception> action, long periodId, long abstractId);
        //void GetAllJobIndexInPeriods(Action<List<JobIndexInPeriodDTODescription>,Exception> action,long periodId);
        // void GetPeriodJobIndexs(Action<PeriodJobIndexesDTO, Exception> action,long periodId);
        void GetPeriodAbstractIndexes(Action<List<AbstractIndexInPeriodDTOWithActions>, Exception> action, long periodId);
        void GetAllPeriodJobIndexes(Action<List<JobIndexInPeriodDTO>, Exception> action, long periodId);
        //void GetJobIndexInPeriodCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, long jobIndexInPeriodId);

        void AddJobIndexGroupInPeriod(Action<JobIndexGroupInPeriodDTO, Exception> action, JobIndexGroupInPeriodDTO jobIndexGroupInPeriod);
        void UpdateJobIndexGroupInPeriod(Action<JobIndexGroupInPeriodDTO, Exception> action, JobIndexGroupInPeriodDTO jobIndexGroupInPeriod);
        void GetJobIndexGroupInPeriod(Action<JobIndexGroupInPeriodDTO, Exception> action, long periodId, long abstractId);
        void DeleteJobIndexGroupInPeriod(Action<string, Exception> action, long periodId, long abstractId);

        void GetPeriodJobIndexes(Action<List<JobIndexGroupInPeriodDTO>, Exception> action, long periodId);
    }
}
