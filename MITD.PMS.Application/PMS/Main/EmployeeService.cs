using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using System.Transactions;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Exceptions;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMS.Application
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeRepository employeeRep;
        private readonly IPeriodRepository periodRep;
        private readonly IPMSAdminService converter;
        private readonly IJobPositionRepository jobPositionRep;
        private readonly IPeriodManagerService periodChecker;
        private readonly IJobIndexPointRepository jobIndexPointRepository;
        private readonly ICalculationRepository calculationRepository;

        public EmployeeService(IEmployeeRepository employeeRep, IPeriodRepository periodRep, IPMSAdminService converter,
            IJobPositionRepository jobPositionRep, IPeriodManagerService periodChecker, IJobIndexPointRepository jobIndexPointRepository,ICalculationRepository calculationRepository)
        {
            this.employeeRep = employeeRep;
            this.periodRep = periodRep;
            this.converter = converter;
            this.jobPositionRep = jobPositionRep;
            this.periodChecker = periodChecker;
            this.jobIndexPointRepository = jobIndexPointRepository;
            this.calculationRepository = calculationRepository;
        }

        public void Delete(EmployeeId employeeId)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var employee = employeeRep.GetBy(employeeId);
                    employeeRep.Delete(employee);
                    tr.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = employeeRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Employee GetBy(EmployeeId employeeId)
        {
            using (var tr = new TransactionScope())
            {
                var res = employeeRep.GetBy(employeeId);
                tr.Complete();
                return res;
            }
        }

        public Employee Add(PeriodId periodId, string personnelNo, string firstName, string lastName, Dictionary<SharedEmployeeCustomFieldId, string> customFieldIds)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var period = periodRep.GetById(periodId);
                    var employeeCustomFields = converter.GetSharedEmployeeCustomField(customFieldIds.Keys.ToList());
                    var employee = new Employee(personnelNo, period, firstName, lastName,
                                                employeeCustomFields.ToDictionary(e => e, e => customFieldIds[e.Id]));
                    employeeRep.Add(employee);
                    tr.Complete();
                    return employee;
                }
            }
            catch (Exception exp)
            {
                var res = employeeRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Employee Update(EmployeeId employeeId, string firstName, string lastName, Dictionary<SharedEmployeeCustomFieldId, string> customFieldIdValueList)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var employee = employeeRep.GetBy(employeeId);
                    employee.Update(firstName, lastName);
                    var employeeCustomFields =
                        converter.GetSharedEmployeeCustomField(customFieldIdValueList.Keys.ToList());
                    employee.UpdateCustomFieldsAndValues(
                        employeeCustomFields.ToDictionary(e => e, e => customFieldIdValueList[e.Id]), periodChecker);
                    tr.Complete();
                    return employee;
                }
            }
            catch (Exception exp)
            {
                var res = employeeRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Employee AssignJobPositions(EmployeeId employeeId, IEnumerable<JobPositionDuration> jobPositionDurations)
        {
            using (var tr = new TransactionScope())
            {
                var employee = employeeRep.GetBy(employeeId);
                var employeeJobPositions = new List<EmployeeJobPosition>();
                foreach (var jobPositionDuration in jobPositionDurations)
                {
                    var jobPosition = jobPositionRep.GetBy(jobPositionDuration.JobPositionId);
                    employeeJobPositions.Add(new EmployeeJobPosition(employee, jobPosition, jobPositionDuration.FromDate, jobPositionDuration.ToDate, jobPositionDuration.WorkTimePercent, jobPositionDuration.JobPositionWeight, jobPositionDuration.EmployeeJobCustomFieldValues.ToList()));
                }
                employee.AssignJobPositions(employeeJobPositions, periodChecker);
                tr.Complete();
                return employee;
            }

        }

        public Employee UpdateFinalPoint(EmployeeId employeeId)
        {
            using (var tr = new TransactionScope())
            {
                var employee = employeeRep.GetBy(employeeId);
                var period = periodRep.GetById(employeeId.PeriodId);
                var deterministicCalculation = calculationRepository.GetDeterministicCalculation(period);
                if(deterministicCalculation==null)
                    throw new CalculationArgumentException("Calculation", "IsDeterministic");
                var finalEmployeePoint = jobIndexPointRepository.GetEmployeeFinalPointBy(employeeId.PeriodId, employeeId.EmployeeNo,deterministicCalculation.Id);
                employee.SetFinalPoint(period, finalEmployeePoint);
                tr.Complete();
                return employee;
            }
        }

        public void DeleteFinalPoint(EmployeeId employeeId)
        {
            using (var tr = new TransactionScope())
            {
                var employee = employeeRep.GetBy(employeeId);
                employee.DeleteFinalPoint();
                tr.Complete();

            }
        }

        public void ConfirmAboveMaxEmployeePoint(EmployeeId employeeId)
        {
            using (var tr = new TransactionScope())
            {
                var employee = employeeRep.GetBy(employeeId);
                var period = periodRep.GetById(employeeId.PeriodId);
                employee.ConfirmAboveMaxEmployeePoint(period);
                tr.Complete();

            }
        }

        public void ChangeEmployeePoint(EmployeeId employeeId, decimal point)
        {
            using (var tr = new TransactionScope())
            {
                var employee = employeeRep.GetBy(employeeId);
                var period = periodRep.GetById(employeeId.PeriodId);
                employee.ChangeFinalPoint(point, period);
                tr.Complete();

            }
        }

        public void ConfirmFinalPoint(EmployeeId employeeId)
        {
            using (var tr = new TransactionScope())
            {
                var employee = employeeRep.GetBy(employeeId);
                var period = periodRep.GetById(employeeId.PeriodId);
                employee.ConfirmFinalPoint(period);
                tr.Complete();

            }

        }

        public IEnumerable<string> GetAllEmployeeNo(PeriodId periodId)
        {
            return employeeRep.GetAllEmployeeNo(j => true);
        }


    }


}
