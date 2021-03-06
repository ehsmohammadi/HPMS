﻿using System;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Employees
{
    public abstract class EmployeePointState : Enumeration, IValueObject<EmployeePointState>
    {
        #region Properties

        public static readonly EmployeePointState UnCalculated = new EmployeePointUnCalculatedState();
        public static readonly EmployeePointState CalculatedWithNormalPoint = new EmployeePointCalculatedWithNormalPointState();
        public static readonly EmployeePointState CalculatedWithAboveMaxPoint = new EmployeePointCalculatedWithAboveMaxPointState();
        public static readonly EmployeePointState ConfirmedWithAboveMaxPoint = new EmployeePointConfirmedWithAboveMaxPointState();
        public static readonly EmployeePointState ConfirmedWithMaxPoint = new EmployeePointConfirmedWithMaxPointState();
        public static readonly EmployeePointState ConfirmedWithNormalPoint = new EmployeePointConfirmedWithNormalPointState();

        private readonly string description;
        public virtual string Description
        {
            get { return description; }
        }

        #endregion

        #region Constructors

        protected EmployeePointState(string value, string name)
            : base(value, name)
        {

        }

        public EmployeePointState(string value, string displayName, string description)
            : base(value, displayName)
        {
            this.description = description;
        }

        #endregion

        #region IValueObject member

        public bool SameValueAs(EmployeePointState other)
        {
            return Equals(other);
        }
        public static bool operator ==(EmployeePointState left, EmployeePointState right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(EmployeePointState left, EmployeePointState right)
        {
            return !(left == right);
        }

        public static explicit operator int(EmployeePointState x)
        {
            if (x == null)
            {
                throw new InvalidCastException();

            }

            return Convert.ToInt32(x.Value);

        }

        #endregion

        #region State methods

        public virtual void SetPoint(Employee employee, Period period, decimal point)
        {
            throw new EmployeeInvalidStateOperationException("Employee", DisplayName, "SetPoint");
        }

        public virtual void ConfirmAboveMaxEmployeePoint(Employee employee, Period period)
        {
            throw new EmployeeInvalidStateOperationException("Employee", DisplayName, "ConfirmAboveMaxEmployeePoint");
        }

        public virtual void ChangeFinalPoint(Employee employee, Period period, decimal point)
        {
            throw new EmployeeInvalidStateOperationException("Employee", DisplayName, "ChangeFinalPoint");
        }

        public virtual void Rollback(Employee employee,Period period)
        {
            throw new EmployeeInvalidStateOperationException("Employee", DisplayName, "Rollback");
        }

        public virtual void ConfirmFinalPoint(Employee employee, Period period)
        {
            throw new EmployeeInvalidStateOperationException("Employee", DisplayName, "ConfirmFinalPoint");
        }

        #endregion


    }
}
