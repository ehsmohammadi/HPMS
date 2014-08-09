using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class CalculationExceptionDTO 
    {
        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }

        private long calculationId;
        public long CalculationId
        {
            get { return calculationId; }
            set { this.SetField(p => p.CalculationId, ref calculationId, value); }
        }

        private string employeeFullName;
        public string EmployeeFullName
        {
            get { return employeeFullName; }
            set { this.SetField(p => p.EmployeeFullName, ref employeeFullName, value); }
        }

        private string employeeNo;
        public string EmployeeNo
        {
            get { return employeeNo; }
            set { this.SetField(p => p.EmployeeNo, ref employeeNo, value); }
        }

        private int calculationPathNo;
        public int CalculationPathNo
        {
            get { return calculationPathNo; }
            set { this.SetField(p => p.CalculationPathNo, ref calculationPathNo, value); }
        }


        private string message;
        public string Message
        {
            get { return message; }
            set { this.SetField(p => p.Message, ref message, value); }
        }
    }
}
