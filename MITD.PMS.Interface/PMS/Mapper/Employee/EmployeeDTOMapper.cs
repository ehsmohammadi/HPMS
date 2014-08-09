using System;
using MITD.Core;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    public class EmployeeDTOMapper : BaseMapper<Employee, EmployeeDTO>, IMapper<Employee, EmployeeDTO>
    {

        public override EmployeeDTO MapToModel(Employee entity)
        {
            var res = new EmployeeDTO
                {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    PeriodId = entity.Id.PeriodId.Id,
                    PersonnelNo = entity.Id.EmployeeNo                    
                };
            return res;

        }

        public override Employee MapToEntity(EmployeeDTO model)
        {
            throw new InvalidOperationException();

        }

    }

}
