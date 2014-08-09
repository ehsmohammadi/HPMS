using System.Collections.Generic;
using MITD.Core;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.Jobs;

namespace MITD.PMSAdmin.Application.Contracts
{
    public interface IJobService:IService
    { 
        Job AddJob(string jobName,string dictionaryName,List<CustomFieldTypeId> customFieldTypeList);
        Job UppdateJob(JobId jobId, string name, string dictionaryName, IList<CustomFieldTypeId> customFieldTypeIdList);
        void DeleteJob(JobId id);
        Job GetBy(JobId jobId);
        bool IsValidCustomFieldIdList(JobId jobId, IList<CustomFieldTypeId> customFieldTypeIds);
    }
}
