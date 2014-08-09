using MITD.Core.RuleEngine;
using MITD.Core.RuleEngine.Model;
using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MITD.PMS.Domain.Model.Policies
{
    public class RulesCompiled : IDomainEvent<RulesCompiled>
    {
        private RuleCompileResult compileResult;
        private Dictionary<string, RuleBase> rules;
        public RulesCompiled(RuleCompileResult compileResult, Dictionary<string,RuleBase> rules)
        {
            this.compileResult = compileResult;
            this.rules = rules;
        }
        public RuleCompileResult CompileResult { get { return compileResult; } }
        public Dictionary<string, RuleBase> Rules { get { return rules; } }
        public bool SameEventAs(RulesCompiled other)
        {
            throw new NotImplementedException();
        }
    }
}
