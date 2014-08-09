using MITD.Core.RuleEngine.Model;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MITD.PMSAdmin.Domain.Model.Policies
{
    public class RuleEngineBasedPolicy : Policy
    {
        #region Fields


        #endregion

        #region Constructors
        protected RuleEngineBasedPolicy():base()
        {}

        public RuleEngineBasedPolicy(PolicyId policyId, string name, string dictionaryName):base(policyId,name,dictionaryName)
        {} 
        #endregion

        #region Properties

        private readonly IList<RuleId> rules = new List<RuleId>();
        public virtual IReadOnlyList<RuleId> Rules
        {
            get { return rules.ToList().AsReadOnly(); }
        }

        private readonly IList<RuleFunctionId> ruleFunctions = new List<RuleFunctionId>();
        public virtual IReadOnlyList<RuleFunctionId> RuleFunctions
        {
            get { return ruleFunctions.ToList().AsReadOnly(); }
        }

        #endregion

        #region Public Methods
        public virtual void AssignRule(RuleBase rule)
        {
            rules.Add(rule.Id);
        }
        public virtual void RemoveRule(RuleBase rule)
        {
            rules.Remove(rule.Id);
        }
        
        public virtual void AssignRuleFunction(RuleFunctionBase ruleFunction)
        {
            ruleFunctions.Add(ruleFunction.Id);
        }

        public virtual void RemoveRuleFunction(RuleFunctionBase ruleFunction)
        {
            ruleFunctions.Remove(ruleFunction.Id);
        }

        #endregion


    }
}