using System;
using MITD.Core;
using MITD.PMS.Common;

namespace MITD.PMSSecurity.Exceptions
{
    [Serializable]
    public class PMSSecurityException : Exception, IException
    {
        public PMSSecurityException()
            : base(ApiExceptionCode.UnknownSecurityException.DisplayName)
        {
            Code = (int)ApiExceptionCode.UnknownSecurityException;
        }

        public PMSSecurityException(string message)
            : base(message)
        {
            Code = (int)ApiExceptionCode.UnknownSecurityException;
        }
        public PMSSecurityException(int code, string message)
            : base(message)
        {
            Code = code;
        }


        public PMSSecurityException(string message, Exception ex)
            : base(message, ex)
        {
            Code = (int)ApiExceptionCode.UnknownSecurityException;
        }

        public PMSSecurityException(int code, string message, Exception ex)
            : base(message, ex)
        {
            Code = code;
        }

        public int Code { get; private set; }
    }


    //[Serializable]
    //public class PMSSecurityException : Exception
    //{
    //    public PMSSecurityException()
    //        : base("اشکال امنیتی در اجرای درخواست مورد نظر")
    //    {

    //    }

    //    public PMSSecurityException(string message)
    //        : base(message)
    //    {

    //    }
    //}
    
    [Serializable]
    public class PMSSecurityAccessException : PMSSecurityException
    {
        public PMSSecurityAccessException():
            base((int)ApiExceptionCode.UnauthorizedAccessToOperation, ApiExceptionCode.UnauthorizedAccessToOperation.DisplayName)
        {
                
        }

        public PMSSecurityAccessException(string message)
            : base(message)
        {

        }
    }

    [Serializable]
    public class PMSSecurityIdentityException : PMSSecurityException
    {
        public PMSSecurityIdentityException() : 
            base((int)ApiExceptionCode.InvalidUsernameOrPassword, ApiExceptionCode.InvalidUsernameOrPassword.DisplayName)
            
        {

        }

        public PMSSecurityIdentityException(string message)
            : base(message)
        {

        }
    }
}
