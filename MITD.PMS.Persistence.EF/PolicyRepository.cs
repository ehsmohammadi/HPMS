using MITD.DataAccess.EF;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Persistence.EF
{
    public class PolicyRepository :EFRepository<Policy>, IPolicyRepository
    {
        private IPolicyConfigurator policyConfigurator; 

        public PolicyRepository(EFUnitOfWork unitofwork, IPolicyConfigurator policyConfigurator) : base(unitofwork)
        {
            this.policyConfigurator = policyConfigurator;
        }

        public PolicyRepository(IUnitOfWorkScope unitofworkscope, IPolicyConfigurator policyConfigurator) : base(unitofworkscope)
        {
            this.policyConfigurator = policyConfigurator;
        }

        public Policy GetById(PolicyId policyId)
        {
            var policy = Single(p => p.Id == policyId);
            policyConfigurator.Config(policy);
            return policy;
        }

        public long GetNextId()
        {
            throw new System.NotImplementedException();
        }
    }
}
