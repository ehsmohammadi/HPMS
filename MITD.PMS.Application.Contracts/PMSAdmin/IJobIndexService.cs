using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.JobIndices;

namespace MITD.PMSAdmin.Application.Contracts
{
    public interface IJobIndexService : IService
    {
        JobIndexCategory AddJobIndexCategory(AbstractJobIndexId parentId, string name, string dictionaryName);
        JobIndex AddJobIndex(AbstractJobIndexId categoryId, string name, string dictionaryName
            , IList<CustomFieldTypeId> customFieldTypeIdList, Guid transferId);

        JobIndexCategory UpdateJobIndexCategory(AbstractJobIndexId jobIndexCatId, AbstractJobIndexId parentId,
                                                string name, string dictionaryName);

        JobIndex UpdateJobIndex(AbstractJobIndexId jobIndexId, AbstractJobIndexId categoryId,
            string name, string dictionaryName, IList<CustomFieldTypeId> customFieldTypeIdList);

        void DeleteAbstractJobIndex(AbstractJobIndexId abstractJobIndexId);
        bool IsValidCustomFieldIdList(AbstractJobIndexId jobIndexId, IList<CustomFieldTypeId> customFieldTypeIds);
        JobIndex GetBy(AbstractJobIndexId jobIndexId);
    }
}
