using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Presentation.Contracts;
using System;
using MITD.PMSReport.Domain.Model;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Model;

namespace MITD.PMS.Interface
{
    //  [Interceptor(typeof(Interception))]
    public class PeriodJobPositionServiceFacade : IPeriodJobPositionServiceFacade
    {
        private readonly IJobPositionService jobPositionService;
        private readonly IMapper<JobPosition, JobPositionInPeriodAssignmentDTO> jobPositionAssignmentMapper;
        private readonly IMapper<JobPosition, JobPositionInPeriodDTOWithActions> jobPositionInPeriodDTOWithActionsMapper;
        private readonly IMapper<JobPosition, JobPositionInPeriodDTO> jobPositionInPeriodDTOMapper;
        private readonly IJobPositionRepository jobPositionRep;
        private readonly IEmployeeRepository employeeRep;
        private readonly IUnitRepository _unitRepository;
        private readonly IJobRepository _jobRepository;

        public PeriodJobPositionServiceFacade(IJobPositionService jobPositionService,
            IMapper<JobPosition, JobPositionInPeriodAssignmentDTO> jobPositionAssignmentMapper,
            IMapper<JobPosition, JobPositionInPeriodDTOWithActions> jobPositionInPeriodDTOWithActionsMapper,
            IMapper<JobPosition, JobPositionInPeriodDTO> jobPositionInPeriodDTOMapper,
            IJobPositionRepository jobPositionRep,
            IEmployeeRepository employeeRep,
            IUnitRepository unitRepository,
            IJobRepository jobRepository)
        {
            this.jobPositionService = jobPositionService;
            this.jobPositionAssignmentMapper = jobPositionAssignmentMapper;
            this.jobPositionInPeriodDTOWithActionsMapper = jobPositionInPeriodDTOWithActionsMapper;
            this.jobPositionInPeriodDTOMapper = jobPositionInPeriodDTOMapper;
            this.jobPositionRep = jobPositionRep;
            this.employeeRep = employeeRep;
            _unitRepository = unitRepository;
            _jobRepository = jobRepository;
        }

        [RequiredPermission(ActionType.AddJobPositionInPeriod)]
        public JobPositionInPeriodAssignmentDTO AssignJobPosition(long periodId, JobPositionInPeriodAssignmentDTO jobPositionInPeriod)
        {
            var jobPosition = jobPositionService.AssignJobPosition(new PeriodId(periodId),
                new SharedJobPositionId(jobPositionInPeriod.JobPositionId),
                jobPositionInPeriod.ParentJobPositionId != null
                    ? new SharedJobPositionId(jobPositionInPeriod.ParentJobPositionId.Value)
                    : null, new JobId(new PeriodId(periodId), new SharedJobId(jobPositionInPeriod.JobId)),
                new UnitId(new PeriodId(periodId), new SharedUnitId(jobPositionInPeriod.UnitId)));
            return jobPositionAssignmentMapper.MapToModel(jobPosition);
        }

        [RequiredPermission(ActionType.ModifyJobPositionInPeriod)]
        public JobPositionInPeriodAssignmentDTO UpdateJobPosition(long periodId, JobPositionInPeriodAssignmentDTO jobPositionInPeriod)
        {
            var jobPosition = jobPositionService.ModifyJobPosition(new PeriodId(periodId),
                new SharedJobPositionId(jobPositionInPeriod.JobPositionId),
                jobPositionInPeriod.ParentJobPositionId != null
                    ? new SharedJobPositionId(jobPositionInPeriod.ParentJobPositionId.Value)
                    : null, new JobId(new PeriodId(periodId), new SharedJobId(jobPositionInPeriod.JobId)),
                new UnitId(new PeriodId(periodId), new SharedUnitId(jobPositionInPeriod.UnitId)));
            return jobPositionAssignmentMapper.MapToModel(jobPosition);
        }

        [RequiredPermission(ActionType.DeleteJobPositionInPeriod)]
        public string RemoveJobPosition(long periodId, long jobPositionId)
        {
            jobPositionService.RemoveJobPosition(new PeriodId(periodId), new SharedJobPositionId(jobPositionId));
            return "JobPosition with Id " + jobPositionId + " removed";
        }

        [RequiredPermission(ActionType.ShowJobPositionInPeriod)]
        public IEnumerable<JobPositionInPeriodDTOWithActions> GetJobPositionsWithActions(long periodIdParam)
        {
            var periodId = new PeriodId(periodIdParam);
            var jobPositions = jobPositionRep.GetJobPositions(periodId);
            var res = jobPositions.Select(u => jobPositionInPeriodDTOWithActionsMapper.MapToModel(u)).ToList();

            res.ForEach(d =>
            {
                d.UnitName = _unitRepository.GetBy(new UnitId(periodId, new SharedUnitId(d.Unitid))).Name;
                d.JobName = _jobRepository.GetById(new JobId(periodId, new SharedJobId(d.JobId))).Name;
            });

            return res;
        }

        [RequiredPermission(ActionType.ShowJobPositionInPeriod)]
        public IEnumerable<JobPositionInPeriodDTO> GetJobPositions(long periodIdParam)
        {
            var periodId = new PeriodId(periodIdParam);
            var jobPositions = jobPositionRep.GetJobPositions(periodId);
            var res = jobPositions.Select(u => jobPositionInPeriodDTOMapper.MapToModel(u)).ToList();
            res.ForEach(d =>
            {
                d.UnitName = _unitRepository.GetBy(new UnitId(periodId, new SharedUnitId(d.Unitid))).Name;
                d.JobName = _jobRepository.GetById(new JobId(periodId, new SharedJobId(d.JobId))).Name;
            });
            return res;
        }

        [RequiredPermission(ActionType.ShowJobPositionInPeriod)]
        public JobPositionInPeriodAssignmentDTO GetJobPosition(long periodId, long jobPositionId)
        {
            var jobPosition = jobPositionRep.GetBy(new JobPositionId(new PeriodId(periodId), new SharedJobPositionId(jobPositionId)));
            return jobPositionAssignmentMapper.MapToModel(jobPosition);
        }

        [RequiredPermission(ActionType.ManageJobPositionInPeriodInquiry)]
        public List<InquirySubjectWithInquirersDTO> GetInquirySubjectsWithInquirers(long periodId, long jobPositionId)
        {
            var inquirySubjectWIthInquirersList = new List<InquirySubjectWithInquirersDTO>();
            var configurationItems =
                jobPositionService.GetInquirySubjectWithInquirer(new JobPositionId(new PeriodId(periodId),
                    new SharedJobPositionId(jobPositionId)));
            var inquirySubjectWithinquirers = configurationItems.GroupBy(c => c.Id.InquirySubjectId);
            foreach (var inquirySubjectWithinquirer in inquirySubjectWithinquirers)
            {
                var inquirySubject = employeeRep.GetBy(inquirySubjectWithinquirer.Key);
                var inquirySubjectInquirerDTO = new InquirySubjectWithInquirersDTO
                {
                    EmployeeName = inquirySubject.FullName,
                    EmployeeNo = inquirySubject.Id.EmployeeNo,
                };
                inquirySubjectInquirerDTO.CustomInquirers = new List<InquirerDTO>();
                inquirySubjectInquirerDTO.Inquirers = new List<InquirerDTO>();

                foreach (var itm in inquirySubjectWithinquirer)
                {
                    var inquirer = employeeRep.GetBy(itm.Id.InquirerId);
                    var inquirerJobPositionName = jobPositionRep.GetBy(itm.Id.InquirerJobPositionId).Name;
                    if (itm.IsAutoGenerated)
                    {

                        inquirySubjectInquirerDTO.Inquirers.Add(new InquirerDTO
                        {
                            EmployeeNo = inquirer.Id.EmployeeNo,
                            FullName = inquirer.FullName,
                            IsPermitted = itm.IsPermitted,
                            EmployeeJobPositionId = itm.Id.InquirerJobPositionId.SharedJobPositionId.Id,
                            EmployeeJobPositionName = inquirerJobPositionName
                        });
                    }
                    else
                    {

                        inquirySubjectInquirerDTO.CustomInquirers.Add(new InquirerDTO
                        {
                            EmployeeNo = inquirer.Id.EmployeeNo,
                            FullName = inquirer.FullName,
                            EmployeeJobPositionId = itm.Id.InquirerJobPositionId.SharedJobPositionId.Id,
                            EmployeeJobPositionName = inquirerJobPositionName
                        });
                    }

                }

                inquirySubjectWIthInquirersList.Add(inquirySubjectInquirerDTO);


            }
            return inquirySubjectWIthInquirersList;

        }

        [RequiredPermission(ActionType.ManageJobPositionInPeriodInquiry)]
        public InquirySubjectWithInquirersDTO UpdateInquirySubjectInquirers(long periodId, long jobPositionId,
            string inquirySubjectEmployeeNo, InquirySubjectWithInquirersDTO inquirySubjectWithInquirersDTO)
        {
            //var inquirerEmployeeIdList = new List<EmployeeId>();
            //inquirerEmployeeIdList.AddRange(inquirySubjectWithInquirersDTO.Inquirers.Where(i => i.IsPermitted)
            //    .Select(i => new EmployeeId(i.EmployeeNo, new PeriodId(periodId))));
            //inquirerEmployeeIdList.AddRange(
            //    inquirySubjectWithInquirersDTO.CustomInquirers.Select(
            //        c => new EmployeeId(c.EmployeeNo, new PeriodId(periodId))));
            //jobPositionService.UpdateInquirers(
            //    new EmployeeId(inquirySubjectWithInquirersDTO.EmployeeNo, new PeriodId(periodId)),
            //    new JobPositionId(new PeriodId(periodId), new SharedJobPositionId(jobPositionId)),
            //    inquirerEmployeeIdList);
            //return inquirySubjectWithInquirersDTO;

            var inquirerEmployeeIdList = new List<EmployeeIdWithJobPositionId>();
            inquirerEmployeeIdList.AddRange(inquirySubjectWithInquirersDTO.Inquirers.Where(i => i.IsPermitted)
               .Select(i => new EmployeeIdWithJobPositionId()
               {
                   EmployeeId = new EmployeeId(i.EmployeeNo, new PeriodId(periodId)),
                   JobPositionId = new JobPositionId(new PeriodId(periodId), new SharedJobPositionId(i.EmployeeJobPositionId))
               }));

            inquirerEmployeeIdList.AddRange(inquirySubjectWithInquirersDTO.CustomInquirers
               .Select(i => new EmployeeIdWithJobPositionId()
               {
                   EmployeeId = employeeRep.GetBy(new EmployeeId(i.EmployeeNo, new PeriodId(periodId))).Id,
                   JobPositionId = jobPositionRep.GetBy(new JobPositionId(new PeriodId(periodId), new SharedJobPositionId(i.EmployeeJobPositionId))).Id
               }));

            jobPositionService.UpdateInquirers(

                new EmployeeId(inquirySubjectWithInquirersDTO.EmployeeNo, new PeriodId(periodId)),
                new JobPositionId(new PeriodId(periodId), new SharedJobPositionId(jobPositionId)),
                inquirerEmployeeIdList);

            return inquirySubjectWithInquirersDTO;

        }


    }
}
