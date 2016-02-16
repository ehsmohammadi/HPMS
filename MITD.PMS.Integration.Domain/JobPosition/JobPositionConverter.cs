
using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class JobPositionConverter : IJobPositionConverter
    {
        #region Fields
        private readonly IJobPositionDataProvider jobPositionDataProvider;
        private readonly IJobPositionServiceWrapper jobPositionService;
        private readonly IJobPositionInPeriodServiceWrapper jobPositionInPeriodServiceWrapper;
        private List<UnitDTO> unitList;
        private List<JobDTO> jobInPeriodList;
        private JobPositionIntegrationDTO root;
        private List<JobPositionDTO> jobPositionList = new List<JobPositionDTO>();
        private int totalJobPositionsCount;
        private readonly IEventPublisher publisher;
        #endregion

        #region Constructors
        public JobPositionConverter(IJobPositionDataProvider jobPositionDataProvider, IJobPositionServiceWrapper jobPositionService, IJobPositionInPeriodServiceWrapper jobPositionInPeriodServiceWrapper, IEventPublisher publisher)
        {
            this.jobPositionDataProvider = jobPositionDataProvider;
            this.jobPositionService = jobPositionService;
            this.jobPositionInPeriodServiceWrapper = jobPositionInPeriodServiceWrapper;
            this.publisher = publisher;
        }

        #endregion

        #region public methods
        public void ConvertJobPositions(Period period, List<UnitDTO> unitList, List<JobDTO> jobList)
        {
            Console.WriteLine("Starting jobPositions Convert progress...");
            this.unitList = unitList;
            jobInPeriodList = jobList;
            root = jobPositionDataProvider.GetRoot();
            totalJobPositionsCount = jobPositionDataProvider.GetCount();
            convertJobPosition_Rec(root, period.Id, null);
            publisher.Publish(new JobPositionConverted(jobPositionList));
        }

      
        #endregion

        #region Private methods


        private void convertJobPosition_Rec(JobPositionIntegrationDTO sourceJobPositionDTO, long periodId, long? jobPositionParentIdParam)
        {
            var desJobPositionDTO = createDestinationJobPosition(sourceJobPositionDTO);
            var jobPosition = jobPositionService.AddJobPosition(desJobPositionDTO);
            var unit = unitList.Single(u => sourceJobPositionDTO.UnitIntegrationDTO.TransferId == u.TransferId);
            //var unit =
            //    (from c in unitList where c.TransferId == sourceJobPositionDTO.UnitIntegrationDTO.TransferId select c)
            //        .Single();
            var job = jobInPeriodList.Single(u => sourceJobPositionDTO.JobIntegrationDto.TransferId == u.TransferId);
            var jobPositionInPriodAssignment = createDestinationJobPositionInPeriod(jobPosition,periodId,unit.Id,job.Id, jobPositionParentIdParam);
            var res = jobPositionInPeriodServiceWrapper.AddJobPositionInPeriod(jobPositionInPriodAssignment);
            jobPositionList.Add(jobPosition);
            var jobPositionDataChildIdList = jobPositionDataProvider.GetChildIDs(sourceJobPositionDTO.ID);
            foreach (var jobPositionDataChildId in jobPositionDataChildIdList)
            {
                var jobPositiondataChid = jobPositionDataProvider.GetJobPositionDetail(jobPositionDataChildId);
                convertJobPosition_Rec(jobPositiondataChid, periodId, res.JobPositionId);
            }
            Console.WriteLine("JobPosition convert progress state: " + jobPositionList.Count + " From " + totalJobPositionsCount.ToString());

        }

        private void handleException(Exception exception)
        {
            throw new Exception("Error In Add JobPosition", exception);
        }


        private JobPositionDTO createDestinationJobPosition(JobPositionIntegrationDTO sourceJobPosition)
        {
            var res = new JobPositionDTO
                      {
                          Name = sourceJobPosition.JobPositionName,
                          DictionaryName = "jp" + sourceJobPosition.ID,
                          TransferId = sourceJobPosition.TransferId

                      };
            return res;
        }


        private JobPositionInPeriodAssignmentDTO createDestinationJobPositionInPeriod(JobPositionDTO jobPosition,long periodId,long unitId,long jobId, long? parentId)
        {
            var res = new JobPositionInPeriodAssignmentDTO
            {
                JobPositionId = jobPosition.Id,
                PeriodId = periodId,
                ParentJobPositionId = parentId,
                JobId = jobId,
                UnitId = unitId
            };
            return res;
        }


        #endregion
    }


}


