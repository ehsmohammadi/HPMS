using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Model;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
    //  [Interceptor(typeof(Interception))]
    public class PeriodJobServiceFacade : IPeriodJobServiceFacade
    {
        private readonly IJobService jobService;
        private readonly IJobIndexService jobIndexService;
        private readonly IFilterMapper<Job, JobInPeriodDTOWithActions> jobInPeriodDTOWithActionsMapper;
        private readonly IFilterMapper<Job, JobInPeriodDTO> jobInPeriodDTOMapper;
        private readonly IMapper<JobCustomField, CustomFieldDTO> jobCustomFieldMapper;
        private readonly IMapper<AbstractJobIndex, AbstractIndexInPeriodDTO> jobIndexInPeriodMapper;
        private readonly IJobRepository jobRep;
        private readonly IJobPositionRepository jobPositionRep;

        public PeriodJobServiceFacade(IJobService jobService,
            IJobIndexService jobIndexService,
            IFilterMapper<Job,JobInPeriodDTOWithActions> jobInPeriodDTOWithActionsMapper,
            IFilterMapper<Job, JobInPeriodDTO> jobInPeriodDTOMapper,
            IMapper<JobCustomField,CustomFieldDTO> jobCustomFieldMapper,
            IMapper<AbstractJobIndex, AbstractIndexInPeriodDTO> jobIndexInPeriodMapper,
            IJobRepository jobRep ,
            IJobPositionRepository jobPositionRep
            )
        {
            this.jobService = jobService;
            this.jobIndexService = jobIndexService;
            this.jobInPeriodDTOWithActionsMapper = jobInPeriodDTOWithActionsMapper;
            this.jobInPeriodDTOMapper = jobInPeriodDTOMapper;
            this.jobCustomFieldMapper = jobCustomFieldMapper;
            this.jobIndexInPeriodMapper = jobIndexInPeriodMapper;
            this.jobRep = jobRep;
            this.jobPositionRep = jobPositionRep;
        }


        [RequiredPermission(ActionType.ShowJobInPeriod)]
        public PageResultDTO<JobInPeriodDTOWithActions> GetAllJobs(long periodId,int pageSize, int pageIndex, QueryStringConditions queryStringConditions, string selectedColumns)
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
            jobRep.Find(e => e.Id.PeriodId == new PeriodId(periodId), fs);
            var res = new PageResultDTO<JobInPeriodDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result = fs.PageCriteria.PageResult.Result.Select(r => jobInPeriodDTOWithActionsMapper.MapToModel(r, selectedColumns.Split(','))).ToList();
            return res;
        }

        [RequiredPermission(ActionType.AddJobInPeriod)]
        public JobInPeriodDTO AssignJob(long periodId, JobInPeriodDTO jobInPeriod)
        {
            //Job job= jobService.AssignJob(new JobId(new PeriodId(periodId), new SharedJobId(jobInPeriod.JobId)));
            //return jobInPeriodDTOMapper.MapToModel(job,new string[]{});

            Job job = jobService.AssignJob(new JobId(new PeriodId(periodId), new SharedJobId(jobInPeriod.JobId)),
                jobInPeriod.CustomFields.Select(c=> new SharedJobCustomFieldId(c.Id)).ToList(),
                jobInPeriod.JobIndices.Select(c => new JobIndexForJob(new AbstractJobIndexId(c.Id),c.ShowforTopLevel,c.ShowforSameLevel,c.ShowforLowLevel)).ToList()
                );
            return jobInPeriodDTOMapper.MapToModel(job,new string[]{});
        }

        [RequiredPermission(ActionType.DeleteJobInPeriod)]
        public void RemoveJob(long periodId, long jobId)
        {
            jobService.RemoveJob(new JobId(new PeriodId(periodId), new SharedJobId(jobId)));
        }

        [RequiredPermission(ActionType.ShowJobInPeriod)]
        public JobInPeriodDTOWithActions GetJobWithActions(long periodId, long jobId, string selectedColumns)
        {
            Job job = jobRep.GetById(new JobId(new PeriodId(periodId), new SharedJobId(jobId)));
            //var customFieldList = customFieldRep.GetAllCustomField(new JobId(id));
            var jobDto = jobInPeriodDTOWithActionsMapper.MapToModel(job,selectedColumns.Split(','));
            //jobDto.CustomFields = customFieldList.Select(c => abstractcustomFieldDtoMapper.MapToModel(c)).ToList();
            return jobDto;
        }

        [RequiredPermission(ActionType.ShowJobInPeriod)]
        public JobInPeriodDTO GetJob(long periodId, long jobId, string selectedColumns)
        {
            var job = jobRep.GetById(new JobId(new PeriodId(periodId), new SharedJobId(jobId)));
            var jobDto = jobInPeriodDTOMapper.MapToModel(job, selectedColumns.Split(','));
            jobDto.CustomFields = job.CustomFields.Select(c => jobCustomFieldMapper.MapToModel(c)).ToList();
            var jobindexIdList = job.JobIndexList.Select(j => j.JobIndexId).ToList();
            var jobIndices = jobIndexService.FindJobIndices(jobindexIdList);
            //todo:(LOW) change this mapping to valid mapping need som work !!!!!!
            var jobInPeriodJobIndexDTOList = new List<JobInPeriodJobIndexDTO>();
            foreach (var jobIndex in jobIndices)
            {
                var jobJobIndex = job.JobIndexList.Single(j => j.JobIndexId == jobIndex.Id);
                jobInPeriodJobIndexDTOList.Add(new JobInPeriodJobIndexDTO
                {
                    Id = jobIndex.Id.Id,
                    IsInquireable = jobIndex.IsInquireable,
                    Name = jobIndex.Name,
                    ShowforTopLevel = jobJobIndex.ShowforTopLevel,
                    ShowforSameLevel = jobJobIndex.ShowforSameLevel,
                    ShowforLowLevel = jobJobIndex.ShowforLowLevel
                });
            }
            jobDto.JobIndices = jobInPeriodJobIndexDTOList;
            return jobDto;
        }

        [RequiredPermission(ActionType.ShowJobInPeriod)]
        public List<JobInPeriodDTOWithActions> GetAllJobWithActions(long periodId, string selectedColumns)
        {
            var res = jobRep.GetAllJob(new PeriodId(periodId));
            return res.Select(r => jobInPeriodDTOWithActionsMapper.MapToModel(r, selectedColumns.Split(','))).ToList();
        }

        [RequiredPermission(ActionType.ShowJobInPeriod)]
        public List<JobInPeriodDTO> GetAllJob(long periodId, string selectedColumns)
        {
            var res = jobRep.GetAllJob(new PeriodId(periodId));
            return res.Select(r => jobInPeriodDTOMapper.MapToModel(r, selectedColumns.Split(','))).ToList();
        }

        [RequiredPermission(ActionType.ModifyJobInPeriod)]
        public JobInPeriodDTO UpdateJob(long periodId, JobInPeriodDTO jobInPeriod)
        {
            
            var job = jobService.UpdateJob(new JobId(new PeriodId(periodId),new SharedJobId(jobInPeriod.JobId)),
                jobInPeriod.CustomFields.Select(c=>new SharedJobCustomFieldId(c.Id)).ToList(),
                jobInPeriod.JobIndices.Select(c => new JobIndexForJob(new AbstractJobIndexId(c.Id), c.ShowforTopLevel, c.ShowforSameLevel, c.ShowforLowLevel)).ToList()
                );
            return jobInPeriodDTOMapper.MapToModel(job, new string[] {});
        }

        public JobInPeriodDTO GetJobByJobPositionId(long periodId, long jobPositionId)
        {
            var jobPosition =
                jobPositionRep.GetBy(new JobPositionId(new PeriodId(periodId), new SharedJobPositionId(jobPositionId)));
            var jobId = jobPosition.JobId;
            return GetJob(periodId, jobId.SharedJobId.Id, "");

        }
    }
}
