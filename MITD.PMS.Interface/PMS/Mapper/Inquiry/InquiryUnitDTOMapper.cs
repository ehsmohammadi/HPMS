using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSReport.Domain.Model;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class InquiryUnitDTOMapper : BaseMapper<InquirySubjectWithUnit, InquiryUnitDTO>, IMapper<InquirySubjectWithUnit, InquiryUnitDTO>
    {

        public override InquiryUnitDTO MapToModel(InquirySubjectWithUnit entity)
        {
            var res = new InquiryUnitDTO
            {
                FullName = entity.InquirerUnit.FullName,
                EmployeeNo = entity.InquirerUnit.Id.EmployeeNo,
                IndexName = entity.UnitIndex.UnitIndexId.Id.ToString(),
                UnitName=entity.InquirySubject.Name,
                UnitId = entity.InquirySubject.Id.SharedUnitId.Id,
                ActionCodes = new List<int>
                {
                    (int) ActionType.FillInquiryUnitForm
                },
               
            };
            return res;

        }

        public override InquirySubjectWithUnit MapToEntity(InquiryUnitDTO model)
        {
            throw new InvalidOperationException();

        }

    }

}
