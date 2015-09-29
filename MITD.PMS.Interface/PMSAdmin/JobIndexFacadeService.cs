using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Contracts.Fasade;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.JobIndices;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
   // //  [Interceptor(typeof(Interception))]
    public class JobIndexFacadeService : IJobIndexFacadeService
    { 
        private readonly IJobIndexService jobIndexService;
        private readonly ICustomFieldRepository customFieldRep;
        private readonly IMapper<AbstractJobIndex, AbstractIndex> jobIndexMapper;
        private readonly IMapper<AbstractJobIndex, AbstractJobIndexDTOWithActions> jobIndexWithActionsMapper;
        private readonly IMapper<CustomFieldType, CustomFieldDTO> customFieldDtoMapper;
        private readonly IJobIndexRepository jobIndexRep;

        public JobIndexFacadeService(
            IJobIndexService jobIndexService,
            ICustomFieldRepository customFieldRep,
            IMapper<AbstractJobIndex, AbstractIndex> jobIndexMapper,
            IMapper<AbstractJobIndex, AbstractJobIndexDTOWithActions> jobIndexWithActionsMapper,
            IMapper<CustomFieldType, CustomFieldDTO> customFieldDtoMapper,
            IJobIndexRepository jobIndexRep
            )
        {
            this.jobIndexService = jobIndexService;
            this.customFieldRep = customFieldRep;
            this.jobIndexMapper = jobIndexMapper;
            this.jobIndexWithActionsMapper = jobIndexWithActionsMapper;
            this.customFieldDtoMapper = customFieldDtoMapper;
            this.jobIndexRep = jobIndexRep;
        }



        public PageResultDTO<AbstractJobIndexDTOWithActions> GetAllJobIndicesWithPagination(int pageSize, int pageIndex, QueryStringConditions queryStringConditions)
        {
            var fs = new ListFetchStrategy<JobIndex>(Enums.FetchInUnitOfWorkOption.NoTracking);
            var sortBy = QueryConditionHelper.GetSortByDictionary(queryStringConditions.SortBy);
            foreach (var item in sortBy)
            {
                fs.SortCriterias.Add(new StringSortCriteria<JobIndex>(item.Key,
                                                                             (item.Value.ToUpper() == "ASC")
                                                                                 ? Enums.SortOrder.Ascending
                                                                               : Enums.SortOrder.Descending));
            }
            fs.OrderBy(x => x.Id);
            fs.WithPaging(pageSize, pageIndex);
            jobIndexRep.GetAllJobIndex(fs);
            var res = new PageResultDTO<AbstractJobIndexDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result = fs.PageCriteria.PageResult.Result.Select(r => jobIndexWithActionsMapper.MapToModel(r));
            return res;
        }

        public PageResultDTO<AbstractJobIndexDTOWithActions> GetAllJobIndexCategoriesWithPagination(int pageSize, int pageIndex, QueryStringConditions queryStringConditions)
        {
            var fs = new ListFetchStrategy<JobIndexCategory>(Enums.FetchInUnitOfWorkOption.NoTracking);
            var sortBy = QueryConditionHelper.GetSortByDictionary(queryStringConditions.SortBy);
            foreach (var item in sortBy)
            {
                fs.SortCriterias.Add(new StringSortCriteria<JobIndexCategory>(item.Key,
                                                                             (item.Value.ToUpper() == "ASC")
                                                                                 ? Enums.SortOrder.Ascending
                                                                               : Enums.SortOrder.Descending));
            }
            fs.OrderBy(x => x.Id);
            fs.WithPaging(pageSize, pageIndex);
            jobIndexRep.GetAllJobIndexCategory(fs);
            var res = new PageResultDTO<AbstractJobIndexDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result = fs.PageCriteria.PageResult.Result.Select(r => jobIndexWithActionsMapper.MapToModel(r));
            return res;
        }

        public IEnumerable<AbstractJobIndexDTOWithActions> GetAllAbstractJobIndices()
        {
            var abstractList = jobIndexRep.GetAll();
            return abstractList.Select(r => jobIndexWithActionsMapper.MapToModel(r));
        }

        public AbstractIndex GetAbstarctJobIndexById(long id)
        {
            var jobIndex = jobIndexRep.GetById(new AbstractJobIndexId(id));
            var abstractIndexDto = jobIndexMapper.MapToModel(jobIndex);
            if (abstractIndexDto is JobIndexDTO)
            {
                var customFieldList = customFieldRep.GetAllCustomField(new AbstractJobIndexId(id));
                ((JobIndexDTO)abstractIndexDto).CustomFields = customFieldList.Select(c => customFieldDtoMapper.MapToModel(c)).ToList();
            }

            return abstractIndexDto;
        }

       

        public AbstractIndex AddJobIndex(JobIndexDTO jobIndexDto)
        {
            var jobIndex = jobIndexService.AddJobIndex(new AbstractJobIndexId(jobIndexDto.ParentId.Value),
                                                jobIndexDto.Name, jobIndexDto.DictionaryName
                                                ,jobIndexDto.CustomFields.Select(c => new CustomFieldTypeId(c.Id)).ToList());
            return jobIndexMapper.MapToModel(jobIndex);
        }

        public AbstractIndex AddJobIndexCategory(JobIndexCategoryDTO jobIndexCategoryDto)
        {
            var jobIndexCat = jobIndexService.AddJobIndexCategory(
                (jobIndexCategoryDto.ParentId == null) ? null:new AbstractJobIndexId(jobIndexCategoryDto.ParentId.Value),
                                                jobIndexCategoryDto.Name, jobIndexCategoryDto.DictionaryName);
            return jobIndexMapper.MapToModel(jobIndexCat);
        }


        public AbstractIndex UpdateJobIndex(JobIndexDTO jobIndexDto)
        {
            var jobIndex = jobIndexService.UpdateJobIndex(new AbstractJobIndexId(jobIndexDto.Id)
                , new AbstractJobIndexId(jobIndexDto.ParentId.Value), jobIndexDto.Name, jobIndexDto.DictionaryName
                ,jobIndexDto.CustomFields.Select(c => new CustomFieldTypeId(c.Id)).ToList()
                );
            return jobIndexMapper.MapToModel(jobIndex);
        }

        public AbstractIndex UpdateJobIndexCategory(JobIndexCategoryDTO jobIndexCatDto)
        {
            var jobIndexCat = jobIndexService.UpdateJobIndexCategory(new AbstractJobIndexId(jobIndexCatDto.Id)
                , (jobIndexCatDto.ParentId.HasValue) ? new AbstractJobIndexId(jobIndexCatDto.ParentId.Value) : null
                , jobIndexCatDto.Name, jobIndexCatDto.DictionaryName);
            return jobIndexMapper.MapToModel(jobIndexCat);
        }

        public string DeleteAbstractJobIndex(long id)
        {
            jobIndexService.DeleteAbstractJobIndex(new AbstractJobIndexId(id));
            return "JobIndex deleted successfully";
        }

        public IList<AbstractIndex> GetAllJobIndices()
        {
            var jobIndexList =  jobIndexRep.GetAllJobIndex();
            return jobIndexList.Select(j => jobIndexMapper.MapToModel(j)).ToList();
        }

        public IList<AbstractIndex> GetAllJobIndexCategories()
        {
            var jobIndexList = jobIndexRep.GetAllJobIndexCategory();
            return jobIndexList.Select(j => jobIndexMapper.MapToModel(j)).ToList();
        }
    }
}
