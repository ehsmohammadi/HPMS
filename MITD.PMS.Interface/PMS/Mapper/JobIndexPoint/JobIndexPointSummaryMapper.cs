using MITD.Core;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.Policies;
using MITD.PMSReport.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Interface
{
    public class JobIndexPointSummaryMapper : BaseMapper<JobIndexPointWithEmployee, JobIndexPointSummaryDTOWithAction>
        , IMapper<JobIndexPointWithEmployee, JobIndexPointSummaryDTOWithAction>
    {
        public override JobIndexPointSummaryDTOWithAction MapToModel(JobIndexPointWithEmployee entity)
        {
            var res = new JobIndexPointSummaryDTOWithAction 
            { 
                EmployeeNo = entity.Employee.Id.EmployeeNo,
                EmployeeName = entity.Employee.FullName,
                TotalScore =   entity.JobIndexPoint.Value
            };

            return res;
        }
        public override JobIndexPointWithEmployee MapToEntity(JobIndexPointSummaryDTOWithAction model)
        {
            throw new NotImplementedException();
        }
    }
}
