using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.Core
{
    public class ExceptionCoverter : IExceptionConverter
    {
        public Dictionary<string, object> Convert(Exception exception)
        {
            var expData = new Dictionary<string, object>();
            expData.Add("Message", exception.Message);
            
            if (exception is IDuplicateException)
            {
                expData.Add("Type", typeof(IDuplicateException).Name);
                var exp = exception as IDuplicateException;
                expData.Add("Code", exp.Code);
                expData.Add("DomainObjectName", exp.DomainObjectName);
                expData.Add("PropertyName", exp.PropertyName);
                return expData;
            }
            if (exception is IDeleteException)
            {
                expData.Add("Type", typeof(IDeleteException).Name);
                var exp = exception as IDeleteException;
                expData.Add("Code", exp.Code);
                expData.Add("DomainObjectName", exp.DomainObjectName);
                expData.Add("RelatedObjectName", exp.RelatedObjectName);
                return expData;
            }
            if (exception is ICompareException)
            {
                expData.Add("Type", typeof(ICompareException).Name);
                var exp = exception as ICompareException;
                expData.Add("Code", exp.Code);
                expData.Add("DomainObjectName", exp.DomainObjectName);
                expData.Add("PropertyNameSource", exp.PropertyNameSource);
                expData.Add("PropertyNameCompare", exp.PropertyNameCompare);
                return expData;
            }
            if (exception is IModifyException)
            {
                expData.Add("Type", typeof(IModifyException).Name);
                var exp = exception as IModifyException;
                expData.Add("Code", exp.Code);
                expData.Add("DomainObjectName", exp.DomainObjectName);
                expData.Add("PropertyName", exp.PropertyName);
                return expData;
            }
            if (exception is IInvalidStateOperationException)
            {
                expData.Add("Type", typeof(IInvalidStateOperationException).Name);
                var exp = exception as IInvalidStateOperationException;
                expData.Add("Code", exp.Code);
                expData.Add("DomainObjectName", exp.DomainObjectName);
                expData.Add("StateName", exp.StateName);
                expData.Add("OperationName", exp.OperationName);
                return expData;
            }
            if (exception is IArgumentException)
            {
                expData.Add("Type", typeof(IArgumentException).Name);
                var exp = exception as IArgumentException;
                expData.Add("Code", exp.Code);
                expData.Add("DomainObjectName", exp.DomainObjectName);
                expData.Add("ArgumentName", exp.ArgumentName);
                return expData;
            }
            if (exception is IException)
            {
                expData.Add("Type", typeof(IException).Name);
                expData.Add("Code", (exception as IException).Code.ToString());
                return expData;
            }
            return expData;
        }

        public Exception ConvertBack(Dictionary<string, object> expData)
        {
            if (!expData.Keys.Contains("Type")||!expData.Keys.Contains("Message"))
                return new InvalidCastException();
            var exceptionType = expData["Type"].ToString();
            var message= expData["Message"].ToString();

            if (exceptionType == typeof (IDuplicateException).Name)
            {
                
            }
            if (exceptionType == typeof(IDeleteException).Name)
            {

            }
            if (exceptionType == typeof(ICompareException).Name)
            {

            }
            if (exceptionType == typeof(IInvalidStateOperationException).Name)
            {

            }
            if (exceptionType == typeof(IArgumentException).Name)
            {

            }
            if (exceptionType == typeof(IException).Name)
            {

            }
            return new Exception(message);

        }
    }
}
