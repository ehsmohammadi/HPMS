using System;
using MITD.Core;


namespace MITD.Core
{
    public class ApiExceptionCode:Enumeration
    {
        public static ApiExceptionCode Unknown = new ApiExceptionCode("100", "unexpected exception occured");
        public static ApiExceptionCode Duplicated = new ApiExceptionCode("101", "Duplicated");
        public static ApiExceptionCode DeleteFailed = new ApiExceptionCode("102", "DeleteFailed");
        public static ApiExceptionCode InvalidArgument = new ApiExceptionCode("103", "InvalidArgument");
        public static ApiExceptionCode ModifyForbidden = new ApiExceptionCode("105", "ModifyForbidden");
        public static ApiExceptionCode InvalidCompare = new ApiExceptionCode("105", "InvalidCompare");
        public static ApiExceptionCode InvalidStateOperation = new ApiExceptionCode("124", "Invalid Operation on State");

        public static ApiExceptionCode DoesNotExistEvaluationForEmployee = new ApiExceptionCode("106", "Does not exist evaluation for employee");
        public static ApiExceptionCode CouldNotInitializeInquiryForInactivePeriod = new ApiExceptionCode("107", "Could not initialize inquiry , period must be activated before Initialize Inquiry");
        public static ApiExceptionCode CouldNotStartClaimingWithoutAnyDeterministicCalculation = new ApiExceptionCode("108", "Could not start claiming  without any deterministic calculation");
        public static ApiExceptionCode CouldNotCompleteInquiryWithNotFilledInquiryForms = new ApiExceptionCode("109", "Could not completeInquiry with not filled inquiry forms");
        public static ApiExceptionCode CouldNotModifyJobIndicesInInquiryStartedState = new ApiExceptionCode("110", "Could not modify jobIndices in inquiryStarted state");
        
        public static ApiExceptionCode ExceedViolationInDeterministicCalculationInPeriod = new ApiExceptionCode("111", "just one calculation can be deteministic in each period ");
        public static ApiExceptionCode DoesNotExistAnyDeterministicCalculationInPeriod = new ApiExceptionCode("112", "Does not exist any deterministic calculation in period");
        
        public static ApiExceptionCode CouldNotDeleteClaimByAnotherUser = new ApiExceptionCode("113", "could not delete claim by another user, only claim owner can  delete it ");
        public static ApiExceptionCode DoesNotExistAnyActivePeriod = new ApiExceptionCode("114", "Does not exist any active period ");
        public static ApiExceptionCode CouldNotActivatePeriodWhileExistsAnotherActivePeriod = new ApiExceptionCode("115", "Could not activate period while exists another active period");
        public static ApiExceptionCode CouldNotClosePeriodWithoutAnyDeterministicCalculation = new ApiExceptionCode("116", "Could not close period without any deterministic calculation ");
        public static ApiExceptionCode CouldNotClosePeriodWithOpenClaims = new ApiExceptionCode("117", "Could not close period with open claims");

        public static ApiExceptionCode CouldNotDeleteDeterministicCalculation = new ApiExceptionCode("118", "Could not delete deterministic calculation , you can change calculation state to nonDeterministic and then delete it ");
        public static ApiExceptionCode InvalidSumEmployeeWorkTimePercents = new ApiExceptionCode("119", "Invalid sum of employee workTime percents");

        public static ApiExceptionCode InvalidClaimState = new ApiExceptionCode("120", "Invalid claim state");

        public static ApiExceptionCode UnauthorizedAccessToOperation = new ApiExceptionCode("121", "you are not authorized to access to this operation");
        public static ApiExceptionCode InvalidUsernameOrPassword = new ApiExceptionCode("122", "Invalid username or password");
        
        public static ApiExceptionCode UnknownSecurityException = new ApiExceptionCode("123", "Operation failed,unexpected security exception occured");
       
        

        public ApiExceptionCode(string value) : base(value)
        {
        }

        public ApiExceptionCode(string value, string displayName) : base(value, displayName)
        {
        }

        public static explicit operator int(ApiExceptionCode x)
        {
            if (x == null)
                throw new InvalidCastException();
            return Convert.ToInt32(x.Value);
        }
        public static implicit operator ApiExceptionCode(int val)
        {
            return FromValue<ApiExceptionCode>(val.ToString());
        }


        
    }
}
