using MITD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IJobIndexPointFacadeService : IFacadeService
    {
        PageResultDTO<JobIndexPointSummaryDTOWithAction> GetAllJobIndexPoints(long periodId, long calculationId, int pageSize, int pageIndex);
        List<JobPositionValueDTO> GetEmployeeJobPositionsCalculationResult(long periodId, long calculationId, string employeeNo);
        JobIndexPointSummaryDTOWithAction GetEmployeeSummaryCalculationResult(long periodId, long calculationId, string employeeNo);
    }
}
