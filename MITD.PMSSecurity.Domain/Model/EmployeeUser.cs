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
                    //ActionType.GetEmployeeJobPositions,
                    //ActionType.ShowCalculationResultDetail,
                    ActionType.FillInquiryForm,                   
                    ActionType.AddClaim,
                    ActionType.ShowClaim,
                    ActionType.DeleteClaim,
                    ActionType.CancelClaim,
                    ActionType.AddPermittedUserToMyTasks,
                    ActionType.RemovePermittedUserFromMyTasks,
                    ActionType.SettingPermittedUserToMyTasks,
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