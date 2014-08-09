using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Common;
using MITD.PMSAdmin.Domain.Model.Policies;
using NHibernate.Linq;

namespace MITD.PMSAdmin.Persistence.NH
{
    public class PolicyRepository : NHRepository, IPolicyRepository
    {
        NHRepository<Policy> rep;
        public PolicyRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public PolicyRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep = new NHRepository<Policy>(unitOfWork);
        }

        public void FindBy(ListFetchStrategy<Policy> fs)
        {
            rep.GetAll(fs);
        }

        public void Add(Policy policy)
        {
            rep.Add(policy);
        }

        public void UpdatePolicy(Policy policy)
        {
            rep.Update(policy);
        }

        public Policy GetById(PolicyId policyId)
        {
            return rep.FindByKey(policyId);

        }

        public PolicyId GetNextId()
        {
            using (var ctx = Session.SessionFactory.OpenStatelessSession())
            {
                ctx.BeginTransaction();
                var res = ctx.CreateSQLQuery("Select next value for dbo.PolicySeq").UniqueResult<long>();
                return new PolicyId(res);
            }
        }

        public void DeletePolicy(Policy policy)
        {
            rep.Delete(policy);
        }

        public RuleEngineBasedPolicy GetRuleBasePolicyById(PolicyId policyId)
        {
            return Session.Query<Policy>().OfType<RuleEngineBasedPolicy>().First(re => re.Id == policyId);
        }

        public IList<Policy> GetAll()
        {
            return rep.GetAll();
        }

        public PolicyException ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new PolicyDuplicateException("Policy", "DictionaryName");
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new PolicyDeleteException("Policy", "Id");
            throw new Exception();
        }

        public PolicyException TryConvertException(Exception exp)
        {
            PolicyException res = null;
            try
            {
                res = ConvertException(exp);
            }
            catch (Exception e)
            {

            }
            return res;
        }
    }
}
