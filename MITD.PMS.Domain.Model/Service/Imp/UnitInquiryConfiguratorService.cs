using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Units;

namespace MITD.PMS.Domain.Service
{
    public class UnitInquiryConfiguratorService : IUnitInquiryConfiguratorService
    {
        private readonly IUnitRepository _unitRep;
        private readonly IJobPositionRepository _jobPositionRepository;
        private readonly IJobPositionInquiryConfiguratorService _jobPositionInquiryConfiguratorService;


        public UnitInquiryConfiguratorService(IUnitRepository unitRep,IJobPositionRepository jobPositionRepository,IJobPositionInquiryConfiguratorService jobPositionInquiryConfiguratorService)
        {
            this._unitRep = unitRep;
            _jobPositionRepository = jobPositionRepository;
            _jobPositionInquiryConfiguratorService = jobPositionInquiryConfiguratorService;
        }

        public List<UnitInquiryConfigurationItem> Configure(Unit unit)
        {
           // var childs = _unitRep.GetAllUnitByParentId(unit.Id);
        //    var siblings = _unitRep.Find(j => j.Parent == unit.Parent && j.Id.PeriodId.Id == unit.Id.PeriodId.Id);
           // var inquirersunits = childs.ToDictionary(j => j, u => UnitLevel.Childs);
           // inquirersunits = inquirersunits.Concat(siblings.ToDictionary(j => j, u => UnitLevel.Siblings)).ToDictionary(s => s.Key, s => s.Value);
           
           // if (unit.Parent != null)
             //   inquirersunits.Add(unit.Parent, UnitLevel.Parents);
            /////////////////////////////////////////////////////////////////////////////////////////

           // var inquirers = new Dictionary<UnitEmployee, UnitLevel>();
            //foreach (var jp in inquirersunits)
              //  inquirers = inquirers.Concat(jp.Key.Employees.ToDictionary(e => e, u => jp.Value)).ToDictionary(s => s.Key, s => s.Value);

            //todo New Code

            var parentUnit = unit.Parent;

            var jobpositions =
                _jobPositionRepository.Find(
                    c => c.UnitId.SharedUnitId == parentUnit.SharedUnit.Id && c.UnitId.PeriodId.Id == parentUnit.Id.PeriodId.Id);
            var res = new List<UnitInquiryConfigurationItem>();
            foreach (var jobposition in jobpositions)
            {
                jobposition.ConfigeInquirer(_jobPositionInquiryConfiguratorService,false);
                foreach (var empl in jobposition.Employees)
                {
                    //res.Add(
                    // new UnitInquiryConfigurationItem(
                    //     new UnitInquiryConfigurationItemId(parentUnit.Id, empl.EmployeeId, unit.Id)
                    //     , unit, true, true));
                }

            }

            //var res = new List<UnitInquiryConfigurationItem>();
            //foreach (var inquirySubject in unit.Employees)
            //{
            //    foreach (var inquirer in inquirers)
            //        res.Add(
            //            new UnitInquiryConfigurationItem(
            //                new UnitInquiryConfigurationItemId(inquirer.Key.Unit.Id, inquirer.Key.EmployeeId, unit.Id,
            //                    inquirySubject.EmployeeId), unit, true, true, inquirer.Value));
            //}

            return res;
        }

        public List<UnitInquiryConfigurationItem> GetUnitInquiryConfigurationItemBy(Employee inquirer)
        {
            if (inquirer == null)
                return new List<UnitInquiryConfigurationItem>();
            var unitInquirySubjects = _unitRep.GetAllInquirySubjectUnits(inquirer.Id);
            var res = new List<UnitInquiryConfigurationItem>();
            foreach (var itm in unitInquirySubjects)
            {
                res.AddRange(itm.ConfigurationItemList.Where(c => c.Id.InquirerId.Equals(inquirer.Id)));
            }
            return res.Distinct().ToList();
        }
    }
}
