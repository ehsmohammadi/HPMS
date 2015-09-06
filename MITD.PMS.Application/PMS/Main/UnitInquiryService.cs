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
        private readonly IUnitInquiryConfiguratorService configurator;
        private readonly IEmployeeRepository employeeRep;
        private readonly IInquiryUnitIndexPointRepository inquiryUnitIndexPointRep;

        private readonly IUnitRepository unitRep;
        private readonly IUnitIndexRepository unitIndexRep;
        private readonly IInquiryUnitIndexPointService inquiryUnitIndexPointService;
        private readonly IPeriodManagerService periodChecker;


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

        public List<InquirySubjectWithUnit> GetInquirySubjects(EmployeeId inquirerEmployeeId)
        {
            //var inquirer = employeeRep.GetBy(inquirerEmployeeId);
            //periodChecker.CheckShowingInquirySubject(inquirer);
            //var configurationItems = configurator.GetUnitInquiryConfigurationItemBy(inquirer);
            //return employeeRep.GetEmployeeByWithUnit(configurationItems.Select(c => c.Id),inquirer.Id.PeriodId);


            var res = new List<InquirySubjectWithUnit>();
            var inquirer = employeeRep.GetBy(inquirerEmployeeId);

            //todo bz
            // periodChecker.CheckShowingInquirySubject(inquirer);
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

        //  public List<InquiryUnitIndexPoint> GetAllInquiryUnitIndexPointBy(UnitInquiryConfigurationItemId configurationItemId)
        public List<InquiryUnitIndexPoint> GetAllInquiryUnitIndexPointBy(EmployeeId employeeId, UnitId id)
        {
            #region comment

            //var unitPosition = unitPositionRep.GetBy(configurationItemId.InquirySubjectUnitId);
            //periodChecker.CheckShowingInquiryUnitIndexPoint(unitPosition);
            //var itm = unitPosition.ConfigurationItemList.Single(c => c.Id.Equals(configurationItemId));
            //CreateAllInquiryUnitIndexPoint(itm);
            //return inquiryUnitIndexPointRep.GetAllBy(itm.Id);

            //var unit = unitRep.GetBy(configurationItemId.InquirySubjectUnitId);

            ////todo bz question from mh
            //// periodChecker.CheckShowingInquiryUnitIndexPoint(unit);
            //var itm = unit.ConfigurationItemList.Single(c => c.Id.Equals(configurationItemId));
            //CreateAllInquiryUnitIndexPoint(itm);
            //return inquiryUnitIndexPointRep.GetAllBy(itm.Id);

            #endregion

            ////todo bz question from mh
            //// periodChecker.CheckShowingInquiryUnitIndexPoint(unit);
            var inquirer = employeeRep.GetBy(employeeId);
            var configurationItems = configurator.GetUnitInquiryConfigurationItemBy(inquirer);
            var itm = configurationItems.Where(c => c.Id.InquirySubjectUnitId == id).ToList();
            foreach (var unitInquiryConfigurationItem in itm)
            {
                CreateAllInquiryUnitIndexPoint(unitInquiryConfigurationItem);
            }
            return inquiryUnitIndexPointRep.GetAllBy(employeeId,id);
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
            if (inquryUnitIndexPoints == null)
            {
                create(itm);
            }
        }

        private void create(UnitInquiryConfigurationItem configurationItem)
        {
           // var unit = unitRep.GetBy(configurationItem.Id.InquirySubjectUnitId);
            //foreach (var unitUnitIndex in unit.UnitIndexList)
            //{
            //    //todo check for no error
            //    var unitIndex = unitIndexRep.GetById(unitUnitIndex.UnitIndexId);
            //    if ((unitIndex as UnitIndex).IsInquireable)
            //    {
            //        //if ((configurationItem.InquirerUnitLevel == UnitLevel.Childs &&
            //        //     unitUnitIndex.ShowforLowLevel) ||
            //        //    (configurationItem.InquirerUnitLevel == UnitLevel.Parents &&
            //        //     unitUnitIndex.ShowforTopLevel) ||
            //        //    (configurationItem.InquirerUnitLevel == UnitLevel.Siblings &&
            //        //     unitUnitIndex.ShowforSameLevel) || configurationItem.InquirerUnitLevel == UnitLevel.None)
            //            inquiryUnitIndexPointService.Add(configurationItem, unitIndex as UnitIndex, string.Empty);

            //    }

            //todo bz question for check IsInquireable
            //   var unitIndex = unit.UnitIndexList.Single(c => c.UnitIndexId == configurationItem.Id.UnitIndexIdUintPeriod);
            inquiryUnitIndexPointService.Add(configurationItem, string.Empty);
        }


       
    }


}
