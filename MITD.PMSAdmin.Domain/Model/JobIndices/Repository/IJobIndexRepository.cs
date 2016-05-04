using System;
using System.Collections.Generic;
using MITD.Domain.Repository;

namespace MITD.PMSAdmin.Domain.Model.JobIndices
{
    public interface IJobIndexRepository : IRepository
    {
        IList<JobIndex> GetAllJobIndex(ListFetchStrategy<JobIndex> fs);
        IList<JobIndexCategory> GetAllJobIndexCategory(ListFetchStrategy<JobIndexCategory> fs);
        AbstractJobIndexId GetNextId();
        void Add(AbstractJobIndex jobIndex);
        void Update(AbstractJobIndex jobIndexy);
        IList<AbstractJobIndex> GetAll();
        AbstractJobIndex GetById(AbstractJobIndexId jobIndexId);
        AbstractJobIndex GetByTransferId(Guid transferId);
        void Delete(AbstractJobIndexId jobIndexId);
        JobIndexCategory GetJobIndexCategory(AbstractJobIndexId parentId);
        JobIndex GetJobIndex(AbstractJobIndexId jobIndexId);

        IList<JobIndex> GetAllJobIndex();
        IList<JobIndexCategory> GetAllJobIndexCategory();

        JobIndexException ConvertException(Exception exp);
        JobIndexException TryConvertException(Exception exp);
        
    }
}
