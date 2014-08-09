using System;
using MITD.Domain.Repository;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.Policies;
using System.Transactions;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Application
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository policyRep;

        public PolicyService(IPolicyRepository policyRep)
        {
            this.policyRep = policyRep;
        }

        public Policy AddPolicy(string name, string dictionaryName)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var id = policyRep.GetNextId();
                    var policy = new RuleEngineBasedPolicy(id, name, dictionaryName);
                    policyRep.Add(policy);
                    scope.Complete();
                    return policy;
                }
            }
            catch (Exception exp)
            {
                var res = policyRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Policy UpdatePolicy(PolicyId policyId, string name, string dictionaryName)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var policy = policyRep.GetById(policyId);
                    policy.Update(name, dictionaryName);
                    scope.Complete();
                    return policy;
                }
            }
            catch (Exception exp)
            {
                var res = policyRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public void DeletePolicy(PolicyId policyId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var policy = policyRep.GetById(policyId);
                    policyRep.DeletePolicy(policy);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = policyRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }
    }
}
