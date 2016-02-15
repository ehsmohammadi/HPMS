using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Windows.Input;
using MITD.PMS.Interface;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Contracts.Fasade;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Model.AccessPermissions;

namespace MITD.PMS.Service.Host
{
    public class AccessPermissionSetup
    {

        public void Execute(AccessPermission accessPermission,int numberIfacade)
        {
          //  var periodCatalog = new PermissionCatalog(typeof(IPeriodServiceFacade));
          //  periodCatalog.AddPermission(new Permission("AddPeriod", new List<ActionType>() {ActionType.AddPeriod}));
          //  periodCatalog.AddPermission(new Permission("UpdatePeriod", new List<ActionType>() {ActionType.ModifyPeriod}));
          //  periodCatalog.AddPermission(new Permission("DeletePeriod", new List<ActionType>() {ActionType.DeletePeriod}));
          //  periodCatalog.AddPermission(new Permission("ChangePeriodState", new List<ActionType>() { 
          //          ActionType.ActivatePeriod,
          //          ActionType.InitializePeriodForInquiry,
          //          ActionType.StartInquiry,
          //          ActionType.CompleteInquiry,
          //          ActionType.ClosePeriod}));
          //  accessPermission.AddCatalog(periodCatalog);

          //  var periodUnitCatalog = new PermissionCatalog(typeof(IPeriodUnitServiceFacade));
          //      periodCatalog.AddPermission(new Permission("GetUnitsWithActions", new List<ActionType>() {ActionType.ManageUnits}));
          //      periodCatalog.AddPermission(new Permission("AssignUnit", new List<ActionType>() {ActionType.AddUnitInPeriod}));
          //      periodCatalog.AddPermission(new Permission("RemoveUnit", new List<ActionType>() {ActionType.DeleteUnitInPeriod}));
          //   accessPermission.AddCatalog(periodUnitCatalog);

          //  var periodJobPositionCatalog = new PermissionCatalog(typeof(IPeriodJobPositionServiceFacade));
          //      periodJobPositionCatalog.AddPermission(new Permission("GetJobPositionsWithActions", new List<ActionType>()    {ActionType.ManageJobPositions}));
          //      periodJobPositionCatalog.AddPermission(new Permission("AssignJobPosition", new List<ActionType>()    {ActionType.AddJobPositionInPeriod}));
          //      periodJobPositionCatalog.AddPermission(new Permission("RemoveJobPosition", new List<ActionType>()    {ActionType.DeleteJobPositionInPeriod}));
          //      periodJobPositionCatalog.AddPermission(new Permission("GetInquirySubjectsWithInquirers", new List<ActionType>()    {ActionType.ManageJobPositionInPeriodInquiry}));
          //      periodJobPositionCatalog.AddPermission(new Permission("UpdateInquirySubjectInquirers", new List<ActionType>(){ActionType.ManageJobPositionInPeriodInquiry}));
          //  accessPermission.AddCatalog(periodJobPositionCatalog);
            
          // var periodJobIndexCatalog = new PermissionCatalog(typeof(IPeriodJobIndexServiceFacade));
          //     periodJobIndexCatalog.AddPermission(new Permission("GetAllAbstractJobIndices", new List<ActionType>() {ActionType.ManageJobIndices}));
          //     periodJobIndexCatalog.AddPermission(new Permission("AddJobIndex", new List<ActionType>() {ActionType.AddJobIndexInPeriod}));
          //     periodJobIndexCatalog.AddPermission(new Permission("UpdateJobIndex", new List<ActionType>() {ActionType.ModifyJobIndexInPeriod}));
          //     periodJobIndexCatalog.AddPermission(new Permission("DeleteAbstractJobIndex", new List<ActionType>() {ActionType.DeleteJobIndexInPeriod, ActionType.DeleteJobIndexGroupInPeriod}));
          //     periodJobIndexCatalog.AddPermission(new Permission("AddJobIndexGroup", new List<ActionType>() {ActionType.AddJobIndexGroupInPeriod}));
          //     periodJobIndexCatalog.AddPermission(new Permission("UpdateJobIndexGroup", new List<ActionType>() {ActionType.ModifyJobIndexGroupInPeriod}));
          // accessPermission.AddCatalog(periodJobIndexCatalog);
           
          //var periodJobCatalog = new PermissionCatalog(typeof(IPeriodJobServiceFacade));
          //    periodJobCatalog.AddPermission(new Permission("GetAllJobs", new List<ActionType>() {ActionType.ManageJobInPeriod}));
          //    periodJobCatalog.AddPermission(new Permission("AssignJob", new List<ActionType>() {ActionType.AddJobInPeriod}));
          //    periodJobCatalog.AddPermission(new Permission("UpdateJob", new List<ActionType>() {ActionType.ModifyJobInPeriod}));
          //    periodJobCatalog.AddPermission(new Permission("RemoveJob", new List<ActionType>() {ActionType.DeleteJobInPeriod}));
          //accessPermission.AddCatalog(periodJobCatalog);
    
          //var calculationCatalog = new PermissionCatalog(typeof(ICalculationServiceFacade));
          //    calculationCatalog.AddPermission(new Permission("GetAllCalculations", new List<ActionType>() {ActionType.ManageCalculations}));
          //    calculationCatalog.AddPermission(new Permission("AddCalculation", new List<ActionType>() {ActionType.AddCalculation}));
          //    calculationCatalog.AddPermission(new Permission("DeleteCalculation", new List<ActionType>() {ActionType.DeleteCalculation}));
          //    calculationCatalog.AddPermission(new Permission("ChangeCalculationState", new List<ActionType>() { 
          //          ActionType.ModifyCalculation,
          //          ActionType.RunCalculation,
          //          ActionType.StopCalculation,
          //          ActionType.SetDeterministicCalculation}));
          //accessPermission.AddCatalog(calculationCatalog);
      
          //var employeeCatalog = new PermissionCatalog(typeof(IEmployeeServiceFacade));
          //    employeeCatalog.AddPermission(new Permission("GetAllEmployees", new List<ActionType>() {ActionType.ManageEmployees}));
          //    employeeCatalog.AddPermission(new Permission("AddEmployee", new List<ActionType>() {ActionType.AddEmployee}));
          //    employeeCatalog.AddPermission(new Permission("UpdateEmployee", new List<ActionType>() {ActionType.ModifyEmployee}));
          //    employeeCatalog.AddPermission(new Permission("DeleteEmployee", new List<ActionType>() {ActionType.DeleteEmployee}));
          //    employeeCatalog.AddPermission(new Permission("AssignJobPositionsToEmployee", new List<ActionType>() {ActionType.ManageEmployeeJobPositions}));
          //accessPermission.AddCatalog(employeeCatalog);
            
          //var jobIndexCatalog = new PermissionCatalog(typeof(IJobIndexFacadeService));
          //    jobIndexCatalog.AddPermission(new Permission("AddJobIndex", new List<ActionType>() {ActionType.AddJobIndex}));
          //    jobIndexCatalog.AddPermission(new Permission("UpdateJobIndex", new List<ActionType>() {ActionType.ModifyJobIndex, ActionType.ManageJobIndexCustomFields}));
          //    jobIndexCatalog.AddPermission(new Permission("DeleteAbstractJobIndex", new List<ActionType>() {ActionType.DeleteJobIndexCategory}));
          //    jobIndexCatalog.AddPermission(new Permission("AddJobIndexCategory", new List<ActionType>() {ActionType.AddJobIndexCategory}));
          //    jobIndexCatalog.AddPermission(new Permission("UpdateJobIndexCategory", new List<ActionType>() {ActionType.ModifyJobIndexCategory}));
          //  accessPermission.AddCatalog(jobIndexCatalog);
         
          //   var jobPositionCatalog = new PermissionCatalog(typeof(IJobPositionFacadeService));
          //       jobPositionCatalog.AddPermission(new Permission("AddJobPosition", new List<ActionType>() {ActionType.AddJobPosition}));
          //       jobPositionCatalog.AddPermission(new Permission("DeleteJob", new List<ActionType>() {ActionType.DeleteJobPosition}));
          //       jobPositionCatalog.AddPermission(new Permission("UpdateJobPosition", new List<ActionType>() {ActionType.ModifyJobPosition}));
          //   accessPermission.AddCatalog(jobPositionCatalog);
          
          //   var functionCatalog = new PermissionCatalog(typeof(IFunctionFacadeService));
          //       functionCatalog.AddPermission(new Permission("AddFunction", new List<ActionType>() {ActionType.CreateFunction}));
          //       functionCatalog.AddPermission(new Permission("DeleteFunction", new List<ActionType>() {ActionType.DeleteFunction}));
          //       functionCatalog.AddPermission(new Permission("UpdateFunction", new List<ActionType>() {ActionType.ModifyFunction}));
          //       functionCatalog.AddPermission(new Permission("GetPolicyFunctionsWithPagination", new List<ActionType>() {ActionType.ManageFunctions}));
          //  accessPermission.AddCatalog(functionCatalog);

          //  var customFieldCatalog = new PermissionCatalog(typeof(ICustomFieldFacadeService));
          //      customFieldCatalog.AddPermission(new Permission("AddCustomField", new List<ActionType>() {ActionType.CreateCustomField}));
          //      customFieldCatalog.AddPermission(new Permission("DeleteCustomeField", new List<ActionType>() {ActionType.DeleteCustomField}));
          //      customFieldCatalog.AddPermission(new Permission("UpdateCustomField", new List<ActionType>() {ActionType.ModifyCustomField}));
          //  accessPermission.AddCatalog(customFieldCatalog);

          //  var jobCatalog = new PermissionCatalog(typeof(IJobFacadeService));
          //      jobCatalog.AddPermission(new Permission("AddJob", new List<ActionType>() {ActionType.CreateJob}));
          //      jobCatalog.AddPermission(new Permission("DeleteJob", new List<ActionType>() {ActionType.DeleteJob}));
          //      jobCatalog.AddPermission(new Permission("UpdateJob", new List<ActionType>() {ActionType.ModifyJob, ActionType.ModifyJobCustomFields}));
          //   accessPermission.AddCatalog(jobCatalog);

          //    var unitCatalog = new PermissionCatalog(typeof(IUnitFacadeService));
          //        unitCatalog.AddPermission(new Permission("AddUnit", new List<ActionType>() {ActionType.AddUnit}));
          //        unitCatalog.AddPermission(new Permission("DeleteUnit", new List<ActionType>() {ActionType.DeleteUnit}));
          //        unitCatalog.AddPermission(new Permission("UpdateUnit", new List<ActionType>() {ActionType.ModifyUnit}));
          //  accessPermission.AddCatalog(unitCatalog);

          //  var policyCatalog = new PermissionCatalog(typeof(IPolicyFacadeService));
          //      policyCatalog.AddPermission(new Permission("AddPolicy", new List<ActionType>() {ActionType.AddPolicy}));
          //      policyCatalog.AddPermission(new Permission("DeletePolicy", new List<ActionType>() {ActionType.DeletePolicy}));
          //      policyCatalog.AddPermission(new Permission("UpdatePolicy", new List<ActionType>() {ActionType.ModifyPolicy}));
          //  accessPermission.AddCatalog(policyCatalog);

          //  var ruleCatalog = new PermissionCatalog(typeof(IRuleFacadeService));
          //      ruleCatalog.AddPermission(new Permission("GetPolicyRulesWithPagination", new List<ActionType>() {ActionType.ManageRules}));
          //      ruleCatalog.AddPermission(new Permission("AddRule", new List<ActionType>() {ActionType.AddRule}));
          //      ruleCatalog.AddPermission(new Permission("DeleteRule", new List<ActionType>() {ActionType.DeleteRule}));
          //      ruleCatalog.AddPermission(new Permission("UpdateRule", new List<ActionType>() {ActionType.ModifyRule}));
          //  accessPermission.AddCatalog(ruleCatalog);
         
        

          //  var jobIndexPointCatalog = new PermissionCatalog(typeof(IJobIndexPointFacadeService));
          //      jobIndexPointCatalog.AddPermission(new Permission("GetAllJobIndexPoints", new List<ActionType>() {ActionType.ShowCalculationResult}));
          //   accessPermission.AddCatalog(jobIndexPointCatalog);
  
          //  var inquiryCatalog = new PermissionCatalog(typeof(IInquiryServiceFacade));
          //      inquiryCatalog.AddPermission(new Permission("GetInquiryForm", new List<ActionType>() {ActionType.FillInquiryForm}));
          //      inquiryCatalog.AddPermission(new Permission("UpdateInquirySubjectForm", new List<ActionType>() {ActionType.FillInquiryForm}));    
          //  accessPermission.AddCatalog(inquiryCatalog);
         
          //  var periodClaimCatalog = new PermissionCatalog(typeof(IPeriodClaimServiceFacade));
          //      periodClaimCatalog.AddPermission(new Permission("AddClaim", new List<ActionType>() {ActionType.AddClaim}));
          //      periodClaimCatalog.AddPermission(new Permission("GetClaim", new List<ActionType>() {ActionType.ShowClaim}));
          //      periodClaimCatalog.AddPermission(new Permission("ChangeClaimState", new List<ActionType>() {ActionType.ReplyToClaim}));
          //      periodClaimCatalog.AddPermission(new Permission("DeleteClaim", new List<ActionType>() {ActionType.DeleteClaim}));
          //      periodClaimCatalog.AddPermission(new Permission("GetAllClaimsForAdminWithActions", new List<ActionType>() {ActionType.ShowAdminClaimList}));
          //  accessPermission.AddCatalog(periodClaimCatalog);

            if (numberIfacade != accessPermission.CatalogCount)
                throw new SecurityException("Count Of Catalog not valid");


        }
    }
}