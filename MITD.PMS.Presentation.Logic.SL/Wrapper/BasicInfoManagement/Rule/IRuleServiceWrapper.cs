using System.Collections.Generic;
using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;
using System.Collections.ObjectModel;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MITD.PMS.Presentation.Logic
{
    public interface IRuleServiceWrapper : IServiceWrapper
    {

        void DeleteRule(Action<string, Exception> action, long policyId, long ruleId);
        void GetRule(Action<RuleDTO, Exception> action, long policyId, long id);
        void AddRule(Action<RuleDTO, Exception> action, RuleDTO rule);
        void UpdateRule(Action<RuleDTO, Exception> action, RuleDTO rule);
        void GetPolicyRulesWithPagination(Action<PolicyRules, Exception> action, long periodId, int pageSize, int pageIndex);
        void GetPolicyRules(Action<PolicyRules, Exception> action, long policyId);

        void CompileRule(Action<string, Exception> action, long policyId, RuleContentDTO ruleContent);
        void GetRuleExcuteTimes(Action<List<RuleExcuteTimeDTO>, Exception> action);

        void GetAllRuleTrails(Action<PageResultDTO<RuleTrailDTOWithAction>, Exception> action, long policyId, long ruleId, int pageSize, int pageIndex);
        void GetRuleTrail(Action<RuleTrailDTO, Exception> action, long policyId, long ruleId, long trailId);
    }
}
