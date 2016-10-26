using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Contracts.Fasade;
using MITD.PMSSecurity.Domain;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
    [Interceptor(typeof(Interception))]
    public class PeriodServiceFacade : IPeriodServiceFacade
    {
        private readonly IPeriodService periodService;
        private readonly IMapper<Period, PeriodDescriptionDTO> periodDescriptionMapper;
        private readonly IMapper<Period, PeriodDTOWithAction> periodDTOWithActionsMapper;
        private readonly IMapper<Period, PeriodDTO> periodDTOMapper;
        private readonly IMapper<InquiryInitializingProgress, PeriodStateWithIntializeInquirySummaryDTO> periodInitializeInquiryStateReportMapper;
        private readonly IMapper<BasicDataCopyingProgress, PeriodStateWithCopyingSummaryDTO>
            periodCopyingStateReportMapper;
        private readonly IPeriodRepository periodRep;
        private IPeriodEngineService periodEngine;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IJobPositionRepository jobPositionRepository;
        private readonly IUnitRepository unitRepository;
        private readonly IJobIndexPointRepository jobIndexPointRepository;
        private readonly ICalculationRepository calculationRepository;
        private readonly IJobIndexRepository jobIndexRepository;

        public PeriodServiceFacade(IPeriodService periodService,
            IMapper<Period, PeriodDescriptionDTO> periodDescriptionMapper,
            IMapper<Period, PeriodDTOWithAction> periodDTOWithActionsMapper,
            IMapper<Period, PeriodDTO> periodDTOMapper,
            IMapper<InquiryInitializingProgress, PeriodStateWithIntializeInquirySummaryDTO>
                periodInitializeInquiryStateReportMapper,
            IMapper<BasicDataCopyingProgress, PeriodStateWithCopyingSummaryDTO> periodCopyingStateReportMapper,
            IPeriodRepository periodRep, IPeriodEngineService periodEngine, IEmployeeRepository employeeRepository,
            IJobPositionRepository jobPositionRepository, IUnitRepository unitRepository,
            IJobIndexPointRepository jobIndexPointRepository, ICalculationRepository calculationRepository, IJobIndexRepository jobIndexRepository)
        {
            this.periodService = periodService;
            this.periodDescriptionMapper = periodDescriptionMapper;
            this.periodDTOWithActionsMapper = periodDTOWithActionsMapper;
            this.periodDTOMapper = periodDTOMapper;
            this.periodInitializeInquiryStateReportMapper = periodInitializeInquiryStateReportMapper;
            this.periodCopyingStateReportMapper = periodCopyingStateReportMapper;
            this.periodRep = periodRep;
            this.periodEngine = periodEngine;
            this.employeeRepository = employeeRepository;
            this.jobPositionRepository = jobPositionRepository;
            this.unitRepository = unitRepository;
            this.jobIndexPointRepository = jobIndexPointRepository;
            this.calculationRepository = calculationRepository;
            this.jobIndexRepository = jobIndexRepository;
        }

        [RequiredPermission(ActionType.ShowPeriod)]
        public PageResultDTO<PeriodDTOWithAction> GetAllPeriods(int pageSize, int pageIndex)
        {
            var fs = new ListFetchStrategy<Period>(Enums.FetchInUnitOfWorkOption.NoTracking);
            fs.WithPaging(pageSize, pageIndex);
            periodRep.GetAll(fs);
            var res = new PageResultDTO<PeriodDTOWithAction>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result =
                fs.PageCriteria.PageResult.Result.Select(p => periodDTOWithActionsMapper.MapToModel(p)).ToList();
            return res;
        }

        [RequiredPermission(ActionType.ShowPeriod)]
        public List<PeriodDescriptionDTO> GetAllPeriods()
        {
            var periods = periodRep.GetAll();
            return periods.Select(p => periodDescriptionMapper.MapToModel(p)).ToList();
        }

        public List<PeriodDescriptionDTO> GetPeriodsWithConfirmedResult()
        {
            List<Period> periods = periodRep.GetPeriodsWithConfirmedResult();
            return periods.Select(p => periodDescriptionMapper.MapToModel(p)).ToList();
        }

        public EmployeeResultDTO GetEmployeeResultInPeriod(long periodIdParam, string employeeNo)
        {
            var periodId = new PeriodId(periodIdParam);
            var period = periodRep.GetById(periodId);
            var employee = employeeRepository.GetBy(new EmployeeId(employeeNo, periodId));
            if (employee == null)
                throw new Exception("شماره پرسنلی شما در سیستم موجود نمی باشد");
            var employeeJobPositionIds = employee.JobPositions.Select(j => j.JobPositionId).ToList();
            var employeeUnitIds = new List<UnitId>();
            var jobPositionNames = string.Empty;
            var unitNames = string.Empty;
            var unitRootNames = string.Empty;
            var calculation = calculationRepository.GetDeterministicCalculation(period);
            foreach (var jobPositionId in employeeJobPositionIds)
            {
                var jobPosition = jobPositionRepository.GetBy(jobPositionId);
                jobPositionNames += jobPosition.Name;
                if (employeeJobPositionIds.Count() > 1)
                    jobPositionNames += " - ";
                employeeUnitIds.Add(jobPosition.UnitId);
            }

            foreach (var unitId in employeeUnitIds)
            {
                var unit = unitRepository.GetBy(unitId);
                unitNames += unit.Name;
                if (employeeUnitIds.Count > 1)
                    unitNames += " - ";
                unitRootNames += unit.Parent.Name;
                if (employeeUnitIds.Count > 1)
                    unitRootNames += " - ";
            }

            //var finalUnitPoint =jobIndexPointRepository.GetFinalUnitPoint(calculation.Id, employee.Id);

            var res = new EmployeeResultDTO
            {
                PeriodName = period.Name,
                PeriodTimeLine = "از تاریخ " + PDateHelper.GregorianToHijri(period.StartDate, false) + " تا تاریخ " + PDateHelper.GregorianToHijri(period.EndDate.Date, false),
                EmployeeFullName = employee.FullName,
                EmployeeNo = employeeNo,
                EmployeeJobPositionName = jobPositionNames,
                TotalPoint = employee.FinalPoint.ToString(CultureInfo.InvariantCulture),
                EmployeeUnitName = unitNames,
                EmployeeUnitRootName = unitRootNames,
                //TotalUnitPoint =finalUnitPoint==null?(0).ToString(): finalUnitPoint.Value.ToString(CultureInfo.InvariantCulture),
                JobIndexValues = new List<JobIndexValueDTO>()
            };

            var employeeIndexPoints = jobIndexPointRepository.GetBy(calculation.Id, employee.Id);
            foreach (var indexPoint in employeeIndexPoints)
            {
                var jobIndex = (JobIndex)jobIndexRepository.GetById(indexPoint.JobIndexId);
                res.JobIndexValues.Add(new JobIndexValueDTO
                {
                    JobIndexName = jobIndex.Name,
                    IndexValue = indexPoint.Value.ToString()
                });
            }


            return res;
            //Find<EmployeePoint>(
            //    jp =>
            //        jp.Name == "finalunitPoint" && jp.CalculationId == calculation.Id &&
            //        jp.EmployeeId == employee.Id,fs);
            //var jobpositions=jobPositionRepository.


        }

        public SubordinatesResultDTO GetSubordinatesResultInPeriod(long periodIdParam, string managerEmployeeNo)
        {
            var periodId = new PeriodId(periodIdParam);
            var period = periodRep.GetById(periodId);
            var manager = employeeRepository.GetBy(new EmployeeId(managerEmployeeNo, periodId));
            if (manager == null)
                throw new Exception("شماره پرسنلی شما در سیستم موجود نمی باشد");
            var managerJobPositionIds = manager.JobPositions.Select(j => j.JobPositionId).ToList();
            var managerUnitIds = new List<UnitId>();
            var unitNames = string.Empty;
            var unitRootNames = string.Empty;
            var calculation = calculationRepository.GetDeterministicCalculation(period);
            foreach (var jobPositionId in managerJobPositionIds)
            {
                var jobPosition = jobPositionRepository.GetBy(jobPositionId);
                managerUnitIds.Add(jobPosition.UnitId);
            }

            foreach (var unitId in managerUnitIds)
            {
                var unit = unitRepository.GetBy(unitId);
                unitNames += unit.Name;
                if (managerUnitIds.Count > 1)
                    unitNames += " - ";
                unitRootNames += unit.Parent.Name;
                if (managerUnitIds.Count > 1)
                    unitRootNames += " - ";
            }

            //var finalUnitPoint = jobIndexPointRepository.GetFinalUnitPoint(calculation.Id, manager.Id);

            var res = new SubordinatesResultDTO
            {
                PeriodName = period.Name,
                PeriodTimeLine = "از تاریخ " + PDateHelper.GregorianToHijri(period.StartDate, false) + " تا تاریخ " + PDateHelper.GregorianToHijri(period.EndDate.Date, false),
                EmployeeUnitName = unitNames,
                EmployeeUnitRootName = unitRootNames,
                //TotalUnitPoint = finalUnitPoint == null ? (0).ToString() : finalUnitPoint.Value.ToString(CultureInfo.InvariantCulture),
                Subordinates = new List<EmployeeResultDTO>()
            };
            var subordinateEmployeeJobposition = new List<JobPosition>();
            foreach (var managerJobPositionId in managerJobPositionIds)
            {
                subordinateEmployeeJobposition.AddRange(jobPositionRepository.GetAllJobPositionByParentId(managerJobPositionId));
            }
            var employeeIds = subordinateEmployeeJobposition.SelectMany(sj => sj.Employees.Select(e => e.EmployeeId)).ToList();
            var oneOfSubordinate = employeeIds.FirstOrDefault();
            if (oneOfSubordinate != null)
            {
                var finalUnitPoint = jobIndexPointRepository.GetFinalUnitPoint(calculation.Id, oneOfSubordinate);
                res.TotalUnitPoint = finalUnitPoint.Value.ToString(CultureInfo.InvariantCulture);
            }
            foreach (var employeeId in employeeIds)
            {
                var employee = employeeRepository.GetBy(employeeId);
                var employeeIndexPoints = jobIndexPointRepository.GetBy(calculation.Id, employee.Id);
                var employeeResult = new EmployeeResultDTO
                {
                    EmployeeFullName = employee.FullName,
                    EmployeeNo = employeeId.EmployeeNo,
                    JobIndexValues = new List<JobIndexValueDTO>(),
                    //EmployeeJobPositionName = jobPositionNames,
                    TotalPoint = employee.FinalPoint.ToString(CultureInfo.InvariantCulture),
                };
                foreach (var indexPoint in employeeIndexPoints)
                {
                    var jobIndex = (JobIndex)jobIndexRepository.GetById(indexPoint.JobIndexId);
                    employeeResult.JobIndexValues.Add(new JobIndexValueDTO
                    {
                        JobIndexName = jobIndex.Name,
                        IndexValue = indexPoint.Value.ToString(),
                        JobIndexId = jobIndex.SharedJobIndexId.Id,
                        Id = indexPoint.Id.Id

                    });
                }
                res.Subordinates.Add(employeeResult);
            }

            return res;
        }

        [RequiredPermission(ActionType.DeletePeriod)]
        public string DeletePeriod(long id)
        {
            periodService.Delete(new PeriodId(id));
            return "Period with " + id + " Deleted";
        }

        public PeriodDTO GetPeriod(long id)
        {
            var period = periodRep.GetById(new PeriodId(id));
            return periodDTOMapper.MapToModel(period);
        }

        [RequiredPermission(ActionType.AddPeriod)]
        public PeriodDTO AddPeriod(PeriodDTO dto)
        {
            var period = periodService.AddPeriod(dto.Name, dto.StartDate, dto.EndDate, dto.MaxFinalPoint);
            return periodDTOMapper.MapToModel(period);
        }

        [RequiredPermission(ActionType.ModifyPeriod)]
        public PeriodDTO UpdatePeriod(PeriodDTO dto)
        {

            Period period = periodService.UpdatePeriod(new PeriodId(dto.Id), dto.Name, dto.StartDate, dto.EndDate, dto.MaxFinalPoint);
            return periodDTOMapper.MapToModel(period);
        }

        public PeriodDTO GetCurrentPeriod()
        {

            Period period = periodService.GetCurrentPeriod();
            return periodDTOMapper.MapToModel(period);
        }

        [RequiredPermission(ActionType.ActivatePeriod)]
        [RequiredPermission(ActionType.InitializePeriodForInquiry)]
        [RequiredPermission(ActionType.StartInquiry)]
        [RequiredPermission(ActionType.CompleteInquiry)]
        [RequiredPermission(ActionType.ClosePeriod)]
        public void ChangePeriodState(long periodId, PeriodStateDTO stateDto)
        {
            if (stateDto.State == (int)PeriodState.InitializingForInquiry)
                periodEngine.InitializeInquiry(new PeriodId(periodId));
            else if (stateDto.State == (int)PeriodState.InquiryStarted)
                periodService.StartInquiry(new PeriodId(periodId));
            else if (stateDto.State == (int)PeriodState.InquiryCompleted)
                periodService.CompleteInquiry(new PeriodId(periodId));
            else if (stateDto.State == (int)PeriodState.Closed)
                periodService.Close(new PeriodId(periodId));
            else if (stateDto.State == (int)PeriodState.Confirmation)
                periodService.StartConfirmation(new PeriodId(periodId));
            else if (stateDto.State == (int)PeriodState.Confirmed)
                periodService.Confirm(new PeriodId(periodId));
        }

        public void CopyBasicData(long sourcePeriodId, long destionationPeriodId, PeriodStateDTO stateDto)
        {
            periodEngine.CopyBasicData(new PeriodId(sourcePeriodId), new PeriodId(destionationPeriodId));
        }

        public PeriodStateWithIntializeInquirySummaryDTO GetPeriodState(long id)
        {
            var inquiryIp = periodEngine.GetIntializeInquiryState(new PeriodId(id));
            return new PeriodStateWithIntializeInquirySummaryDTO
            {
                MessageList = inquiryIp.MessageList,
                Percent = inquiryIp.Percent,
                StateName = inquiryIp.StateName
            };
        }

        public PeriodStateWithCopyingSummaryDTO GetPeriodBasicDataCopyState(long periodId)
        {
            var progress = periodEngine.GetPeriodCopyingStateProgress(new PeriodId(periodId));
            return new PeriodStateWithCopyingSummaryDTO
            {
                MessageList = progress.MessageList,
                Percent = progress.Percent,
                StateName = progress.StateName
            };
        }

        public void RollBackPeriodState(long periodId)
        {
            periodService.RollBack(new PeriodId(periodId));
        }

        public void ChangePeriodActiveStatus(long id, bool active)
        {
            periodService.ChangePeriodActiveStatus(new PeriodId(id), active);

        }


    }
}
