using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class JobInPeriodMapper : BaseFilterMapper<Job,JobInPeriodDTO> , IFilterMapper<Job,JobInPeriodDTO>
    {
        protected override JobInPeriodDTO MapToModel(Job entity)
        {
            var res = new JobInPeriodDTO()
            {
                JobId = entity.Id.SharedJobId.Id,
                Name = entity.Name,
                DictionaryName = entity.DictionaryName,
            };
            return res;
        }
    }


    public class JobInPeriodWithActionsMapper : BaseFilterMapper<Job, JobInPeriodDTOWithActions>, IFilterMapper<Job, JobInPeriodDTOWithActions>
    {
        protected override JobInPeriodDTOWithActions MapToModel(Job entity)
        {
            var res = new JobInPeriodDTOWithActions()
            {
                JobId = entity.Id.SharedJobId.Id,
                Name = entity.Name,
                DictionaryName = entity.DictionaryName,
                ActionCodes =new List<int> {(int)ActionType.AddJobInPeriod,(int)ActionType.ModifyJobInPeriod,(int)ActionType.DeleteJobInPeriod}
            };
            return res;
        }
    }
}
