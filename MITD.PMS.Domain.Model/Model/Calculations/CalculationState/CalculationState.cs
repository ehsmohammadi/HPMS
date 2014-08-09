using System;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Claims;
using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Calculations
{
    public abstract class CalculationState:Enumeration, IValueObject<CalculationState>,ICalculationState
    {
        public static readonly CalculationState Init = new CalculationInitState();
        public static readonly CalculationState Running = new CalculationRunningState();
        public static readonly CalculationState Paused = new CalculationPausedState();
        public static readonly CalculationState Stopped = new CalculationStoppedState();
        public static readonly CalculationState Completed = new CalculationCompletedState();
        public static readonly CalculationState NotCompleted = new CalculationNotCompletedState();

        protected CalculationState(string value, string name)
            : base(value, name)
        {
        }

        public bool SameValueAs(CalculationState other)
        {
            return Equals(other);
        }
        public static bool operator ==(CalculationState left, CalculationState right)
        {
            return object.Equals(left, right);
        }
        public static bool operator !=(CalculationState left, CalculationState right)
        {
            return !(left == right);
        }
        internal virtual void Run(Calculation calculation, IJobIndexPointCalculator calculator)
        {
            throw new ClaimInvalidStateOperationException("Calculation", DisplayName, "Run");
        }
        internal virtual void Pause(Calculation calculation, IJobIndexPointCalculator calculator)
        {
            throw new ClaimInvalidStateOperationException("Calculation", DisplayName, "Pause");
        }
        internal virtual void Stop(Calculation calculation, Service.IJobIndexPointCalculator calculator)
        {
            throw new ClaimInvalidStateOperationException("Calculation", DisplayName, "Stop");
        }
        internal virtual void Finished(Calculation calculation, Service.IJobIndexPointCalculator calculator)
        {
            throw new ClaimInvalidStateOperationException("Calculation", DisplayName, "Finished");
        }
        internal virtual void ReCalculate(Calculation calculation, Service.IJobIndexPointCalculator calculator)
        {
            throw new ClaimInvalidStateOperationException("Calculation", DisplayName, "ReCalculate");
        }

        internal virtual void ChangeDeterministicStatus(Calculation calculation, bool isDeterministic, ICalculationRepository calcRep)
        {
            throw new ClaimInvalidStateOperationException("Calculation", DisplayName, "ChangeDeterministicStatus");
        }
    }
}
