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

namespace MITD.PMS.Interface
{
    //[Interceptor(typeof(Interception))]
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

        public InquiryUnitFormDTO GetInquiryForm(long periodId, string inquirerEmployeeNo, long unitId, long indexId)
        {
            List<InquiryUnitIndexPoint> inquryUnitIndexPoints =
                inquiryService.GetAllInquiryUnitIndexPointBy(new UnitInquiryConfigurationItemId(
                    null,
                    new EmployeeId(inquirerEmployeeNo, new PeriodId(periodId)),
                    new UnitId(new PeriodId(periodId), new SharedUnitId(unitId)),
                    new AbstractUnitIndexId(indexId)
                    ));

            // TODO:  Mapper and Domain Report Needed
            var inquiryForm = new InquiryUnitFormDTO
                {
                    InquirerEmployeeNo = inquirerEmployeeNo,
                    PeriodId = periodId,
                    InquiryUnitId = unitId,
                    UnitIndexId = indexId
                };
            //   var inquiryUnitIndexValueList = new List<UnitIndexValueDTO>();

            var inquiryUnitIndexValue = new UnitIndexValueDTO();
            var inquiryUnitIndexPoint = inquryUnitIndexPoints.SingleOrDefault();
            //foreach (var inquiryUnitIndexPoint in inquryUnitIndexPoints)
            //{
            var abstractUnitIndex = unitIndexRep.GetById(inquiryUnitIndexPoint.UnitIndexId);
            var unitIndex = abstractUnitIndex as UnitIndex;
            if (unitIndex != null && unitIndex.IsInquireable)
            {
                //inquiryUnitIndexValueList.Add(new UnitIndexValueDTO
                //{

                inquiryUnitIndexValue.Id = inquiryUnitIndexPoint.Id.Id;
                inquiryUnitIndexValue.IndexValue = inquiryUnitIndexPoint.UnitIndexValue;
                inquiryUnitIndexValue.UnitIndexId = inquiryUnitIndexPoint.UnitIndexId.Id;
                inquiryUnitIndexValue.UnitIndexName = (unitIndex).Name;
                // });
            }
            //}

            //todo bz
            // inquiryForm.UnitIndexValueDTO = inquiryUnitIndexValue;

            return inquiryForm;
        }

        public InquiryUnitFormDTO UpdateInquirySubjectForm(InquiryUnitFormDTO inquiryForm)
        {
            inquiryService.UpdateInquiryUnitIndexPoints(
                new InquiryUnitIndexPoinItem(
                    new UnitInquiryConfigurationItemId(null,
                        new EmployeeId(inquiryForm.InquirerEmployeeNo, new PeriodId(inquiryForm.PeriodId)),
                        new UnitId(new PeriodId(inquiryForm.PeriodId), new SharedUnitId(inquiryForm.InquiryUnitId)),
                        new AbstractUnitIndexId(inquiryForm.UnitIndexId)
                        ), null));//todo bz  //inquiryForm.UnitIndexValueDTO.IndexValue));


            return inquiryForm;
        }


    }
}
