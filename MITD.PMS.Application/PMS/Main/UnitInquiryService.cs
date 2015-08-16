using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryUnitIndexPoints;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;
using MITD.PMSReport.Domain.Model;

namespace MITD.PMS.Application
{
    public class UnitInquiryService : IInquiryService
    {
        private readonly IUnitInquiryConfiguratorService configurator;
        private readonly IEmployeeRepository employeeRep;
        private readonly IInquiryUnitIndexPointRepository inquiryUnitIndexPointRep;
        private readonly IUnitRepository jobPositionRep;
        private readonly IUnitRepository jobRep;
        private readonly IUnitIndexRepository jobIndexRep;
      //  private readonly IInquiryUnitIndexPointService inquiryUnitIndexPointService;
        private readonly IPeriodManagerService periodChecker;


        public UnitInquiryService(
            IUnitInquiryConfiguratorService configurator,
            IEmployeeRepository employeeRep,
            IInquiryUnitIndexPointRepository inquiryUnitIndexPointRep,
            IUnitRepository jobPositionRep,
            IUnitRepository jobRep,
            IUnitIndexRepository jobIndexRep,
         //   IInquiryUnitIndexPointService inquiryUnitIndexPointService,
            IPeriodManagerService periodChecker
            )
        {
            this.configurator = configurator;
            this.employeeRep = employeeRep;
            this.inquiryUnitIndexPointRep = inquiryUnitIndexPointRep;
            this.jobPositionRep = jobPositionRep;
            this.jobRep = jobRep;
            this.jobIndexRep = jobIndexRep;
         //   this.inquiryUnitIndexPointService = inquiryUnitIndexPointService;
            this.periodChecker = periodChecker;
        }

        public List<InquirySubjectWithUnit> GetInquirySubjects(EmployeeId inquirerEmployeeId)
        {
            var inquirer = employeeRep.GetBy(inquirerEmployeeId);
            periodChecker.CheckShowingInquirySubject(inquirer);
            var configurationItems = configurator.GetUnitInquiryConfigurationItemBy(inquirer);
            return employeeRep.GetEmployeeByWithUnit(configurationItems.Select(c => c.Id),inquirer.Id.PeriodId);
        }

        public List<InquiryUnitIndexPoint> GetAllInquiryUnitIndexPointBy(UnitInquiryConfigurationItemId configurationItemId)
        {
            var jobPosition = jobPositionRep.GetBy(configurationItemId.InquirySubjectUnitId);
            periodChecker.CheckShowingInquiryUnitIndexPoint(jobPosition);
            var itm = jobPosition.ConfigurationItemList.Single(c => c.Id.Equals(configurationItemId));
            CreateAllInquiryUnitIndexPoint(itm);
            return inquiryUnitIndexPointRep.GetAllBy(itm.Id);
        }

        public void UpdateInquiryUnitIndexPoints(IEnumerable<InquiryUnitIndexPoinItem> inquiryUnitIndexPoinItems)
        {
            foreach (var inquiryUnitIndexPoinItem in inquiryUnitIndexPoinItems)
            {
                inquiryUnitIndexPointService.Update(inquiryUnitIndexPoinItem.ConfigurationItemId,
                    inquiryUnitIndexPoinItem.UnitIndexId, inquiryUnitIndexPoinItem.UnitIndexValue);
            }
        }

        public void CreateAllInquiryUnitIndexPoint(UnitInquiryConfigurationItem itm)
        {
            var inquryUnitIndexPoints = inquiryUnitIndexPointRep.GetAllBy(itm.Id);
            if (inquryUnitIndexPoints == null || inquryUnitIndexPoints.Count == 0)
            {
                create(itm);
            }
        }

        private void create(UnitInquiryConfigurationItem configurationItem)
        {
            var job = jobRep.GetById(configurationItem.Unit.UnitId);
            foreach (var jobUnitIndex in job.UnitIndexList)
            {
                //todo check for no error
                var jobIndex = jobIndexRep.GetById(jobUnitIndex.UnitIndexId);
                if ((jobIndex as UnitIndex).IsInquireable)
                {
                    if ((configurationItem.InquirerUnitLevel == UnitLevel.Childs &&
                         jobUnitIndex.ShowforLowLevel) ||
                        (configurationItem.InquirerUnitLevel == UnitLevel.Parents &&
                         jobUnitIndex.ShowforTopLevel) ||
                        (configurationItem.InquirerUnitLevel == UnitLevel.Siblings &&
                         jobUnitIndex.ShowforSameLevel) || configurationItem.InquirerUnitLevel == UnitLevel.None)
                        inquiryUnitIndexPointService.Add(configurationItem, jobIndex as UnitIndex, string.Empty);

                }

            }
        }
    }

  
}
