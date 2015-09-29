using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.CalculationExceptions;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Presentation.Contracts;
using Omu.ValueInjecter;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMSReport.Domain.Model;
using System;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Interface
{
  //  [Interceptor(typeof(Interception))]
    public class CalculationServiceFacade : ICalculationServiceFacade
    {
        private readonly ICalculationService calculationService;
        private readonly IMapper<CalculationWithPolicyAndPeriod, CalculationDTO> calculationMapper;
        private readonly IMapper<CalculationWithPolicyAndPeriod, CalculationBriefDTOWithAction> calculationBriefMapper;
        private readonly IMapper<EmployeeCalculationException, CalculationExceptionBriefDTOWithAction> calculationExceptionBriefMapper;
        private readonly IMapper<EmployeeCalculationException, CalculationExceptionDTO> calculationExceptionMapper;
        private readonly ICalculationRepository calculationRep;
        private IMapper<CalculationStateReport, CalculationStateWithRunSummaryDTO> calculationStateReportMapper;
        private IPeriodRepository PeriodRep;
        private IPolicyRepository PolicyRep;
        private readonly IEmployeeRepository employeeRep;
        private readonly ICalculationExceptionRepository calculationExpRepository;
        private ICalculationEngineService calcEngineService;

        public CalculationServiceFacade(ICalculationService calculationService,
                                        IMapper<CalculationWithPolicyAndPeriod, CalculationDTO> calculationMapper,
                                        IMapper<CalculationWithPolicyAndPeriod, CalculationBriefDTOWithAction> calculationBriefMapper,
                                        IMapper<EmployeeCalculationException, CalculationExceptionBriefDTOWithAction> calculationExceptionBriefMapper,
                                        IMapper<EmployeeCalculationException, CalculationExceptionDTO> calculationExceptionMapper,
                                        IMapper<CalculationStateReport, CalculationStateWithRunSummaryDTO> calculationStateReportMapper,
                                        ICalculationRepository calculationRep, IPeriodRepository PeriodRep, IPolicyRepository PolicyRep,
                                        IEmployeeRepository employeeRep,
                                        ICalculationExceptionRepository calculationExpRepository,
                                        ICalculationEngineService calcEngineService)
        {
            this.calculationService = calculationService;
            this.calculationMapper = calculationMapper;
            this.calculationBriefMapper = calculationBriefMapper;
            this.calculationExceptionBriefMapper = calculationExceptionBriefMapper;
            this.calculationExceptionMapper = calculationExceptionMapper;
            this.calculationRep = calculationRep;
            this.calculationStateReportMapper = calculationStateReportMapper;
            this.PeriodRep = PeriodRep;
            this.PolicyRep = PolicyRep;
            this.employeeRep = employeeRep;
            this.calculationExpRepository = calculationExpRepository;
            this.calcEngineService = calcEngineService;
        }

        public CalculationDTO AddCalculation(CalculationDTO calculation)
        {
            var calc=calculationService.AddCalculation(new PolicyId(calculation.PolicyId),
                new PeriodId(calculation.PeriodId),calculation.Name, calculation.EmployeeIdList);
            var res = new CalculationWithPolicyAndPeriod
            {
                Calculation = calc,
                Policy = PolicyRep.GetById(calc.PolicyId),
                Period = PeriodRep.GetById(calc.PeriodId)
            };
            return calculationMapper.MapToModel(res);

        }

        public PageResultDTO<CalculationBriefDTOWithAction> GetAllCalculations(long periodId,int pageSize, int pageIndex)
        {
            var fs = new ListFetchStrategy<CalculationWithPolicyAndPeriod>(Enums.FetchInUnitOfWorkOption.NoTracking);
            fs.WithPaging(pageSize, pageIndex).OrderBy(c=>c.Calculation.Id.Id);
            var x = calculationRep.FindByWithPolicy(new PeriodId(periodId),fs);

            var res = new PageResultDTO<CalculationBriefDTOWithAction>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            var lst = fs.PageCriteria.PageResult.Result.Select(p => calculationBriefMapper.MapToModel(p)).ToList();
            res.Result = lst;
            return res;
        }

        public CalculationStateWithRunSummaryDTO GetCalculationState(long id)
        {
            var progress = calcEngineService.GetCalculationState(new CalculationId(id));
            return new CalculationStateWithRunSummaryDTO
            {
                MessageList = progress.MessageList.ToList(),
                Percent = progress.Percent,
                StateName = progress.StateName
            };
        }

        public CalculationDTO GetCalculation(long id)
        {
            var calc = calculationService.GetCalculation(new CalculationId(id));
            var res = new CalculationWithPolicyAndPeriod
            {
                Calculation = calc,
                Policy = PolicyRep.GetById(calc.PolicyId),
                Period = PeriodRep.GetById(calc.PeriodId)
            };
            return calculationMapper.MapToModel(res);
        }


        public void ChangeCalculationState(long periodId, long Id, CalculationStateDTO stateDto)
        {
            if (stateDto.State == Convert.ToInt32(CalculationState.Running.Value))
                calcEngineService.RunCalculation(new CalculationId(Id));
            else if (stateDto.State == Convert.ToInt32(CalculationState.Stopped.Value))
                calcEngineService.StopCalculation(new CalculationId(Id));
            else if (stateDto.State == Convert.ToInt32(CalculationState.Paused.Value))
                calcEngineService.PauseCalculation(new CalculationId(Id));
        }

        public void DeleteCalculation(long id)
        {
            calculationService.DeleteCalculation(new CalculationId(id));
        }

        public CalculationDTO GetDeterministicCalculation(long periodId)
        {
            var calc = calculationService.GetDeterministicCalculation(new PeriodId(periodId));
            var res = new CalculationWithPolicyAndPeriod
            {
                Calculation = calc,
                Policy = PolicyRep.GetById(calc.PolicyId),
                Period = PeriodRep.GetById(calc.PeriodId)
            };
            return calculationMapper.MapToModel(res);
        }

        public void ChangeDeterministicStatus(long calculationId,bool isDeterministic)
        {
            calculationService.ChangeDeterministicStatus(new CalculationId(calculationId), isDeterministic);
        }

        public CalculationExceptionDTO GetCalculationException(long calculationExpId)
        {
            var calcException = calculationExpRepository.GetById(new EmployeeCalculationExceptionId(calculationExpId));
            var employee = employeeRep.GetBy(calcException.EmployeeId);
            var calcExpDto = calculationExceptionMapper.MapToModel(calcException);
            calcExpDto.EmployeeFullName = employee.FullName;
            return calcExpDto;
        }

        public PageResultDTO<CalculationExceptionBriefDTOWithAction> GetAllCalculationExceptions(long calculationId, int pageSize, int pageIndex)
        {
            var fs = new ListFetchStrategy<EmployeeCalculationException>(Enums.FetchInUnitOfWorkOption.NoTracking);
            fs.WithPaging(pageSize, pageIndex).OrderByDescending(c => c.Id.Id);
            var x = calculationExpRepository.FindBy(new CalculationId(calculationId), fs);

            var res = new PageResultDTO<CalculationExceptionBriefDTOWithAction>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            var lst = fs.PageCriteria.PageResult.Result.Select(p => calculationExceptionBriefMapper.MapToModel(p)).ToList();
            res.Result = lst;
            
            return res;
        }
    }
}
