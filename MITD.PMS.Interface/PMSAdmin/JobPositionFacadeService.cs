using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.JobPositions;
using MITD.PMSSecurity.Domain;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
    [Interceptor(typeof(Interception))]
    public class JobPositionFacadeService : IJobPositionFacadeService
    {
        #region Fields

        private readonly IMapper<JobPosition, JobPositionDTOWithActions> jobPositionWithActionMapper;
        private readonly IMapper<JobPosition, JobPositionDTO> jobPositionMapper;
        private readonly IJobPositionService jobPositionService;
        private readonly IJobPositionRepository jobPositionRep; 

        #endregion

        #region Ctor
        public JobPositionFacadeService(IMapper<JobPosition, JobPositionDTOWithActions> jobPositionWithActionMapper,
                                        IMapper<JobPosition, JobPositionDTO> jobPositionMapper,
                                        IJobPositionService jobPositionService,
                                        IJobPositionRepository jobPositionRep)
        {
            this.jobPositionWithActionMapper = jobPositionWithActionMapper;
            this.jobPositionMapper = jobPositionMapper;
            this.jobPositionService = jobPositionService;
            this.jobPositionRep = jobPositionRep;
        } 
        #endregion

        #region Public methods
        [RequiredPermission(ActionType.ManageJobPositions)]
        public PageResultDTO<JobPositionDTOWithActions> GetAllJobPositions(int pageSize, int pageIndex, QueryStringConditions queryStringConditions)
        {

            var fs = new ListFetchStrategy<JobPosition>(Enums.FetchInUnitOfWorkOption.NoTracking);
            var sortBy = QueryConditionHelper.GetSortByDictionary(queryStringConditions.SortBy);
            foreach (var item in sortBy)
            {
                fs.SortCriterias.Add(new StringSortCriteria<JobPosition>(item.Key,
                                                                             (item.Value.ToUpper() == "ASC")
                                                                                 ? Enums.SortOrder.Ascending
                                                                               : Enums.SortOrder.Descending));
            }
            fs.OrderBy(x => x.Id);
            fs.WithPaging(pageSize, pageIndex);
            jobPositionRep.FindBy(fs);
            var res = new PageResultDTO<JobPositionDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result = fs.PageCriteria.PageResult.Result.Select(r => jobPositionWithActionMapper.MapToModel(r)).ToList();
            return res;
        }

        [RequiredPermission(ActionType.ManageJobPositions)]
        public List<JobPositionDTO> GetAllJobPositions()
        {
            List<JobPosition> jobPositions = jobPositionRep.GetAll();
            return jobPositions.Select(j => jobPositionMapper.MapToModel(j)).ToList();
        }

        [RequiredPermission(ActionType.AddJobPosition)]
        public JobPositionDTO AddJobPosition(JobPositionDTO dto)
        {
            var res = jobPositionService.AddJobPosition(dto.Name, dto.DictionaryName, dto.TransferId);
            return jobPositionMapper.MapToModel(res);
        }

        [RequiredPermission(ActionType.ModifyJobPosition)]
        public JobPositionDTO UpdateJobPosition(JobPositionDTO dto)
        {
            //var jobPosition = jobPositionMapper.MapToEntity(dto);
            var res = jobPositionService.UppdateJobPosition(new JobPositionId(dto.Id), dto.Name, dto.DictionaryName);
            return jobPositionMapper.MapToModel(res);
        }

        [RequiredPermission(ActionType.DeleteJobPosition)]
        public string DeleteJob(long id)
        {
            jobPositionService.Delete(new JobPositionId(id));
            return "JobPosition With Id " + id + " delted";
        }

        public JobPositionDTO GetJobPositionById(long id)
        {
            var jobPosition = jobPositionRep.GetById(new JobPositionId(id));
            return jobPositionMapper.MapToModel(jobPosition);
        }

        public JobPositionDTO GetJobPositionByTransferId(Guid transferId)
        {
            var jobPosition = jobPositionRep.GetByTransferId(transferId);
            if (jobPosition == null)
                return null;
            return jobPositionMapper.MapToModel(jobPosition);
        }
        #endregion

    }
}
