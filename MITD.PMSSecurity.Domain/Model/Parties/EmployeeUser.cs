using System.Collections.Generic;

namespace MITD.PMSSecurity.Domain
{
    public class EmployeeUser :User
    {
        private readonly string employeeNo;
        public string EmployeeNo { get { return employeeNo; } }

        public override List<ActionType> Actions
        {
            get
            {
                return new List<ActionType>
                {
                    //ActionType.FillInquiryForm,
                    //ActionType.FillInquiryUnitForm,
                    //ActionType.ShowEmployeeInquiry,
                    //ActionType.ShowCalculationResult
                };
            }
        }

        public EmployeeUser(PartyId userId ,string employeeNo,string fname,string lname, string email)
            : base(userId, fname, lname, email)
        {
            this.employeeNo = employeeNo;
        }

    }
}