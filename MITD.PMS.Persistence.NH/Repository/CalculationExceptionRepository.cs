using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Service;
using MITD.PMSReport.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.CalculationExceptions;

namespace MITD.PMS.Persistence.NH
{
    public class CalculationExceptionRepository : NHRepository, ICalculationExceptionRepository
    {
        private NHRepository<EmployeeCalculationException> rep;

        public CalculationExceptionRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public CalculationExceptionRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep = new NHRepository<EmployeeCalculationException>(unitOfWork);
        }

        public void Add(EmployeeCalculationException calculation)
        {
            rep.Add(calculation);
        }

        public EmployeeCalculationException GetById(EmployeeCalculationExceptionId calculationId)
        {
            return rep.FindByKey(calculationId);
        }

        public EmployeeCalculationExceptionId GetNextId()
        {
            var id = session.CreateSQLQuery("select next value for [dbo].[Calculations_ExceptionsSeq]").UniqueResult<long>();
            return new EmployeeCalculationExceptionId(id);
        }


        public IList<EmployeeCalculationException> GetAllBy(CalculationId calculationId)
        {
            return rep.Find(c => c.CalculationId == calculationId);
        }
        

        public void DeleteAll(CalculationId calculationId)
        {
            session.CreateSQLQuery("DELETE FROM Calculations_Exceptions WHERE CalculationId = :id")
            .SetParameter("id", calculationId.Id)
            .ExecuteUpdate();
        }

        public IList<EmployeeCalculationException> FindBy(CalculationId calculationId, ListFetchStrategy<EmployeeCalculationException> fs)
        {
            return rep.Find(c => c.CalculationId == calculationId, fs);
        }
    }
}
