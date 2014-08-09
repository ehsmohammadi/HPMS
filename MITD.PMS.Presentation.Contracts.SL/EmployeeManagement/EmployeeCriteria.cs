using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public class EmployeeCriteria : ViewModelBase
    {
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                this.SetField(p => p.FirstName, ref firstName, value);
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                this.SetField(p => p.LastName, ref lastName, value);
            }
        }

        private string employeeNo;
        public string EmployeeNo
        {
            get { return employeeNo; }
            set
            {
                this.SetField(p => p.EmployeeNo, ref employeeNo, value);
            }
        }

        private long unitId;
        public long UnitId
        {
            get { return unitId; }
            set
            {
                this.SetField(p => p.UnitId, ref unitId, value);
            }
        }

        private long jobPositionId;
        public long JobPositionId
        {
            get { return jobPositionId; }
            set
            {
                this.SetField(p => p.JobPositionId, ref jobPositionId, value);
            }
        }


    }
}
