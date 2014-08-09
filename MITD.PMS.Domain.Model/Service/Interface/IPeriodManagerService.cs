using System.Collections.Generic;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;
using MITD.Core;

namespace MITD.PMS.Domain.Service
{
    public interface IPeriodManagerService : IService
    {
      

        bool CanActivate(Period period);
        void InitializeInquiry(Period period);
        bool CanCompleteInquiry(Period period);
        InquiryInitializingProgress GetInitializeInquiryProgress(Period period);
        InquiryInitializingProgress GetCompletedInitializeInquiryProgress(Period period);
        Period GetCurrentPeriod();

        void CheckUpdatingJobIndex(JobIndex jobIndex);
        void CheckModifyingJobCustomFields(Job job);
        void CheckModifyingJobIndices(Job job);
        void CheckModifyingJobPositionInquirers(Employee inquirySubject);
        void CheckModifyingEmployeeCustomFieldsAndValues(Employee employee);
        void CheckModifyingEmployeeJobPositions(Employee employee);
        void CheckShowingInquirySubject(Employee inquirer);
        void CheckShowingInquiryJobIndexPoint(JobPosition jobPosition);
        void CheckSettingInquiryJobIndexPointValueValue(InquiryJobIndexPoint inquiryJobIndexPoint);
        void CopyBasicData(Period currentPeriod, Period sourcePeriod);
        void DeleteBasicData(Period period);
        BasicDataCopyingProgress GetCopyingStateProgress(Period period);
        BasicDataCopyingProgress GetBasicDataCopyStatus(Period period);
        bool AllowRollBackToInquiryCompletedState(Period period);
        void DeleteAllCalculations(Period period);
        void ResetAllInquiryPoints(Period period);
        void DeleteAllInquiryConfigurations(Period period);
        void ChangeActiveStatus(Period period, bool activeStatus);
        void DeleteAllCalims(Period period);
        bool HasDeterministicCalculation(Period period);
        bool HasOpenClaim(Period period);
    }
}
