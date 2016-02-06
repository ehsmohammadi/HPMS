using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class JobIndexConverter : IJobIndexConverter
    {

        #region Fields

        private readonly IJobIndexDataProvider jobIndexDataProvider;
        private readonly IJobIndexServiceWrapper jobIndexService;
        private readonly IJobIndexInPeriodServiceWrapper jobIndexAssignmentService;
        private readonly IEventPublisher publisher;
        private Object lockThis = new Object();

        #endregion

        #region Constructors

        public JobIndexConverter(IJobIndexDataProvider jobIndexDataProvider,
            IJobIndexServiceWrapper jobIndexService, IJobIndexInPeriodServiceWrapper jobIndexAssignmentService, IEventPublisher publisher)
        {
            this.jobIndexDataProvider = jobIndexDataProvider;
            this.jobIndexService = jobIndexService;
            this.jobIndexAssignmentService = jobIndexAssignmentService;
            this.publisher = publisher;
        }

        #endregion

        #region Public Methods

        public void ConvertJobIndex(Period period)
        {
            var jobIndexInperiodList = new List<JobIndexInPeriodDTO>();
            var sourceJobIndexListId = jobIndexDataProvider.GetJobIndexListId();
            foreach (var sourceJobIndexId in sourceJobIndexListId)
            {

                var sourceJobIndexDTO = jobIndexDataProvider.GetBy(sourceJobIndexId);
                var desJobIndexDTO = createDestinationJobIndex(sourceJobIndexDTO);
                jobIndexService.AddJobIndex((jobIndexWithOutCf, addJobIndexExp) =>
                {
                    if (addJobIndexExp != null)
                    {
                        handleException(addJobIndexExp);
                    }
                    else
                    {
                        jobIndexService.GetJobIndex((jobIndexWithCf, getJobIndexExp) =>
                        {
                            if (getJobIndexExp != null)
                                handleException(getJobIndexExp);
                            else
                            {
                                var periodJobIndexDTO = createPeriodJobIndexDTO(jobIndexWithCf, period, sourceJobIndexDTO);
                                jobIndexAssignmentService.AddJobIndexInPeriod((res, exp) =>
                                {
                                    if (exp != null)
                                    {
                                        handleException(exp);
                                    }
                                    else
                                    {
                                        jobIndexInperiodList.Add(res);
                                        if (jobIndexInperiodList.Count == sourceJobIndexListId.Count)
                                            publisher.Publish(new JobIndexConverted(jobIndexInperiodList));
                                    }

                                }, periodJobIndexDTO);
                            }
                        }, jobIndexWithOutCf.Id);
                    }

                }, desJobIndexDTO);

            }
        }

        #endregion

        #region Private Methods

        private JobIndexDTO createDestinationJobIndex(JobIndexIntegrationDTO sourceJobIndex)
        {
            var res = new JobIndexDTO
            {
                Name = sourceJobIndex.Title,
                ParentId = PMSCostantData.JobIndexCategoryId,
                CustomFields = new List<CustomFieldDTO>
                {
                    new CustomFieldDTO
                    {
                        Id = PMSCostantData.JobIndexFieldId,
                        Name = "sdfsdfsd",
                        DictionaryName = "ksdkfhjskdfjs",
                        EntityId = 1,
                        MaxValue = 10,
                        MinValue = 1,
                        TypeId = "string",
                    }
                },
                DictionaryName = sourceJobIndex.ID.ToString(),
                TransferId = sourceJobIndex.TransferId
            };
            return res;
        }

        private JobIndexInPeriodDTO createPeriodJobIndexDTO(JobIndexDTO jobIndex, Period period, JobIndexIntegrationDTO sourceJobIndexDTO)
        {
            var res = new JobIndexInPeriodDTO
            {
                //todo: Kharabe
                CalculationLevel = 1,
                CalculationOrder = 1,
                IsInquireable = true,
                Name = jobIndex.Name,
                DictionaryName = jobIndex.DictionaryName,
                JobIndexId = jobIndex.Id,
                PeriodId = period.Id,
                CustomFields = jobIndex.CustomFields.Select(c => new AbstractCustomFieldDescriptionDTO
                {
                    Id = c.Id,
                    Name = "fake",
                    Value = "1"
                }).ToList()

            };
            if (sourceJobIndexDTO.IndexType==1)
            {
                res.ParentId = PMSCostantData.JobIndexGroupBehaviaral;
            }
            else
            {
                res.ParentId = PMSCostantData.JobIndexGroupPerformance;
            }
            return res;
        }

        private void handleException(Exception exception)
        {
            throw new Exception("bad shod", exception);
        }
        #endregion

    }
}
