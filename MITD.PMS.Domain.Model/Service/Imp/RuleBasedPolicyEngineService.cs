using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;
using MITD.Core.RuleEngine;
using System;
using System.Linq;
using System.Security;
using System.Reflection;
using System.Security.Permissions;
using System.Collections.Generic;
using MITD.PMS.Domain.Service;
using MITD.Core.RuleEngine.Model;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model;
using MITD.Core;
using MITD.PMS.Domain.Model.Policies;

namespace MITD.PMS.Domain.Service
{
    public partial class RuleBasedPolicyEngineService : IRuleEngineBasedPolicyService
    {
        private RuleEngineAdapter ruleEngine;
        private AppDomain domain;
        private bool hasBeenSetup = false;
        private Dictionary<string, RuleBase> ruleClasses;
        private IReadOnlyList<RuleFunctionId> ruleFunctions;
        private IServiceLocatorProvider locatorProvider;
        private bool preCalculationDone = false;

        public RuleBasedPolicyEngineService(IServiceLocatorProvider locatorProvider, IEventPublisher publisher)
        {
            this.locatorProvider = locatorProvider;
        }

        public IReadOnlyList<RuleFunctionId> RuleFunctions { get { return ruleFunctions; } }

        public void SetupForCalculation(Policy policy, IEventPublisher publisher)
        {
            SetupForCalculation(policy as RuleEngineBasedPolicy,publisher);
        }


        public void SetupForCalculation(RuleEngineBasedPolicy policy, IEventPublisher publisher)
        {
            domain = setupAppDomain();
            ruleEngine = createRuleEngine();
            ruleFunctions = policy.RuleFunctions;

            var ruleDic = policy.Rules.ToDictionary(x => "Rule" + x.Id);
            ruleClasses = ruleEngine.SetupForCalculation("MITD.Core.RuleEngine",ruleDic , ruleFunctions);
            if (!string.IsNullOrEmpty(ruleEngine.CompileResult.Message))
                throw new Exception(ruleEngine.CompileResult.Message);
            hasBeenSetup = true;
            preCalculationDone = false;
            publisher.Publish(new RulesCompiled(ruleEngine.CompileResult, ruleClasses));
        }

        private RuleEngineAdapter createRuleEngine()
        {
            return
                (RuleEngineAdapter)domain.CreateInstanceAndUnwrap(
                Assembly.GetAssembly(typeof(MITD.Core.RuleEngine.RuleEngineAdapter)).GetName().FullName,
                typeof(MITD.Core.RuleEngine.RuleEngineAdapter).FullName, false, BindingFlags.Default, null,
                new object[] { locatorProvider }, null, null);
        }

        private AppDomain setupAppDomain()
        {
            AppDomainSetup ads = new AppDomainSetup();
            ads.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            ads.ApplicationTrust = AppDomain.CurrentDomain.ApplicationTrust;
            ads.PrivateBinPath = "bin";
            ads.DisallowBindingRedirects = false;
            ads.DisallowCodeDownload = true;
            ads.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            return AppDomain.CreateDomain("Remote Load", AppDomain.CurrentDomain.Evidence, ads,
                new PermissionSet(PermissionState.Unrestricted));
        }

        public CalculationPointPersistanceHolder CalculateFor(Employee employee, Period period, Calculation calculation, ICalculationDataProvider calculationDataProvider, CalculatorSession calculationSession)
        {
            if (!hasBeenSetup)
                throw new PolicyEngineHasNotBeenSetupException();
            if (!preCalculationDone)
            {
                var lst = ruleClasses.Where(x => x.Value.RuleType == RuleType.PreCalculation);
                foreach (var item in lst)
                {
                    ruleEngine.ExecuteRule(item.Key);
                }
                preCalculationDone = true;
            }
            
            ruleEngine.Clear();
            PMSReport.Domain.Model.CalculationData employeeData;
            var calculationData = calculationDataProvider.Provide(employee, out employeeData, calculation, true, calculationSession);
            var rules = ruleClasses.Where(x => x.Value.RuleType == RuleType.PerCalculation);

            foreach (var item in rules)
            {
                ruleEngine.ExecuteRule(item.Key, calculationData);
            }

            var res = ruleEngine.GetResult<PMS.RuleContracts.RuleResult>();

            rules = ruleClasses.Where(x => x.Value.RuleType == RuleType.PostCalculation);
            foreach (var item in rules)
            {
                ruleEngine.ExecuteRule(item.Key);
            }

            return calculationDataProvider.Convert(res,employeeData,employee,period,calculation);
        }


        public bool HasBeenSetup
        {
            get { return hasBeenSetup; }
        }


        ~RuleBasedPolicyEngineService()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (ruleEngine != null)
                    ruleEngine.Dispose();
                if (domain != null)
                    AppDomain.Unload(domain);
            }

        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
