using System;
using MITD.Domain.Repository;
using System.Collections.Generic;

namespace MITD.PMSAdmin.Domain.Model.Policies
{
    public interface IPolicyRepository : IRepository
    {
        void FindBy(ListFetchStrategy<Policy> fs);
        void Add(Policy policy);
        void UpdatePolicy(Policy policy);
        Policy GetById(PolicyId policyId);
        PolicyId GetNextId();
        void DeletePolicy(Policy policy);
        RuleEngineBasedPolicy GetRuleBasePolicyById(PolicyId policyId);
        IList<Policy> GetAll();

        PolicyException ConvertException(Exception exp);
        PolicyException TryConvertException(Exception exp);
    }
}
