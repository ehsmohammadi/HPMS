using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Model;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
    //  [Interceptor(typeof(Interception))]
    public class EmployeeServiceFacade : IEmployeeServiceFacade
    {
        private readonly IEmployeeRepository employeeRep;
        private readonly IMapper<Employee, EmployeeDTOWithActions> employeeDTOWithActionsMapper;
        private readonly IMapper<Employee, EmployeeDTO> employeeDTOMapper;
        private readonly IEmployeeService employeeService;
        private readonly IPeriodRepository periodRep;
        private readonly IJobPositionRepository jobPositionRep;
        private readonly IJobRepository jobRep;
        private readonly IPMSAdminService converter;

        public EmployeeServiceFacade(IEmployeeRepository employeeRep,
            IMapper<Employee, EmployeeDTOWithActions> employeeDTOWithActionsMapper,
            IMapper<Employee, EmployeeDTO> employeeDTOMapper,
            IEmployeeService employeeService,
            IPeriodRepository periodRep,
            IJobPositionRepository jobPositionRep,
            IJobRepository jobRep,
            IPMSAdminService converter)
        {
            this.employeeRep = employeeRep;
            this.employeeDTOWithActionsMapper = employeeDTOWithActionsMapper;
            this.employeeDTOMapper = employeeDTOMapper;
            this.employeeService = employeeService;
            this.periodRep = periodRep;
            this.jobPositionRep = jobPositionRep;
            this.jobRep = jobRep;
            this.converter = converter;
        }

        [RequiredPermission(ActionType.ShowEmployees)]
        public PageResultDTO<EmployeeDTOWithActions> GetAllEmployees(long periodId, int pageSize, int pageIndex)
        {
            var fs = new ListFetchStrategy<Employee>(Enums.FetchInUnitOfWorkOption.NoTracking);
            fs.WithPaging(pageSize, pageIndex);
            fs.OrderBy(e => e.Id);
            employeeRep.Find(e => e.Id.PeriodId == new PeriodId(periodId), fs);
            var res = new PageResultDTO<EmployeeDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result =
                fs.PageCriteria.PageResult.Result.Select(p => employeeDTOWithActionsMapper.MapToModel(p)).ToList();
            return res;
        }

        [RequiredPermission(ActionType.ShowEmployees)]
        public PageResultDTO<EmployeeDTOWithActions> GetAllEmployees(long periodId, int pageSize, int pageIndex,
            string filter)
        {
            var fs = new ListFetchStrategy<Employee>(Enums.FetchInUnitOfWorkOption.NoTracking);
            fs.WithPaging(pageSize, pageIndex);
            fs.OrderBy(e => e.Id);
            //todo:(LOW) Must be code in better way
            var criterias = filter.Split(';');
            var predicate = getEmployeePredicate(criterias, periodId);
            employeeRep.Find(predicate, fs);
            var res = new PageResultDTO<EmployeeDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result =
                fs.PageCriteria.PageResult.Result.Select(p => employeeDTOWithActionsMapper.MapToModel(p)).ToList();
            return res;
        }

        [RequiredPermission(ActionType.ShowEmployees)]
        public List<string> GetAllEmployeeNo(long periodId, string filter)
        {
            var criterias = filter.Split(';');
            var predicate = getEmployeePredicate(criterias, periodId);
            return employeeRep.GetAllEmployeeNo(predicate);
        }

        private Expression<Func<Employee, bool>> getEmployeePredicate(IEnumerable<string> criterias, long periodId)
        {
            Expression<Func<Employee, bool>> res = e => e.Id.PeriodId == new PeriodId(periodId);

            foreach (var criteria in criterias)
            {
                var sp = criteria.Split(':');
                if (sp[0] == "FirstName")
                {
                    res = res.And(e => e.FirstName.Contains(sp[1]));
                }
                if (sp[0] == "LastName")
                {
                    res = res.And(e => e.LastName.Contains(sp[1]));
                }
                if (sp[0] == "EmployeeNo")
                {
                    res = res.And(e => e.Id.EmployeeNo.Contains(sp[1]));
                }
            }
            return res;
        }

        [RequiredPermission(ActionType.ShowEmployees)]
        public List<EmployeeDTO> GetAllEmployees(long periodId)
        {
            return employeeRep.Find(e => e.Id.PeriodId == new PeriodId(periodId)).Select(p => employeeDTOMapper.MapToModel(p)).ToList();
        }

        [RequiredPermission(ActionType.DeleteEmployee)]
        public string DeleteEmployee(long periodId, string personnelNo)
        {
            employeeService.Delete(new EmployeeId(personnelNo, new PeriodId(periodId)));
            return "Employee with PersonnelCode: " + personnelNo + " deleted";
        }

        public EmployeeDTO GetEmployee(long periodId, string employeeNo)
        {
            var employee = employeeService.GetBy(new EmployeeId(employeeNo, new PeriodId(periodId)));
            var employeeDTO = employeeDTOMapper.MapToModel(employee);
            var employeeCustomFields = converter.GetSharedEmployeeCustomField(employee.CustomFieldValues.Keys.ToList());
            employeeDTO.CustomFields =
                employeeCustomFields.Select(
                    ec =>
                        new CustomFieldValueDTO
                        {
                            Id = ec.Id.Id,
                            IsReadOnly = false,
                            Name = ec.Name,
                            Value = employee.CustomFieldValues[ec.Id]
                        }).ToList();
            return employeeDTO;

        }

        [RequiredPermission(ActionType.AddEmployee)]
        public EmployeeDTO AddEmployee(long periodId, EmployeeDTO dto)
        {
            var employee = employeeService.Add(new PeriodId(periodId), dto.PersonnelNo, dto.FirstName, dto.LastName,
                dto.CustomFields.ToDictionary(s => new SharedEmployeeCustomFieldId(s.Id), s => s.Value));
            return employeeDTOMapper.MapToModel(employee);
        }

        [RequiredPermission(ActionType.ModifyEmployee)]
        public EmployeeDTO UpdateEmployee(long periodId, EmployeeDTO dto)
        {
            var employee = employeeService.Update(new EmployeeId(dto.PersonnelNo, new PeriodId(periodId)), dto.FirstName,
                dto.LastName, dto.CustomFields.ToDictionary(s => new SharedEmployeeCustomFieldId(s.Id), s => s.Value));
            return employeeDTOMapper.MapToModel(employee);
        }

        public EmployeeJobPositionsDTO GetEmployeeJobPositions(string employeeNo, long periodId)
        {
            var employee = employeeRep.GetBy(new EmployeeId(employeeNo, new PeriodId(periodId)));
            return mapToEmployeeJobPositionDTO(employee);
        }

        [RequiredPermission(ActionType.ManageEmployeeJobPositions)]
        public EmployeeJobPositionsDTO AssignJobPositionsToEmployee(long periodId, string employeeNo,
            EmployeeJobPositionsDTO employeeJobPositions)
        {
            var employee = employeeService.AssignJobPositions(new EmployeeId(employeeNo, new PeriodId(periodId)),
                employeeJobPositions.EmployeeJobPositionAssignmentList.Select(
                    jp => GetJobPositionDurationWithCustomFields(periodId,jp)));
            return mapToEmployeeJobPositionDTO(employee);
        }

        private JobPositionDuration GetJobPositionDurationWithCustomFields(long periodId,EmployeeJobPositionAssignmentDTO jp)
        {
            var jobPositionInPeriod = jobPositionRep.GetBy(new JobPositionId(new PeriodId(periodId), new SharedJobPositionId(jp.JobPositionId)));
            var job = jobRep.GetById(jobPositionInPeriod.JobId);

             var employeeJobCustomFieldValueList = job.CustomFields.Select(r =>
                new EmployeeJobCustomFieldValue(r.Id
                    ,jp.CustomFieldValueList.Single(j => j.Id == r.SharedJobCustomField.Id.Id).Value)).ToList();
                
            var jpDuration = new JobPositionDuration
                {
                    FromDate = jp.FromDate,
                    ToDate = jp.ToDate,
                    JobPositionId = new JobPositionId(new PeriodId(periodId), new SharedJobPositionId(jp.JobPositionId)),
                    WorkTimePercent = jp.WorkTimePercent,
                    JobPositionWeight=jp.JobPositionWeight,
                    EmployeeJobCustomFieldValues = employeeJobCustomFieldValueList
                };
            return jpDuration;
        }

        private EmployeeJobPositionsDTO mapToEmployeeJobPositionDTO(Employee employee)
        {
            //todo:(LOW)Must convert to Mapper
            var employeeJobPosition = new EmployeeJobPositionsDTO
            {
                EmployeeNo = employee.Id.EmployeeNo,
                PeriodId = employee.Id.PeriodId.Id
            };
            employeeJobPosition.EmployeeJobPositionAssignmentList = new List<EmployeeJobPositionAssignmentDTO>();
            foreach (var itm in employee.JobPositions)
            {
                var employeeJobPositionAssignmentDTO = new EmployeeJobPositionAssignmentDTO
                {
                    JobPositionName = converter.GetSharedJobPosition(itm.JobPositionId.SharedJobPositionId).Name,
                    JobPositionId = itm.JobPositionId.SharedJobPositionId.Id,
                    FromDate = itm.FromDate,
                    ToDate = itm.ToDate,
                    WorkTimePercent = itm.WorkTimePercent,
                    JobPositionWeight = itm.JobPositionWeight,
                    ActionCodes = new List<int>
                    {
                        (int) ActionType.ModifyEmployeeJobCustomFields,

                    }

                };
                employeeJobPositionAssignmentDTO.CustomFieldValueList = new List<CustomFieldValueDTO>();
                foreach (var employeeJobCustomFieldValue in itm.EmployeeJobCustomFieldValues)
                {
                    employeeJobPositionAssignmentDTO.CustomFieldValueList.Add(new CustomFieldValueDTO
                    {
                        Id=employeeJobCustomFieldValue.JobCustomFieldId.SharedJobCustomFieldId.Id,
                        Value = employeeJobCustomFieldValue.JobCustomFieldValue
                    });
                }
                employeeJobPosition.EmployeeJobPositionAssignmentList.Add(employeeJobPositionAssignmentDTO);
            }
            return employeeJobPosition;
        }
    }
}
