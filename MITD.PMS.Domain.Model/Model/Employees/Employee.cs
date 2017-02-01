using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;
using MITD.PMS.Domain.Model.Employees;

namespace MITD.PMS.Domain.Model.Employees
{
    public class Employee : EntityWithDbId<long, EmployeeId>, IEntity<Employee>
    {
        #region Fields

        private readonly byte[] rowVersion;

        #endregion

        #region Properties

        public virtual EmployeeId Id
        {
            get { return id; }
        }

        private string firstName;
        public virtual string FirstName
        {
            get { return firstName; }
        }

        private string lastName;
        public virtual string LastName
        {
            get { return lastName; }
        }

        public virtual string FullName
        {
            get { return firstName + " " + LastName; }
        }

        private decimal finalPoint;
        public virtual decimal FinalPoint
        {
            get { return finalPoint; }
        }

        private decimal calculatedPoint;

        public virtual decimal CalculatedPoint
        {
            get { return calculatedPoint; }
        }

        private EmployeePointState employeePointState;
        public virtual EmployeePointState EmployeePointState
        {
            get { return employeePointState; }
            set { employeePointState = value; }
        }

        private IDictionary<SharedEmployeeCustomFieldId, string> customFieldValues = new Dictionary<SharedEmployeeCustomFieldId, string>();
        public virtual IDictionary<SharedEmployeeCustomFieldId, string> CustomFieldValues
        {
            get { return customFieldValues.ToDictionary(c => c.Key, c => c.Value); }
        }

        private IList<EmployeeJobPosition> jobPositions = new List<EmployeeJobPosition>();
        

        public virtual IReadOnlyList<EmployeeJobPosition> JobPositions
        {
            get { return jobPositions.ToList().AsReadOnly(); }
        }

        

        #endregion

        #region Constructors
        protected Employee()
        {

        }

        public Employee(string employeeNo, Period period, string firstName, string lastName)
        {
            if (period == null)
                throw new ArgumentNullException("period");
            period.CheckCreatingEmployee();

            if (string.IsNullOrWhiteSpace(employeeNo))
                throw new EmployeeArgumentException("Employee", "employeeNo");
            id = new EmployeeId(employeeNo, period.Id);

            this.firstName = firstName;
            if (string.IsNullOrWhiteSpace(lastName))
                throw new EmployeeArgumentException("Employee", "lastName");
            this.lastName = lastName;

            employeePointState=EmployeePointState.UnCalculated;
        }

        public Employee(string employeeNo, Period period, string firstName, string lastName, Dictionary<SharedEmployeeCustomField, string> customFieldValueItems)
            : this(employeeNo, period, firstName, lastName)
        {
            if (customFieldValueItems != null)
                assignCustomFieldAndValues(customFieldValueItems);
        }
        #endregion

        #region Public Methods

        public virtual void Update(string firstName, string lastName)
        {
            this.firstName = firstName;
            if (string.IsNullOrWhiteSpace(lastName))
                throw new EmployeeArgumentException("Employee", "LastName");
            this.lastName = lastName;
        }

        #region Final Point

        public virtual void SetFinalPoint(Period period, decimal finalEmployeePoint)
        {
            EmployeePointState.SetPoint(this, period, finalEmployeePoint);
        }

        public virtual void ConfirmAboveMaxEmployeePoint(Period period)
        {
            EmployeePointState.ConfirmAboveMaxEmployeePoint(this,period);
        }

        public virtual void UpdateFinalPoint(decimal point)
        {
            finalPoint = point;
        }
        public virtual void DeleteFinalPoint()
        {
            UpdateFinalPoint(0);
            EmployeePointState=new EmployeePointUnCalculatedState();
        }

        public virtual void ChangeFinalPoint(decimal point,Period period)
        {
            EmployeePointState.ChangeFinalPoint(this, period,point);
        }

        public virtual void ConfirmFinalPoint(Period period)
        {
            EmployeePointState.ConfirmFinalPoint(this, period);
        }

        #endregion

        #region JobPosition
        public virtual void AssignJobPosition(JobPosition jobPosition, DateTime fromDate, DateTime toDate, int workTimePercent, int jobPositionWeight, List<EmployeeJobCustomFieldValue> custumFieldValues)
        {
            var employeeJobPosition = jobPositions.SingleOrDefault(j => j.JobPositionId.Equals(jobPosition.Id));
            if (employeeJobPosition != null)
                jobPositions.Remove(employeeJobPosition);

            jobPositions.Add(new EmployeeJobPosition(this, jobPosition, fromDate, toDate, workTimePercent, jobPositionWeight, custumFieldValues));
        }

        public virtual void AssignJobPositions(IEnumerable<EmployeeJobPosition> employeeJobPositions, IPeriodManagerService periodChecker)
        {
            if (jobPositions == null)
                return;
            periodChecker.CheckModifyingEmployeeJobPositions(this);
            jobPositions.Clear();
            if (employeeJobPositions == null || employeeJobPositions.Count() == 0)
                return;
            if (employeeJobPositions.Sum(ej => ej.WorkTimePercent) != 100)
                throw new EmployeeException((int)ApiExceptionCode.InvalidSumEmployeeWorkTimePercents, ApiExceptionCode.InvalidSumEmployeeWorkTimePercents.DisplayName);

            foreach (var employeeJobPosition in employeeJobPositions)
            {
                if (employeeJobPosition != null)
                    jobPositions.Add(employeeJobPosition);
            }
        }

        #endregion

        #region CustomFields

        public virtual void UpdateCustomFieldsAndValues(Dictionary<SharedEmployeeCustomField, string> customFieldValueItems, IPeriodManagerService periodChecker)
        {
            periodChecker.CheckModifyingEmployeeCustomFieldsAndValues(this);
            foreach (var itm in customFieldValueItems)
            {
                if (!customFieldValues.Select(c => c.Key).Contains(itm.Key.Id))
                    AssignCustomFieldAndValue(itm.Key, itm.Value);
                else
                {
                    UpdateCustomFieldValue(itm.Key, itm.Value);
                }
            }

            var keys = new List<SharedEmployeeCustomFieldId>(customFieldValues.Keys);
            foreach (var key in keys)
            {
                if (!customFieldValueItems.Select(c => c.Key.Id).Contains(key))
                    removeCustomFieldById(key);
            }
        }

        private void assignCustomFieldAndValues(
            Dictionary<SharedEmployeeCustomField, string> customFieldValueItems)
        {
            foreach (var itm in customFieldValueItems)
            {
                AssignCustomFieldAndValue(itm.Key, itm.Value);
            }

        }

        public virtual void AssignCustomFieldAndValue(SharedEmployeeCustomField customField, string value)
        {
            customFieldValues.Add(customField.Id, value);
        }

        public virtual void RemoveCustomField(SharedEmployeeCustomField customField)
        {
            removeCustomFieldById(customField.Id);
        }

        private void removeCustomFieldById(SharedEmployeeCustomFieldId id)
        {
            customFieldValues.Remove(id);
        }

        public virtual void UpdateCustomFieldValue(SharedEmployeeCustomField customField, string value)
        {
            RemoveCustomField(customField);
            AssignCustomFieldAndValue(customField, value);
        }
        #endregion

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(Employee other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Employee)obj;
            return SameIdentityAs(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        #endregion

        
    }
}
