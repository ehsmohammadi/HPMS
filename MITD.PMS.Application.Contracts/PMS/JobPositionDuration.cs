using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobPositions;
using System;

namespace MITD.PMS.Application.Contracts
{
    public class JobPositionDuration
    {

        public DateTime FromDate
        {
            get;
            set;
        }

        public DateTime ToDate
        {
            get;
            set;
        }


        public JobPositionId JobPositionId
        {
            get;
            set;
        }

        public int WorkTimePercent
        {
            get;
            set;
        }

        private  IList<EmployeeJobCustomFieldValue> employeeJobCustomFieldValues = new List<EmployeeJobCustomFieldValue>();
        public  IList<EmployeeJobCustomFieldValue> EmployeeJobCustomFieldValues
        {
            get { return employeeJobCustomFieldValues; }
            set { employeeJobCustomFieldValues = value; }
        }

        public int JobPositionWeight { get; set; }
    }
}
