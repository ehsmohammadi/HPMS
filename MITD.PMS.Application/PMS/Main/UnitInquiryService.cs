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
    public class UnitInquiryService : IUnitInquiryService
    {
        #region Fields
        private readonly IUnitInquiryConfiguratorService configurator;
        private readonly IEmployeeRepository employeeRep;
        private readonly IInquiryUnitIndexPointRepository inquiryUnitIndexPointRep;
        private readonly IUnitRepository unitRep;
        private readonly IUnitIndexRepository unitIndexRep;
        private readonly IInquiryUnitIndexPointService inquiryUnitIndexPointService;
        private readonly IPeriodManagerService periodChecker; 
        #endregion


        #region Constructors
        public UnitInquiryService(
            IUnitInquiryConfiguratorService configurator,
            IEmployeeRepository employeeRep,
            IInquiryUnitIndexPointRepository inquiryUnitIndexPointRep,
            IUnitRepository unitRep,
            IUnitIndexRepository unitIndexRep,
            IInquiryUnitIndexPointService inquiryUnitIndexPointService,
            IPeriodManagerService periodChecker
            )
        {
            this.configurator = configurator;
            this.employeeRep = employeeRep;
            this.inquiryUnitIndexPointRep = inquiryUnitIndexPointRep;
            this.unitRep = unitRep;
            this.unitIndexRep = unitIndexRep;
            this.inquiryUnitIndexPointService = inquiryUnitIndexPointService;
            this.periodChecker = periodChecker;
        } 
        #endregion

        #region Methods
        public List<InquirySubjectWithUnit> GetInquirySubjects(EmployeeId inquirerEmployeeId)
        {
            var res = new List<InquirySubjectWithUnit>();
            var inquirer = employeeRep.GetBy(inquirerEmployeeId);
            var configurationItems = configurator.GetUnitInquiryConfigurationItemBy(inquirer);
            foreach (var item in configurationItems)
            {
                res.Add(new InquirySubjectWithUnit()
                {
                    InquirerUnit = inquirer,
                    InquirySubject = unitRep.GetBy(item.Id.InquirySubjectUnitId),
                    UnitIndex = new UnitUnitIndex(item.Id.UnitIndexIdUintPeriod, false, false, false)

                });
            }

            return res;

        }

        public List<InquiryUnitIndexPoint> GetAllInquiryUnitIndexPointBy(EmployeeId employeeId, UnitId id)
        {

            var inquirer = employeeRep.GetBy(employeeId);
            var configurationItems = configurator.GetUnitInquiryConfigurationItemBy(inquirer);
            var itm = configurationItems.Where(c => c.Id.InquirySubjectUnitId == id).ToList();
            foreach (var unitInquiryConfigurationItem in itm)
            {
                CreateAllInquiryUnitIndexPoint(unitInquiryConfigurationItem);
            }
            return inquiryUnitIndexPointRep.GetAllBy(employeeId, id);
        }

        public void UpdateInquiryUnitIndexPoints(List<InquiryUnitIndexPoinItem> inquiryUnitIndexPoinItems)
        {
            foreach (var inquiryUnitIndexPoinItem in inquiryUnitIndexPoinItems)
            {
                inquiryUnitIndexPointService.Update(inquiryUnitIndexPoinItem.ConfigurationItemId,
                    inquiryUnitIndexPoinItem.ConfigurationItemId.UnitIndexIdUintPeriod, inquiryUnitIndexPoinItem.UnitIndexValue);
            }
        }

        public void CreateAllInquiryUnitIndexPoint(UnitInquiryConfigurationItem itm)
        {
            var inquryUnitIndexPoints = inquiryUnitIndexPointRep.GetBy(itm.Id);
            if (inquryUnitIndexPoints == null )
            {
                create(itm);
            }
        }

        private void create(UnitInquiryConfigurationItem configurationItem)
        {
#if(DEBUG)
            inquiryUnitIndexPointService.Add(configurationItem, "3");
#else
                   inquiryUnitIndexPointService.Add(configurationItem, string.Empty);
#endif
        } 
        #endregion



    }


}
