using System;
using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using MITD.PMS.RuleContracts;
using MITD.PMS.Domain.Model.Calculations;
using MITD.Core;
using Employee = MITD.PMS.Domain.Model.Employees.Employee;

namespace MITD.PMS.Domain.Model.Policies
{
    public abstract class Policy : IEntity<Policy>, IDisposable
    {
        #region Fields
        private readonly PolicyId id;
        
        private readonly byte[] rowVersion;

        protected IPolicyEngineService PolicyEngine;
        private string name;
        private string dictionaryName;
        #endregion

        #region Properties

        public virtual PolicyId Id
        {
            get { return id; }
        }

        public virtual string Name
        {
            get { return name; }
        }

        public virtual string DictionaryName
        {
            get { return dictionaryName; }
        }        
        #endregion

        #region Constructors
        protected Policy()
        {

        }
        #endregion
        
        #region Public Methods
        public virtual CalculationPointPersistanceHolder CalculateFor(DateTime startDate, Employee employee, Period period, Calculation calculation, ICalculationDataProvider calculationDataProvider, IEventPublisher publisher, CalculatorSession calculationSession)
        {
            if (!PolicyEngine.HasBeenSetup)
                PolicyEngine.SetupForCalculation(this, publisher);
            var res = PolicyEngine.CalculateFor(employee, period, calculation, calculationDataProvider, calculationSession);
            return res;
        }
        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(Policy other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Policy)obj;
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



        ~Policy()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (PolicyEngine != null)
                    PolicyEngine.Dispose();
            }

        }
        
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
