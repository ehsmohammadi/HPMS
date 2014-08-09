using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MITD.Core;
using MITD.Core.Exceptions;
using MITD.PMS.Presentation.Contracts;
using Newtonsoft.Json.Linq;

namespace MITD.PMS.Presentation.Logic
{
    public static class ExceptionAdapter
    {
        public static ApplicationException Convert(Exception exp, IMainAppLocalizedResources localizedResources)
        {

            var message = exp.Message;
            if (string.IsNullOrWhiteSpace(message))
                return new ApplicationException(localizedResources.UnExpectedException);
            var json = JObject.Parse(message);

            var dic = json.ToDictionary();
            var convertedException=ExceptionConvertorService.ConvertBack(dic.ToDictionary(c => c.Key, k => k.Value.ToString()));

            if (convertedException == null||!(convertedException is IException))
            {
                return new ApplicationException(localizedResources.UnExpectedException); 
            }
            
            var expCode = (convertedException as IException).Code.ToString();
             

            if (convertedException is IDuplicateException)
            {
                var exception = (convertedException as IDuplicateException);
                var objectName = exception.DomainObjectName;
                var propName = exception.PropertyName ;
                var msg = string.Format(localizedResources.DuplicateException, expCode,
                    getFromLocalizedResource(objectName, localizedResources),
                    getFromLocalizedResource(propName, localizedResources));
                return new ApplicationException(msg);
            }

            if (convertedException is IDeleteException)
            {
                var exception = (convertedException as IDeleteException);
                var objectName = exception.DomainObjectName;
                var relatedObjName = exception.RelatedObjectName;
                var msg =
                    string.Format(
                       localizedResources.DeleteException,
                        expCode, getFromLocalizedResource(objectName, localizedResources),
                        getFromLocalizedResource(relatedObjName, localizedResources));
                return new ApplicationException(msg);
            }

            if (convertedException is ICompareException)
            {
                var exception = (convertedException as ICompareException);
                var objectName = exception.DomainObjectName ;
                var propertyNameSource = exception.PropertyNameSource ;
                var propertyNameCompare = exception.PropertyNameCompare ;
                var msg = string.Format(localizedResources.CompareException, expCode,
                    getFromLocalizedResource(objectName, localizedResources),
                    getFromLocalizedResource(propertyNameSource, localizedResources),
                    getFromLocalizedResource(propertyNameCompare, localizedResources));
                return new ApplicationException(msg);
            }

            if (convertedException is IModifyException)
            {
                var exception = (convertedException as IModifyException);
                var objectName = exception.DomainObjectName ;
                var propertyName = exception.PropertyName ;
                var msg = string.Format(localizedResources.ModifyForbiddenException, expCode,
                    getFromLocalizedResource(objectName, localizedResources),
                    getFromLocalizedResource(propertyName, localizedResources));
                return new ApplicationException(msg);
            }

            if (convertedException is IInvalidStateOperationException)
            {
                var exception = (convertedException as IInvalidStateOperationException);
                var objectName = exception.DomainObjectName ;
                var stateName = exception.StateName ;
                var operationName = exception.OperationName ;
                var msg = string.Format(localizedResources.InvalidStateOperationException, expCode,
                    getFromLocalizedResource(objectName, localizedResources),
                    getFromLocalizedResource(stateName, localizedResources),
                    getFromLocalizedResource(operationName, localizedResources));
                return new ApplicationException(msg);
            }

            if (convertedException is IArgumentException)
            {
                var exception = (convertedException as IArgumentException);
                var objectName = exception.DomainObjectName ;
                var argumentName = exception.ArgumentName;
                var msg = string.Format(localizedResources.InvalidArgumentException, expCode,
                    getFromLocalizedResource(objectName, localizedResources),
                    getFromLocalizedResource(argumentName, localizedResources));
                return new ApplicationException(msg);
            }

            
            if (expCode.ToString() == ApiExceptionCode.UnknownSecurityException.Value)
            {
                var msg = string.Format("Security Exception ", expCode);
                return new ApplicationException(msg);
            }

            var msgExp = getErrorMessageFromLocalResources(expCode.ToString(),localizedResources);
            msgExp = string.Format("خطای {0} ،", expCode) + msgExp;
            return new ApplicationException(msgExp);

        }

        private static string getFromLocalizedResource(string name, IMainAppLocalizedResources localizedResources)
        {
            var property = localizedResources.GetType().GetProperty(name);
            if (property == null)
                return name;
            var res = property.GetValue(localizedResources, null);
            return res as string;
        }

        private static string getErrorMessageFromLocalResources(string expCode, IMainAppLocalizedResources localizedResources)
        {
            if (expCode == ApiExceptionCode.CouldNotActivatePeriodWhileExistsAnotherActivePeriod.Value)
                return localizedResources.CouldNotActivatePeriodWhileExistsAnotherActivePeriod;
            if (expCode == ApiExceptionCode.CouldNotClosePeriodWithOpenClaims.Value)
                return localizedResources.CouldNotClosePeriodWithOpenClaims;
            if (expCode == ApiExceptionCode.CouldNotClosePeriodWithOpenClaims.Value)
                return localizedResources.CouldNotClosePeriodWithOpenClaims;
            if (expCode == ApiExceptionCode.CouldNotClosePeriodWithoutAnyDeterministicCalculation.Value)
                return localizedResources.CouldNotClosePeriodWithoutAnyDeterministicCalculation;
            if (expCode == ApiExceptionCode.CouldNotCompleteInquiryWithNotFilledInquiryForms.Value)
                return localizedResources.CouldNotCompleteInquiryWithNotFilledInquiryForms;
            if (expCode == ApiExceptionCode.CouldNotDeleteClaimByAnotherUser.Value)
                return localizedResources.CouldNotDeleteClaimByAnotherUser;
            if (expCode == ApiExceptionCode.CouldNotDeleteDeterministicCalculation.Value)
                return localizedResources.CouldNotDeleteDeterministicCalculation;
            if (expCode == ApiExceptionCode.CouldNotInitializeInquiryForInactivePeriod.Value)
                return localizedResources.CouldNotInitializeInquiryForInactivePeriod;
            if (expCode == ApiExceptionCode.CouldNotModifyJobIndicesInInquiryStartedState.Value)
                return localizedResources.CouldNotModifyJobIndicesInInquiryStartedState;
            if (expCode == ApiExceptionCode.CouldNotStartClaimingWithoutAnyDeterministicCalculation.Value)
                return localizedResources.CouldNotStartClaimingWithoutAnyDeterministicCalculation;
            if (expCode == ApiExceptionCode.DoesNotExistAnyActivePeriod.Value)
                return localizedResources.DoesNotExistAnyActivePeriod;
            if (expCode == ApiExceptionCode.DoesNotExistAnyDeterministicCalculationInPeriod.Value)
                return localizedResources.DoesNotExistAnyDeterministicCalculationInPeriod;
            if (expCode == ApiExceptionCode.DoesNotExistEvaluationForEmployee.Value)
                return localizedResources.DoesNotExistEvaluationForEmployee;
            if (expCode == ApiExceptionCode.ExceedViolationInDeterministicCalculationInPeriod.Value)
                return localizedResources.ExceedViolationInDeterministicCalculationInPeriod;
            if (expCode == ApiExceptionCode.InvalidSumEmployeeWorkTimePercents.Value)
                return localizedResources.InvalidSumEmployeeWorkTimePercents;
            if (expCode == ApiExceptionCode.InvalidUsernameOrPassword.Value)
                return localizedResources.InvalidUsernameOrPassword;
            if (expCode == ApiExceptionCode.UnauthorizedAccessToOperation.Value)
                return localizedResources.UnauthorizedAccessToOperation;

            return localizedResources.UnExpectedException;



        }
    }
}
