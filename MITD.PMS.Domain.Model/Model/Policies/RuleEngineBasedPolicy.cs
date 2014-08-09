using MITD.Core.RuleEngine.Model;
using MITD.PMS.Domain.Service;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MITD.PMS.Domain.Model.Policies
{
    public class RuleEngineBasedPolicy : Policy
    {
        #region Fields
        private readonly IList<RuleId> rules = new List<RuleId>();
        private readonly IList<RuleFunctionId> ruleFunctions = new List<RuleFunctionId>();
        private readonly byte[] rowVersion;

        #endregion

        #region Constructors
        protected RuleEngineBasedPolicy():base()
        {}

        #endregion

        #region Properties
        public virtual IReadOnlyList<RuleId> Rules { get { return rules.ToList().AsReadOnly(); } }
        public virtual IReadOnlyList<RuleFunctionId> RuleFunctions { get { return ruleFunctions.ToList().AsReadOnly(); } }
        #endregion

        #region Public Methods
        public virtual void SetPolicyEngine(IRuleEngineBasedPolicyService ruleEngineBasedPolicyService)
        {
            PolicyEngine = ruleEngineBasedPolicyService;
        }
        #endregion
    }
}