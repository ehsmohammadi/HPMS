using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Claims;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Exceptions;
using MITD.PMS.Domain.Model.Claims;

namespace MITD.PMS.Domain.Service
{
    public static class ClaimControlService 
    {
       

        public static void CheckDeleteClaim(Claim claim, Employee employee)
        {
            if(employee==null)
                throw new ClaimException((int)ApiExceptionCode.CouldNotDeleteClaimByAnotherUser, ApiExceptionCode.CouldNotDeleteClaimByAnotherUser.DisplayName);

            if (claim.State != ClaimState.Canceled)
                throw new ClaimInvalidStateOperationException("Claim", "DeleteClaim");

            if (claim.EmployeeNo != employee.Id.EmployeeNo)
                throw new ClaimException((int)ApiExceptionCode.CouldNotDeleteClaimByAnotherUser, ApiExceptionCode.CouldNotDeleteClaimByAnotherUser.DisplayName);
        }
    }
}
