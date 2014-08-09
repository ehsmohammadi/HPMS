using System;
using System.Linq;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Domain.Service;
using MITD.PMS.Domain.Model.Employees;
using System.Collections.Generic;
using MITD.Core.RuleEngine.Model;

namespace MITD.PMS.Domain.Model.Calculations
{


    public class Calculation : IEntity<Calculation>
    {
        public class Progress
        {
            private readonly long totalEmployeesCount;
            private readonly long employeesCalculatedCount;
            private IList<string> messages = new List<string>();
            private readonly string messagesDelimited;
            private readonly int? lastCalculatedPath;
            private readonly EmployeeId lastCalculatedEmployeeId;



            protected Progress()
            {

            }
            public Progress(long totalEmployeesCount, long employeesCalculatedCount, IList<string> messages, int? lastCalculatedPath, Employee lastCalculatinEmployee)
            {
                this.totalEmployeesCount = totalEmployeesCount;
                this.employeesCalculatedCount = employeesCalculatedCount;
                this.messages = messages;
                this.messagesDelimited = string.Join("|", messages);
                this.lastCalculatedPath = lastCalculatedPath;
                if (lastCalculatinEmployee != null)
                    this.lastCalculatedEmployeeId = lastCalculatinEmployee.Id;
            }
            public Progress(long totalEmployeesCount, long employeesCalculatedCount, IList<string> messages)
                : this(totalEmployeesCount, employeesCalculatedCount, messages, null, null)
            {
            }

            public long TotalEmployeesCount { get { return totalEmployeesCount; } }
            public long EmployeesCalculatedCount { get { return employeesCalculatedCount; } }
            public int? LastCalculatedPath { get { return lastCalculatedPath; } }
            public EmployeeId LastCalculatedEmployeeId { get { return lastCalculatedEmployeeId; } }

            public IReadOnlyList<string> Messages
            {
                get
                {
                    if ((messages.Count() == 0) && !string.IsNullOrEmpty(messagesDelimited))
                    {
                        messages = messagesDelimited.Split(new[] { "|" }, StringSplitOptions.None).ToList();
                    }
                    return messages.ToList();
                }
            }
            public int Percent
            {
                get { return totalEmployeesCount == 0 ? 0 : Convert.ToInt32((employeesCalculatedCount * 100) / totalEmployeesCount); }
            }
        }

        #region Fields
        private readonly CalculationId id;
        private readonly byte[] rowVersion;
        private readonly DateTime creationDate;
        private readonly string name;
        private readonly DateTime? startRunTime;
        private readonly DateTime? endRunTime;
        private readonly string employeeIdDelimetedList;
        private readonly Lazy<IReadOnlyList<EmployeeId>> employeeIdList;
        private readonly Periods.PeriodId periodId;
        private readonly Policies.PolicyId policyId;
        private CalculationState state;
        private string libraryText;
        private IReadOnlyDictionary<string, RuleBase> rules;
        private string serializedRules;
        private Progress calculationResult;
        private bool isDeterministic = false;

        //private IList<EmployeeCalculationException> employeeCalculationExceptions = new List<EmployeeCalculationException>();
        //public virtual IReadOnlyList<EmployeeCalculationException> EmployeeCalculationExceptions
        //{
        //    get { return employeeCalculation ons.ToList().AsReadOnly(); }
        //}

        #endregion

        #region Properties


        public virtual CalculationId Id
        {
            get { return id; }
        }

        public virtual bool IsDeterministic
        {
            get { return isDeterministic; }
        }

        public virtual CalculationState State
        {
            get { return state; }
            protected internal set { state = value; }
        }

        public virtual string Name { get { return name; } }
        public virtual DateTime? StartRunTime { get { return startRunTime; } }
        public virtual DateTime? EndRunTime { get { return endRunTime; } }
        public virtual int EmployeeCount { get { return employeeIdList.Value.Count(); } }
        public virtual PeriodId PeriodId { get { return periodId; } }
        public virtual PolicyId PolicyId { get { return policyId; } }
        public virtual IReadOnlyList<EmployeeId> EmployeeIdList { get { return employeeIdList.Value; } }
        public virtual string LibraryText { get { return libraryText; } }
        public virtual IReadOnlyDictionary<string, RuleBase> Rules
        {
            get
            {
                if (rules == null && !string.IsNullOrEmpty(serializedRules))
                    rules = MITD.Services.ByteArraySerializer.ToObject<Dictionary<string, RuleBase>>(serializedRules);
                return rules;
            }
        }
        public virtual Progress CalculationResult { get { return calculationResult; } }

        #endregion

        #region Constructors
        protected Calculation()
        {
            //For OR mapper
            employeeIdList = new Lazy<IReadOnlyList<EmployeeId>>(() =>
            {
                if (string.IsNullOrEmpty(employeeIdDelimetedList))
                    return new List<EmployeeId>().AsReadOnly();
                return employeeIdDelimetedList.Split(new[] { ";" }, StringSplitOptions.None).Select(i =>
                    {
                        return new EmployeeId(i, periodId);
                    }).ToList();
            });
        }

        public Calculation(CalculationId calculationId,
                           Period period,
                           Policy policy,
                           string name,
                           DateTime creationDate,
                           string employeeIdList)
            : this()
        {
            if (calculationId == null)
                throw new ArgumentNullException("calculationId");
            if (period == null)
                throw new ArgumentNullException("period");
            period.CheckCreatingCalculation();
            if (policy == null)
                throw new ArgumentNullException("policy");

            this.id = calculationId;
            this.periodId = period.Id;
            this.policyId = policy.Id;
            this.creationDate = creationDate;

            if (string.IsNullOrWhiteSpace(name))
                throw new CalculationArgumentException("Calculation","Name");
            this.name = name;
            this.employeeIdDelimetedList = employeeIdList;
            state = new CalculationInitState();
            isDeterministic = false;
        }

        #endregion

        #region Public Methods

        public virtual void ChangeDeterministicStatus(bool isDeterministic, ICalculationRepository calcRep, Period period)
        {
            period.CheckChangeCalculationDeterministicStatus();
            State.ChangeDeterministicStatus(this, isDeterministic, calcRep);
        }

        protected internal virtual void SetDeterministic()
        {
            isDeterministic = true;
        }

        protected internal virtual void UnsetDeterministic()
        {
            isDeterministic = false;
        }

        public virtual void Run(IJobIndexPointCalculator calculator)
        {
            State.Run(this, calculator);
        }

        public virtual void Pause(IJobIndexPointCalculator calculator)
        {
            State.Pause(this, calculator);
        }

        public virtual void Stop(IJobIndexPointCalculator calculator)
        {
            State.Stop(this, calculator);
        }

        public virtual void Finish(IJobIndexPointCalculator calculator)
        {
            state.Finished(this, calculator);
        }

        //public virtual void SetDeter(IJobIndexPointCalculator calculator)
        //{
        //    state.Finished(this, calculator);
        //}

        public virtual void UpdateCompileResult(string libraryText, Dictionary<string, Core.RuleEngine.Model.RuleBase> rules)
        {
            this.rules = rules;
            this.libraryText = libraryText;
            this.serializedRules = MITD.Services.ByteArraySerializer.ToString(rules);
        }

        public virtual void UpdateCalculationResult(long totalEmployees, long employeesCalculated, List<String> messages)
        {
            this.calculationResult = new Progress(totalEmployees, employeesCalculated, messages);
        }

        public virtual void UpdateCalculationResult(long totalEmployees, long employeesCalculated, List<String> messages, int lastCalculatedPath, Employee lastCalculatedEmployee)
        {
            this.calculationResult = new Progress(totalEmployees, employeesCalculated, messages, lastCalculatedPath, lastCalculatedEmployee);
        }

        
        //public virtual void AddCalculationException(Employee employee,int pathNo,string message)
        //{
        //     employeeCalculationExceptions.Add(new EmployeeCalculationException(this,employee.Id,pathNo,message));
        //}

        //public virtual void UpdateCalculationException(List<EmployeeCalculationException> calculationExceptions)
        //{
        //    employeeCalculationExceptions.Clear();
        //    foreach (var itm in calculationExceptions)
        //    {
        //        employeeCalculationExceptions.Add(itm);
        //    }
        //}



        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(Calculation other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Calculation)obj;
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
