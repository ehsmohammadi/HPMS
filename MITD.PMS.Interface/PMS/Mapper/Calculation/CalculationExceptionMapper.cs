using MITD.Core;
using MITD.PMS.Domain.Model.CalculationExceptions;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.Policies;
using MITD.PMSReport.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class CalculationExceptionBriefMapper : BaseMapper<EmployeeCalculationException, CalculationExceptionBriefDTOWithAction>
        , IMapper<EmployeeCalculationException, CalculationExceptionBriefDTOWithAction>
    {
        public override CalculationExceptionBriefDTOWithAction MapToModel(EmployeeCalculationException entity)
        {
            var res = new CalculationExceptionBriefDTOWithAction 
            { 
                Id = entity.Id.Id, 
                CalculationId = entity.CalculationId.Id, 
                CalculationPathNo = entity.CalculationPathNo,
                EmployeeNo = entity.EmployeeId.EmployeeNo,
                EmployeeFullName = entity.EmployeeId.EmployeeNo,
            };
            res.ActionCodes = new List<int>
            {
                (int) ActionType.ShowCalculationException,
            };

            return res;
        }
        public override EmployeeCalculationException MapToEntity(CalculationExceptionBriefDTOWithAction model)
        {
            throw new NotImplementedException();
        }
    }


    public class CalculationExceptionMapper : BaseMapper<EmployeeCalculationException, CalculationExceptionDTO>
       , IMapper<EmployeeCalculationException, CalculationExceptionDTO>
    {
        public override CalculationExceptionDTO MapToModel(EmployeeCalculationException entity)
        {
            var res = new CalculationExceptionDTO
            {
                Id = entity.Id.Id,
                CalculationId = entity.CalculationId.Id,
                CalculationPathNo = entity.CalculationPathNo,
                EmployeeNo = entity.EmployeeId.EmployeeNo,
                EmployeeFullName = entity.EmployeeId.EmployeeNo,
                Message = entity.Message
            };
           
            return res;
        }
        public override EmployeeCalculationException MapToEntity(CalculationExceptionDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
