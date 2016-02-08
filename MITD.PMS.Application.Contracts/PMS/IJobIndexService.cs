using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MITD.Core;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Application.Contracts
{
    public interface IJobIndexService : IService
    {
        JobIndexGroup AddJobIndexGroup(PeriodId periodId, AbstractJobIndexId parentId, string name, string dictionaryName);
        JobIndex AddJobIndex(PeriodId periodId, AbstractJobIndexId groupId, SharedJobIndexId jabIndexId, IDictionary<SharedJobIndexCustomFieldId, string> customFieldValues, bool isInquireable, int sortOrder, long calculationLevel);

        JobIndexGroup UpdateJobIndexGroup(AbstractJobIndexId jobIndexGroupId, AbstractJobIndexId parentId,
                                                string name, string dictionaryName);

        JobIndex UpdateJobIndex(AbstractJobIndexId jobIndexId, AbstractJobIndexId groupId, IDictionary<SharedJobIndexCustomFieldId, string> customFieldValues, bool isInquireable, int calculationOrder, long calculationLevel);

        void DeleteAbstractJobIndex(AbstractJobIndexId abstractJobIndexId);

        IList<SharedJobIndexCustomField> GetSharedJobIndexCustomField(SharedJobIndexId sharedJobIndexId, List<SharedJobIndexCustomFieldId> fieldIdList);

        IEnumerable<JobIndex> FindJobIndices(IEnumerable<AbstractJobIndexId> jobIndexIds);
        //bool IsValidCustomFieldIdList(AbstractJobIndexId jobIndexId, IList<SharedJobIndexCustomFieldId> customFieldTypeIds);
        //bool IsValidCustomFieldIdList(SharedJobIndexId jobIndexId, IList<SharedJobIndexCustomFieldId> customFieldTypeIds);
        List<AbstractJobIndex> GetAllAbstractJobIndexByParentId(AbstractJobIndexId id);
        List<JobIndexGroup> GetAllParentJobIndexGroup(Period period);
        SharedJobIndexId GetSharedJobIndexIdBy(AbstractJobIndexId abstractJobIndexId);
        AbstractJobIndexId GetJobIndexIdBy(Period jobIndexIds, SharedJobIndexId sharedJobIndexId);
        AbstractJobIndex GetJobIndexById(AbstractJobIndexId jobIndexId);

    }
}
