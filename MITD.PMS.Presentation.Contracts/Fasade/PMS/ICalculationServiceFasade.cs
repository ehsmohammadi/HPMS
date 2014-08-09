

using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface ICalculationServiceFacade:IFacadeService
    { 
        CalculationDTO AddCalculation(CalculationDTO calculations);

        PageResultDTO<CalculationBriefDTOWithAction> GetAllCalculations(long periodId,int pageSize, int pageIndex);

        CalculationStateWithRunSummaryDTO GetCalculationState(long Id);

        CalculationDTO GetCalculation(long id);

        void ChangeCalculationState(long periodId, long Id, CalculationStateDTO stateDto);

        void DeleteCalculation(long id);
        CalculationDTO GetDeterministicCalculation(long periodId);
        void ChangeDeterministicStatus(long calculationId,bool isDeterministic);
        CalculationExceptionDTO GetCalculationException(long id);
        PageResultDTO<CalculationExceptionBriefDTOWithAction> GetAllCalculationExceptions(long calculationId, int pageSize, int pageIndex);
    }
}
