using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class EmployeeDTOWithActionMapper : BaseMapper<Employee, EmployeeDTOWithActions>, IMapper<Employee, EmployeeDTOWithActions>
    {

        public override EmployeeDTOWithActions MapToModel(Employee entity)
        {
            var res = new EmployeeDTOWithActions
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PeriodId = entity.Id.PeriodId.Id,
                PersonnelNo = entity.Id.EmployeeNo,
                FinalPoint = entity.FinalPoint,
                StateName = entity.EmployeePointState.Description,
                
                ActionCodes = new List<int>
                {
                    (int) ActionType.AddEmployee,
                    (int) ActionType.DeleteEmployee,
                    (int)ActionType.ModifyEmployee,
                    (int)ActionType.ManageEmployeeJobPositions,
                    (int)ActionType.ConfirmAboveMaxPoint,
                    (int)ActionType.ChangeEmployeePoint,
                    

                }
            };
            return res;

        }

        public override Employee MapToEntity(EmployeeDTOWithActions model)
        {
            throw new InvalidOperationException();
        }

    }

}
