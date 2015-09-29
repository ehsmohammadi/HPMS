using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Contracts.Fasade;
using MITD.PMS.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMS.Domain.Model.JobIndices;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
    [Interceptor(typeof(Interception))]
    public class PeriodJobIndexFacadeService : IPeriodJobIndexServiceFacade
    { 
        private readonly IJobIndexService jobIndexService;
        private readonly IMapper<AbstractJobIndex, AbstractIndexInPeriodDTO> jobIndexMapper;
        private readonly IMapper<AbstractJobIndex, AbstractIndexInPeriodDTOWithActions> jobIndexWithActionsMapper;
        private readonly IMapper<SharedJobIndexCustomField, AbstractCustomFieldDescriptionDTO> abstractCustomFieldDtoMapper;
        private readonly IJobIndexRepository jobIndexRep;

        public PeriodJobIndexFacadeService(
            IJobIndexService jobIndexService,
            IMapper<AbstractJobIndex, AbstractIndexInPeriodDTO> jobIndexMapper,
            IMapper<AbstractJobIndex, AbstractIndexInPeriodDTOWithActions> jobIndexWithActionsMapper,
            IMapper<SharedJobIndexCustomField, AbstractCustomFieldDescriptionDTO> abstractCustomFieldDtoMapper,
            IJobIndexRepository jobIndexRep
            )
        {
            this.jobIndexService = jobIndexService;
            this.jobIndexMapper = jobIndexMapper;
            this.jobIndexWithActionsMapper = jobIndexWithActionsMapper;
            this.abstractCustomFieldDtoMapper = abstractCustomFieldDtoMapper;
            this.jobIndexRep = jobIndexRep;
        }


        public IEnumerable<AbstractIndexInPeriodDTOWithActions> GetAllAbstractJobIndices(long periodId)
        {
            var abstractList = jobIndexRep.GetAll(new PeriodId(periodId));
                return abstractList.Select(r => jobIndexWithActionsMapper.MapToModel(r));
        }

        public AbstractIndexInPeriodDTO GetAbstarctJobIndexById(long id)
        {
            var abstractJobIndex = jobIndexRep.GetById(new AbstractJobIndexId(id));
            var abstractIndexDto = jobIndexMapper.MapToModel(abstractJobIndex);

           
            if (abstractIndexDto is JobIndexInPeriodDTO)
            {
                var jobIndex = (JobIndex)abstractJobIndex;
                var sharedJobIndexCustomFlds = jobIndexService.GetSharedJobIndexCustomField(jobIndex.SharedJobIndexId,
                                                                     jobIndex.CustomFieldValues.Keys.ToList());
                foreach (var customFld in sharedJobIndexCustomFlds)
                {
                    var abstractDescCustomFld = abstractCustomFieldDtoMapper.MapToModel(customFld);
                    abstractDescCustomFld.Value = jobIndex.CustomFieldValues[customFld.Id];
                    ((JobIndexInPeriodDTO)abstractIndexDto).CustomFields.Add(abstractDescCustomFld); 
                }

            }

            return abstractIndexDto;
        }

        public AbstractIndexInPeriodDTO AddJobIndex(JobIndexInPeriodDTO abstractIndex)
        {
            //var jobIndex = jobIndexService.AddJobIndex(new PeriodId(abstractIndex.PeriodId), new AbstractJobIndexId(abstractIndex.ParentId.Value),
            //                                    new SharedJobIndexId(abstractIndex.JobIndexId)
            //                                    , abstractIndex.CustomFields.ToDictionary(itm => new SharedJobIndexCustomFieldId(itm.Key.Id), itm => itm.Value)
            //                                    , abstractIndex.IsInquireable);

            var jobIndex = jobIndexService.AddJobIndex(new PeriodId(abstractIndex.PeriodId), new AbstractJobIndexId(abstractIndex.ParentId.Value),
                                                new SharedJobIndexId(abstractIndex.JobIndexId)
                                                , abstractIndex.CustomFields.ToDictionary(itm => new SharedJobIndexCustomFieldId(itm.Id), itm => itm.Value)
                                                , abstractIndex.IsInquireable, abstractIndex.CalculationOrder, abstractIndex.CalculationLevel);

            return jobIndexMapper.MapToModel(jobIndex);

        }

        public AbstractIndexInPeriodDTO AddJobIndexGroup(JobIndexGroupInPeriodDTO abstractIndex)
        {
            var jobIndexGroup = jobIndexService.AddJobIndexGroup(new PeriodId(abstractIndex.PeriodId), 
                (abstractIndex.ParentId == null) ? null : new AbstractJobIndexId(abstractIndex.ParentId.Value),
                                                abstractIndex.Name, abstractIndex.DictionaryName);
            return jobIndexMapper.MapToModel(jobIndexGroup);
        }

        public AbstractIndexInPeriodDTO UpdateJobIndex(JobIndexInPeriodDTO abstractIndex)
        {
            var jobIndex = jobIndexService.UpdateJobIndex(new AbstractJobIndexId(abstractIndex.Id)
                , new AbstractJobIndexId(abstractIndex.ParentId.Value)
                , abstractIndex.CustomFields.ToDictionary(itm=>new SharedJobIndexCustomFieldId(itm.Id),itm=>itm.Value),
                abstractIndex.IsInquireable, abstractIndex.CalculationOrder, abstractIndex.CalculationLevel
                );
            return jobIndexMapper.MapToModel(jobIndex);
           
        }

        public AbstractIndexInPeriodDTO UpdateJobIndexGroup(JobIndexGroupInPeriodDTO abstractIndex)
        {
            var jobIndexGroup = jobIndexService.UpdateJobIndexGroup(new AbstractJobIndexId(abstractIndex.Id)
               , (abstractIndex.ParentId.HasValue) ? new AbstractJobIndexId(abstractIndex.ParentId.Value) : null
               , abstractIndex.Name, abstractIndex.DictionaryName);
            return jobIndexMapper.MapToModel(jobIndexGroup);
        }

        public string  DeleteAbstractJobIndex(long id)
        {
            jobIndexService.DeleteAbstractJobIndex(new AbstractJobIndexId(id));
                return "JobIndex deleted successfully";
        }

        public IEnumerable<AbstractIndexInPeriodDTO> GetAllJobIndices(long periodId)
        {
            var abstractList = jobIndexRep.GetAllJobIndex(new PeriodId(periodId));
            return abstractList.Select(r => jobIndexMapper.MapToModel(r));
        }

        public IEnumerable<AbstractIndexInPeriodDTO> GetAllJobIndexGrroups(long periodId)
        {
            var abstractList = jobIndexRep.GetAllJobIndexGroup(new PeriodId(periodId));
            return abstractList.Select(r => jobIndexMapper.MapToModel(r));
        }
    }
}
