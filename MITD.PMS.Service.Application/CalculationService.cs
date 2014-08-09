using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Domain.Model.Calculations.Repository;


namespace MITD.PMS.Service.Application
{
    public class CalculationService:ICalculationService
    {
        private readonly ICalculationRepository calculationRepository;

        public CalculationService(ICalculationRepository calculationRepository)
        {
            this.calculationRepository = calculationRepository;
        }

        public void Calculate()
        {
            
        }

        private void Add()
        {

        }

        private void Run()
        {

        }
    }
}
