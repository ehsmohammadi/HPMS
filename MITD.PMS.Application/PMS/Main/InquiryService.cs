using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Service;
using MITD.PMSReport.Domain.Model;

namespace MITD.PMS.Application
{
    public class InquiryService : IInquiryService
    {
        #region Fields
        private readonly IJobPositionInquiryConfiguratorService configurator;
        private readonly IEmployeeRepository employeeRep;
        private readonly IInquiryJobIndexPointRepository inquiryJobIndexPointRep;
        private readonly IJobPositionRepository jobPositionRep;
        private readonly IJobRepository jobRep;
        private readonly IJobIndexRepository jobIndexRep;
        private readonly IInquiryJobIndexPointService inquiryJobIndexPointService;
        private readonly IPeriodManagerService periodChecker; 
        #endregion

        #region Constructors
        public InquiryService(IJobPositionInquiryConfiguratorService configurator,
            IEmployeeRepository employeeRep,
            IInquiryJobIndexPointRepository inquiryJobIndexPointRep,
            IJobPositionRepository jobPositionRep,
            IJobRepository jobRep,
            IJobIndexRepository jobIndexRep,
            IInquiryJobIndexPointService inquiryJobIndexPointService,
            IPeriodManagerService periodChecker
            )
        {
            this.configurator = configurator;
            this.employeeRep = employeeRep;
            this.inquiryJobIndexPointRep = inquiryJobIndexPointRep;
            this.jobPositionRep = jobPositionRep;
            this.jobRep = jobRep;
            this.jobIndexRep = jobIndexRep;
            this.inquiryJobIndexPointService = inquiryJobIndexPointService;
            this.periodChecker = periodChecker;
        } 
        #endregion

        #region Methods
        public List<InquirySubjectWithJobPosition> GetInquirySubjects(EmployeeId inquirerEmployeeId)
        {
            var inquirer = employeeRep.GetBy(inquirerEmployeeId);

            // Return a list with 0 elements indicating that the given employee has no inquiry subjects
            if (inquirer == null)
                return new List<InquirySubjectWithJobPosition>();

            periodChecker.CheckShowingInquirySubject(inquirer);
            var configurationItems = configurator.GetJobPositionInquiryConfigurationItemBy(inquirer);
            return employeeRep.GetEmployeeByWithJobPosition(configurationItems.Select(c => c.Id), inquirer.Id.PeriodId);
        }

        public List<InquiryJobIndexPoint> GetAllInquiryJobIndexPointBy(JobPositionInquiryConfigurationItemId configurationItemId)
        {
            var jobPosition = jobPositionRep.GetBy(configurationItemId.InquirySubjectJobPositionId);
            periodChecker.CheckShowingInquiryJobIndexPoint(jobPosition);
            var itm = jobPosition.ConfigurationItemList.Single(c => c.Id.Equals(configurationItemId));
            CreateAllInquiryJobIndexPoint(itm);
            return inquiryJobIndexPointRep.GetAllBy(itm.Id);
        }

        public void UpdateInquiryJobIndexPoints(IEnumerable<InquiryJobIndexPoinItem> inquiryJobIndexPoinItems)
        {
            foreach (var inquiryJobIndexPoinItem in inquiryJobIndexPoinItems)
            {
                inquiryJobIndexPointService.Update(inquiryJobIndexPoinItem.ConfigurationItemId,
                    inquiryJobIndexPoinItem.JobIndexId, inquiryJobIndexPoinItem.JobIndexValue);
            }
        }

        public void CreateAllInquiryJobIndexPoint(JobPositionInquiryConfigurationItem itm)
        {
            var inquryJobIndexPoints = inquiryJobIndexPointRep.GetAllBy(itm.Id);
            if (inquryJobIndexPoints == null || inquryJobIndexPoints.Count == 0)
            {
                create(itm);
            }
        }

        private void create(JobPositionInquiryConfigurationItem configurationItem)
        {
            var job = jobRep.GetById(configurationItem.JobPosition.JobId);
            foreach (var jobJobIndex in job.JobIndexList)
            {
                var jobIndex = jobIndexRep.GetById(jobJobIndex.JobIndexId);
                if ((jobIndex as JobIndex).IsInquireable)
                {
                    if ((configurationItem.InquirerJobPositionLevel == JobPositionLevel.Childs &&
                         jobJobIndex.ShowforLowLevel) ||
                        (configurationItem.InquirerJobPositionLevel == JobPositionLevel.Parents &&
                         jobJobIndex.ShowforTopLevel) ||
                        (configurationItem.InquirerJobPositionLevel == JobPositionLevel.Siblings &&
                         jobJobIndex.ShowforSameLevel) ||
                        configurationItem.InquirerJobPositionLevel == JobPositionLevel.None)
                    {
#if(DEBUG)
                        inquiryJobIndexPointService.Add(configurationItem, jobIndex as JobIndex, "5");
#else
                    inquiryJobIndexPointService.Add(configurationItem, jobIndex as JobIndex, string.Empty);
#endif
                    }


                }

            }
        } 
        #endregion
    }

  
}
