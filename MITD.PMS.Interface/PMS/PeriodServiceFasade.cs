using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Contracts.Fasade;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Model;
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

        public PeriodServiceFacade(IPeriodService periodService,
            IMapper<Period, PeriodDescriptionDTO> periodDescriptionMapper,
            IMapper<Period, PeriodDTOWithAction> periodDTOWithActionsMapper,
            IMapper<Period, PeriodDTO> periodDTOMapper,
            IMapper<InquiryInitializingProgress, PeriodStateWithIntializeInquirySummaryDTO> periodInitializeInquiryStateReportMapper,
            IMapper<BasicDataCopyingProgress, PeriodStateWithCopyingSummaryDTO> periodCopyingStateReportMapper,
            IPeriodRepository periodRep, IPeriodEngineService periodEngine)
        {
            this.periodService = periodService;
            this.periodDescriptionMapper = periodDescriptionMapper;
            this.periodDTOWithActionsMapper = periodDTOWithActionsMapper;
            this.periodDTOMapper = periodDTOMapper;
            this.periodInitializeInquiryStateReportMapper = periodInitializeInquiryStateReportMapper;
            this.periodCopyingStateReportMapper = periodCopyingStateReportMapper;
            this.periodRep = periodRep;
            this.periodEngine = periodEngine;

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

        public List<PeriodDescriptionDTO> GetPeriodsWithDeterministicCalculation()
        {
            List<Period> periods = periodRep.GetPeriodsWithDeterministicCalculation();
            return periods.Select(p => periodDescriptionMapper.MapToModel(p)).ToList();
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
            var period = periodService.AddPeriod(dto.Name, dto.StartDate, dto.EndDate);
            return periodDTOMapper.MapToModel(period);
        }

        [RequiredPermission(ActionType.ModifyPeriod)]
        public PeriodDTO UpdatePeriod(PeriodDTO dto)
        {

            Period period = periodService.UpdatePeriod(new PeriodId(dto.Id), dto.Name, dto.StartDate, dto.EndDate);
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
            else if (stateDto.State == (int)PeriodState.ClaimingStarted)
                periodService.StartClaiming(new PeriodId(periodId));
            else if (stateDto.State == (int)PeriodState.ClaimingFinished)
                periodService.FinishClaiming(new PeriodId(periodId));
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
