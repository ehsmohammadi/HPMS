using MITD.DataAccess.EF;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Service;
using MITD.Domain.Model;

namespace MITD.PMS.Persistence.EF
{
    public class CalculationRepository :EFRepository<Calculation>, ICalculationRepository
    {
        private ICalculationConfigurator calculationConfigurator;
        public CalculationRepository(EFUnitOfWork unitofwork, ICalculationConfigurator calculationConfigurator) : base(unitofwork)
        {
            this.calculationConfigurator = calculationConfigurator;
        }

        public CalculationRepository(IUnitOfWorkScope unitofworkscope, ICalculationConfigurator calculationConfigurator) : base(unitofworkscope)
        {
            this.calculationConfigurator = calculationConfigurator;
        }

        public Calculation GetById(CalculationId id)
        {
            var calculation= Single(c => c.Id == id);
            calculationConfigurator.Config(calculation);
            return calculation;
        }


        public void FindBy(Domain.Model.Periods.PeriodId periodId, ListFetchStrategy<Calculation> fs)
        {
            throw new System.NotImplementedException();
        }

        public long GetNextId()
        {
            throw new System.NotImplementedException();
        }
    }
}
