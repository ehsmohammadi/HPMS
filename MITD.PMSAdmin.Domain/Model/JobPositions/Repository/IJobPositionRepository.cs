using System;
using System.Collections.Generic;
using MITD.Domain.Repository;

namespace MITD.PMSAdmin.Domain.Model.JobPositions
{
    public interface  IJobPositionRepository:IRepository
    { 
        void FindBy(ListFetchStrategy<JobPosition> fs);
        void Add(JobPosition jobPosition);
        void UpdateJobPosition(JobPosition jobPosition);
        JobPosition GetById(JobPositionId jobPositionId);
        JobPosition GetByTransferId(Guid transferId);
        JobPositionId GetNextId();
        void Delete(JobPosition jobPostion);
        List<JobPosition> GetAll();

        JobPositionException ConvertException(Exception exp);
        JobPositionException TryConvertException(Exception exp);
        
    }
}
