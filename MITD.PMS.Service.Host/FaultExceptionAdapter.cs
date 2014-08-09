using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using MITD.Core;
using MITD.Core.RuleEngine.Exceptions;
using MITD.Domain.Model;
using MITD.PMS.Exceptions;
using MITD.PMSAdmin.Exceptions;
using MITD.PMSSecurity.Exceptions;
using MITD.Services;

namespace MITD.PMS.Calculation.Contracts
{
    public class CalculationFaultExceptionAdapter : IFaultExceptionAdapter
    {
        public Exception ConvertToException(FaultException exception)
        {
            if (exception is FaultException<ErrorDetail>)
            {
                var er = exception as FaultException<ErrorDetail>;
                return ExceptionConvertorService.ConvertBack(er.Detail.Messages) as Exception;
            }
            return exception;

        }

        public FaultException ConvertToFault(Exception exp)
        {
            var dic = ExceptionConvertorService.Convert(exp);

            return new FaultException<ErrorDetail>(new ErrorDetail(dic), new FaultReason(exp.Message));

        }
    }
}