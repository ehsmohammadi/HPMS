using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Contracts.Fasade;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.Jobs;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Model;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{

    //  [Interceptor(typeof(Interception))]

    public class JobFacadeService : IJobFacadeService
    {
        private readonly IJobRepository jobRep;
        private readonly ICustomFieldRepository customFieldRep;
        private readonly IMapper<Job, JobDTO> jobMapper;
        private readonly IMapper<Job, JobDTOWithActions> jobWithActionsMapper;
        private readonly IMapper<CustomFieldType, CustomFieldDTO> ctcustomFieldDtoMapper;
        private readonly IJobService jobService;

        public JobFacadeService(IJobRepository jobRep, ICustomFieldRepository customFieldRep, IMapper<Job, JobDTO> jobMapper,
            IMapper<Job, JobDTOWithActions> jobWithActionsMapper,
             IMapper<CustomFieldType, CustomFieldDTO> customFieldDtoMapper,
            IJobService jobService)
        {
            this.jobRep = jobRep;
            this.customFieldRep = customFieldRep;
            this.jobMapper = jobMapper;
            this.jobWithActionsMapper = jobWithActionsMapper;
            this.ctcustomFieldDtoMapper = customFieldDtoMapper;
            this.jobService = jobService;
        }

        public PageResultDTO<JobDTOWithActions> GetAllJobs(int pageSize, int pageIndex, QueryStringConditions queryStringConditions)
        {
            var fs = new ListFetchStrategy<Job>(Enums.FetchInUnitOfWorkOption.NoTracking);
            var sortBy = QueryConditionHelper.GetSortByDictionary(queryStringConditions.SortBy);
            foreach (var item in sortBy)
            {
                fs.SortCriterias.Add(new StringSortCriteria<Job>(item.Key,
                                                                             (item.Value.ToUpper() == "ASC")
                                                                                 ? Enums.SortOrder.Ascending
                                                                               : Enums.SortOrder.Descending));
            }
            fs.OrderBy(x => x.Id);
            fs.WithPaging(pageSize, pageIndex);
            jobRep.GetAllJob(fs);
            var res = new PageResultDTO<JobDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result = fs.PageCriteria.PageResult.Result.Select(r => jobWithActionsMapper.MapToModel(r)).ToList();
            return res;
        }

        public IList<JobDTO> GetAllJobs()
        {
            var res = jobRep.GetAllJob();
            return res.Select(r => jobMapper.MapToModel(r)).ToList();
        }

        [RequiredPermission(ActionType.AddJob)]
        public JobDTO AddJob(JobDTO jobDto)
        {
            var res = jobService.AddJob(jobDto.Name, jobDto.DictionaryName, jobDto.CustomFields.Select(c => new CustomFieldTypeId(c.Id)).ToList());
            return jobMapper.MapToModel(res);
        }

        [RequiredPermission(ActionType.ModifyJob)]
        [RequiredPermission(ActionType.ManageJobCustomFields)]
        public JobDTO UpdateJob(JobDTO jobDto)
        {
            var job = jobMapper.MapToEntity(jobDto);
            var res = jobService.UppdateJob(job.Id, job.Name, job.DictionaryName, jobDto.CustomFields.Select(c => new CustomFieldTypeId(c.Id)).ToList());
            return jobMapper.MapToModel(res);
        }

        [RequiredPermission(ActionType.DeleteJob)]
        public string DeleteJob(long id)
        {
            jobService.DeleteJob(new JobId(id));
            return "Job with Id:" + id + " deleted";
        }

        public JobDTO GetJobById(long id)
        {
            Job job = jobRep.GetById(new JobId(id));
            var customFieldList = customFieldRep.GetAllCustomField(new JobId(id));
            var jobDto = jobMapper.MapToModel(job);
            jobDto.CustomFields = customFieldList.Select(c => ctcustomFieldDtoMapper.MapToModel(c)).ToList();
            return jobDto;
        }
    }
}
