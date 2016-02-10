using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryUnitIndexPoints;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSReport.Domain.Model;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    //  [Interceptor(typeof(Interception))]
    public class UnitInquiryServiceFacade : IUnitInquiryServiceFacade
    {
        private readonly IUnitInquiryService inquiryService;
        private readonly IMapper<InquirySubjectWithUnit, InquiryUnitDTO> inquiySubjectMapper;
        private readonly IUnitIndexRepository unitIndexRep;


        public UnitInquiryServiceFacade(IUnitInquiryService inquiryService,
            IMapper<InquirySubjectWithUnit, InquiryUnitDTO> inquiySubjectMapper,
            IUnitIndexRepository unitIndexRep)
        {
            this.inquiryService = inquiryService;
            this.inquiySubjectMapper = inquiySubjectMapper;
            this.unitIndexRep = unitIndexRep;
        }

        [RequiredPermission(ActionType.ShowUnitInPeriodInquiry)]
        public List<InquiryUnitDTO> GetInquirerInquirySubjects(long periodId, string inquirerEmployeeNo)
        {
            var res = new List<InquiryUnitDTO>();
            List<InquirySubjectWithUnit> inquirySubjects =
                 inquiryService.GetInquirySubjects(new EmployeeId(inquirerEmployeeNo, new PeriodId(periodId)));
            res = inquirySubjects.Select(i => inquiySubjectMapper.MapToModel(i)).ToList();

            res.ForEach(c =>
            {
             
                c.IndexName = unitIndexRep.GetUnitIndexById(new AbstractUnitIndexId(Convert.ToInt64(c.IndexName))).Name;
            });
            return res;
        }

        [RequiredPermission(ActionType.ShowUnitInPeriodInquiry)]
        public InquiryUnitFormDTO GetInquiryForm(long periodId, string inquirerEmployeeNo, long unitId)
        {
            List<InquiryUnitIndexPoint> inquryUnitIndexPoints =
                inquiryService.GetAllInquiryUnitIndexPointBy(
                    new EmployeeId(inquirerEmployeeNo, new PeriodId(periodId)),
                    new UnitId(new PeriodId(periodId), new SharedUnitId(unitId))
                   );

            // TODO:(LOW)  Mapper and Domain Report Needed
            var inquiryForm = new InquiryUnitFormDTO
                {
                    InquirerEmployeeNo = inquirerEmployeeNo,
                    PeriodId = periodId,
                    InquiryUnitId = unitId,
                  
                };
        
            inquiryForm.UnitIndexValueList=new List<UnitIndexValueDTO>();
            foreach (var inquiryUnitIndexPoint in inquryUnitIndexPoints)
            {
            
            //var abstractUnitIndex = unitIndexRep.GetById(inquiryUnitIndexPoint.UnitIndexId);
            //var unitIndex = abstractUnitIndex as UnitIndex;
            //if (unitIndex != null && unitIndex.IsInquireable)
            //{
                inquiryForm.UnitIndexValueList.Add(new UnitIndexValueDTO
                {
                Id = inquiryUnitIndexPoint.Id.Id,
                IndexValue = inquiryUnitIndexPoint.UnitIndexValue,
                UnitIndexId = inquiryUnitIndexPoint.ConfigurationItemId.UnitIndexIdUintPeriod.Id,
                UnitIndexName = unitIndexRep.GetUnitIndexById(inquiryUnitIndexPoint.ConfigurationItemId.UnitIndexIdUintPeriod).Name
                 });
           // }
            }


            return inquiryForm;
        }

        public InquiryUnitFormDTO UpdateInquirySubjectForm(InquiryUnitFormDTO inquiryForm)
        {

            var res = inquiryForm.UnitIndexValueList.Select(item => new InquiryUnitIndexPoinItem(new UnitInquiryConfigurationItemId(null, 
                new EmployeeId(inquiryForm.InquirerEmployeeNo,
                new PeriodId(inquiryForm.PeriodId)),
                new UnitId(new PeriodId(inquiryForm.PeriodId),
                new SharedUnitId(inquiryForm.InquiryUnitId)),
                new AbstractUnitIndexId(item.UnitIndexId)), item.IndexValue)).ToList();

            inquiryService.UpdateInquiryUnitIndexPoints(res);
           
            return inquiryForm;
        }


    }
}
