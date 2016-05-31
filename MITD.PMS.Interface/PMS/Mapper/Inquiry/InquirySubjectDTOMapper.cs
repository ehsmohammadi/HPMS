using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSReport.Domain.Model;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class InquirySubjectDTOMapper : BaseMapper<InquirySubjectWithJobPosition, InquirySubjectDTO>, IMapper<InquirySubjectWithJobPosition, InquirySubjectDTO>
    {

        public override InquirySubjectDTO MapToModel(InquirySubjectWithJobPosition entity)
        {
            var res = new InquirySubjectDTO
            {
                FullName = entity.InquirySubject.FullName,
                EmployeeNo = entity.InquirySubject.Id.EmployeeNo,
                JobPositionId = entity.InquirySubjectJobPosition.Id.SharedJobPositionId.Id,             
                JobPositionName = entity.InquirySubjectJobPosition.Name,
                InquirerJobPositionId=entity.InquirerJobPosition.Id.SharedJobPositionId.Id,
                InquirerJobPositionName=entity.InquirerJobPosition.Name,
                Status = entity.IsCompleted?"کامل شده":"ناقص",
                IsInquired = entity.IsCompleted,
                ActionCodes = new List<int>
                {
                    (int) ActionType.FillInquiryForm
                }
               
            };
            return res;

        }

        public override InquirySubjectWithJobPosition MapToEntity(InquirySubjectDTO model)
        {
            throw new InvalidOperationException();

        }

    }

}
