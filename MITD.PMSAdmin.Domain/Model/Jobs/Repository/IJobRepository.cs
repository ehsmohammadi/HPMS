using System;
using System.Collections.Generic;
using MITD.Domain.Repository;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.Jobs
{
    public interface  IJobRepository:IRepository
    { 
        void GetAllJob(ListFetchStrategy<Job> fs);
        IList<Job> GetAllJob();   
        void AddJob(Job job);
        void UpdateJob(Job job);
        Job GetById(JobId jobId);
        Job GetByTransferId(Guid transferId);
        void DeleteJob(Job job);
        JobId GetNextId ();

        JobException ConvertException(Exception exp);
        JobException TryConvertException(Exception exp);
        
    }
}
