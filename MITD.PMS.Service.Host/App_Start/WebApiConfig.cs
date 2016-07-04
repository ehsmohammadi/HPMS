using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Validation.Providers;
using System.Web.Routing;
using Newtonsoft.Json;
using Thinktecture.IdentityModel.Tokens.Http;
using System.IdentityModel.Tokens;
using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using MITD.PMS.Service.Host.App_Start;

namespace MITD.PMS.Service.Host
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var authConfig = new AuthenticationConfiguration
            {
                RequireSsl = false,
                ClaimsAuthenticationManager = new ClaimsTransformer(),
                EnableSessionToken = true,
            };
            var registry = new ConfigurationBasedIssuerNameRegistry();
            registry.AddTrustedIssuer(System.Configuration.ConfigurationManager.AppSettings["SigningThumbPrint"], System.Configuration.ConfigurationManager.AppSettings["IssuerURI"] );
            var handlerConfig = new SecurityTokenHandlerConfiguration();
            handlerConfig.AudienceRestriction.AllowedAudienceUris.Add(new Uri(System.Configuration.ConfigurationManager.AppSettings["AudianceUri"]));
            handlerConfig.IssuerNameRegistry = registry;
            handlerConfig.CertificateValidator = X509CertificateValidator.None;
            handlerConfig.ServiceTokenResolver = new X509CertificateStoreTokenResolver(StoreName.My, StoreLocation.LocalMachine);
            authConfig.AddSaml2(handlerConfig, AuthenticationOptions.ForAuthorizationHeader("SAML"), AuthenticationScheme.SchemeOnly("SAML"));
            
            config.MessageHandlers.Add(new AuthenticationHandler(authConfig));

            config.Routes.MapHttpRoute(
                name: "UsersCurrentPermittedUser",
                routeTemplate: "api/Users/{logonUserName}/CurrentPermittedUser",
                defaults: new {Controller = "UsersCurrentPermittedUser", id = RouteParameter.Optional});
               //constraints:new {httpMethod = new System.Web.Http.Routing.HttpMethodConstraint(HttpMethod.Get,HttpMethod.Post)});
            

            config.Routes.MapHttpRoute(
               name: "Calculations",
               routeTemplate: "api/Periods/{PeriodId}/Calculations/{id}",
               defaults: new { Controller = "Calculations", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
               name: "CalculationsState",
               routeTemplate: "api/Periods/{PeriodId}/Calculations/{id}/State",
               defaults: new { Controller = "CalculationsState", id = RouteParameter.Optional });
            
            config.Routes.MapHttpRoute(
              name: "CalculationsExceptions",
              routeTemplate: "api/Periods/{PeriodId}/Calculations/{calculationId}/Exceptions/{id}",
              defaults: new { Controller = "CalculationsExceptions", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
               name: "JobIndexPoints",
               routeTemplate: "api/Periods/{PeriodId}/Calculations/{CalculationId}/JobIndexpoints/{id}",
               defaults: new { Controller = "JobIndexPoints", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "JobCustomFields",
                routeTemplate: "api/Jobs/{jobId}/CustomFields/{id}",
                defaults: new { Controller = "JobCustomFields", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
               name: "PeriodState",
               routeTemplate: "api/Periods/{id}/State",
               defaults: new { Controller = "PeriodsState", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
               name: "PeriodSourceDestinationState",
               routeTemplate: "api/SourcePeriods/{sourcePeriodId}/DestinationPeriods/{destinationPeriodId}/State",
               defaults: new { Controller = "PeriodSourceDestinationState", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "PolicyFunctions",
                routeTemplate: "api/Policies/{PolicyId}/Functions/{id}",
                defaults: new { Controller = "PolicyFunctions", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "PolicyRules",
                routeTemplate: "api/Policies/{PolicyId}/Rules/{id}",
                defaults: new { Controller = "PolicyRules", id = RouteParameter.Optional });
            
            config.Routes.MapHttpRoute(
               name: "PolicyRuleTrails",
               routeTemplate: "api/Policies/{policyId}/Rules/{ruleId}/Trails/{id}",
               defaults: new { Controller = "PolicyRuleTrails", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "PolicyRuleCompilations",
                routeTemplate: "api/Policies/{PolicyId}/RuleCompilations/{id}",
                defaults: new { Controller = "RuleCompilations", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
               name: "PeriodUnits",
               routeTemplate: "api/Periods/{PeriodId}/Units/{id}",
               defaults: new { Controller = "PeriodUnits", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
               name: "UnitInquirySubjectsController",
               routeTemplate: "api/Periods/{PeriodId}/Units/{UnitId}/InquirySubjects",
               defaults: new { Controller = "UnitInquirySubjects", id = RouteParameter.Optional });


            config.Routes.MapHttpRoute(
              name: "InquirerInquiryUnits",
              routeTemplate: "api/Periods/{PeriodId}/Inquirers/{InquirerEmployeeNo}/InquiryUnits",
              defaults: new { Controller = "InquirerInquiryUnits", id = RouteParameter.Optional });

              
            config.Routes.MapHttpRoute(
               name: "EmployeeUnits",
               routeTemplate: "api/Periods/{PeriodId}/Employees/{EmployeeNo}/Units",
               defaults: new { Controller = "EmployeeUnits", id = RouteParameter.Optional });
         
            config.Routes.MapHttpRoute(
               name: "InquiryUnitInquiryUnitIndexPoints",
               routeTemplate:
                   "api/Periods/{PeriodId}/Inquirers/{InquirerEmployeeNo}/Units/{unitId}/InquiryUnitIndexPoints",
               defaults: new { Controller = "InquiryUnitInquiryUnitIndexPoints", id = RouteParameter.Optional });



            config.Routes.MapHttpRoute(
               name: "PeriodEmployees",
               routeTemplate: "api/Periods/{PeriodId}/Employees/{id}",
               defaults: new { Controller = "PeriodEmployees", id = RouteParameter.Optional });

        
            
            config.Routes.MapHttpRoute(
               name: "EmployeeJobPositions",
               routeTemplate: "api/Periods/{PeriodId}/Employees/{EmployeeNo}/JobPositions",
               defaults: new { Controller = "EmployeeJobPositions", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
               name: "PeriodJobPositions",
               routeTemplate: "api/Periods/{PeriodId}/JobPositions/{id}",
               defaults: new { Controller = "PeriodJobPositions", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
               name: "PeriodJobIndex",
               routeTemplate: "api/Periods/{PeriodId}/JobIndices/{id}",
               defaults: new { Controller = "PeriodJobIndex", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
              name: "PeriodUnitIndex",
              routeTemplate: "api/Periods/{PeriodId}/UnitIndices/{id}",
              defaults: new { Controller = "PeriodUnitIndex", id = RouteParameter.Optional });


            config.Routes.MapHttpRoute(
              name: "PeriodJobs",
              routeTemplate: "api/Periods/{PeriodId}/Jobs/{id}",
              defaults: new { Controller = "PeriodJobs", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
               name: "JobIndexCustomFields",
               routeTemplate: "api/JobIndices/{jobindexid}/CustomFields/{id}",
               defaults: new { Controller = "CustomFields", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "JobPositionJobs",
                routeTemplate: "api/Periods/{PeriodId}/JobPositions/{JobPositionId}/Jobs",
                defaults: new { Controller = "JobPositionJobs", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "JobPositionInquirySubjects",
                routeTemplate: "api/Periods/{PeriodId}/JobPositions/{JobPositionId}/InquirySubjects",
                defaults: new {Controller = "JobPositionInquirySubjects", id = RouteParameter.Optional});

            config.Routes.MapHttpRoute(
               name: "InquirySubjectInquirers",
               routeTemplate: "api/Periods/{PeriodId}/JobPositions/{JobPositionId}/InquirySubjects/{InquirySubjectEmployeeNo}/Inquirers",
               defaults: new { Controller = "InquirySubjectInquirers", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
               name: "InquirerInquirySubjects",
               routeTemplate: "api/Periods/{PeriodId}/Inquirers/{InquirerEmployeeNo}/InquirySubjects",
               defaults: new { Controller = "InquirerInquirySubjects", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
               name: "InquirerInquiryIndices",
               routeTemplate: "api/Periods/{PeriodId}/Inquirers/{InquirerEmployeeNo}/InquiryIndices",
               defaults: new { Controller = "InquirerInquiryIndices", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "InquirySubjectJobPositionInquiryJobIndexPoints",
                routeTemplate:
                    "api/Periods/{PeriodId}/Inquirers/{InquirerEmployeeNo}/InquirySubjects/{InquirySubjectEmployeeNo}/JobPositions/{JobPositionId}/InquiryJobIndexPoints",
                defaults: new { Controller = "InquirySubjectJobPositionInquiryJobIndexPoints", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
               name: "PeriodClaims",
               routeTemplate: "api/Periods/{PeriodId}/Claims/{id}",
               defaults: new { Controller = "PeriodClaims", id = RouteParameter.Optional });

              config.Routes.MapHttpRoute(
               name: "PeriodClaimStates",
               routeTemplate: "api/Periods/{PeriodId}/ClaimStates",
               defaults: new { Controller = "PeriodClaimStates", id = RouteParameter.Optional });
              config.Routes.MapHttpRoute(
                name: "PeriodClaimTypes",
                routeTemplate: "api/Periods/{PeriodId}/ClaimTypes",
                defaults: new { Controller = "PeriodClaimTypes", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Formatters.Add(new PlainTextFormatter());
            config.Filters.Add(new GlobalExceptionFilterAttribute());
            config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            config.Formatters.JsonFormatter.SerializerSettings.TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
             = ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling
                = PreserveReferencesHandling.Objects;
            config.Services.RemoveAll(typeof(System.Web.Http.Validation.ModelValidatorProvider), v => v is InvalidModelValidatorProvider);
        }
    }
}
