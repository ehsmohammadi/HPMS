using System;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.Presentation;


namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public class RuleServiceWrapper : IRuleServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string apiAddress = PMSClientConfig.BaseApiAddress + "Rules";
        private readonly string policyRulesApiAddress = PMSClientConfig.BaseApiAddress + "PolicyRules";

        public RuleServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        public void DeleteRule(Action<string, Exception> action, long policyId, long ruleId)
        {
            var url = string.Format(policyRulesApiAddress + "?policyId=" + policyId + "&Id=" + ruleId);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPolicyRulesWithPagination(Action<PolicyRules, Exception> action, long policyId, int pageSize, int pageIndex)
        {
            var url = string.Format(policyRulesApiAddress + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex + "&PolicyId=" + policyId);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPolicyRules(Action<PolicyRules, Exception> action, long policyId)
        {
            var url = string.Format(policyRulesApiAddress + "?PolicyId=" + policyId);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetRule(Action<RuleDTO, Exception> action,long policyId, long ruleId)
        {
            var url = string.Format(policyRulesApiAddress + "?policyId=" + policyId + "&Id=" + ruleId);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddRule(Action<RuleDTO, Exception> action, RuleDTO rule)
        {
            WebClientHelper.Post(new Uri(apiAddress, PMSClientConfig.UriKind), action, rule, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateRule(Action<RuleDTO, Exception> action, RuleDTO rule)
        {
            var url = string.Format(apiAddress + "?Id=" + rule.Id);
            WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, rule, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void CompileRule(Action<string, Exception> action, long policyId, RuleContentDTO ruleContent)
        {
            var url = string.Format(PMSClientConfig.BaseApiAddress+ "Policies/" + policyId+"/RuleCompilations");
            WebClientHelper.Post(new Uri(url, PMSClientConfig.UriKind), action, ruleContent, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetRuleExcuteTimes(Action<List<RuleExcuteTimeDTO>, Exception> action)
        {
            var execList = new List<RuleExcuteTimeDTO>
                {
                    new RuleExcuteTimeDTO(){Id=1,Name="قبل از محاسبه"},
                     new RuleExcuteTimeDTO(){Id=2,Name="در هر محاسبه"},
                      new RuleExcuteTimeDTO(){Id=3,Name=" بعد از محاسبه"},
                };
            action(execList, null);
        }

        public void GetAllRuleTrails(Action<PageResultDTO<RuleTrailDTOWithAction>, Exception> action, long policyId, long ruleId, int pageSize, int pageIndex)
        {
            var url = PMSClientConfig.BaseApiAddress + "Policies/" + policyId + "/Rules/" + ruleId + "/Trails" + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex;
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetRuleTrail(Action<RuleTrailDTO, Exception> action, long policyId, long ruleId, long trailId)
        {
            var url = PMSClientConfig.BaseApiAddress + "Policies/" + policyId + "/Rules/" + ruleId + "/Trails/" +
                      trailId; 
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }
    }
}
