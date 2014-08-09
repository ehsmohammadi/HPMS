using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MITD.Core;
using MITD.PMS.Common;

namespace MITD.PMSAdmin.Exceptions
{
    [Serializable]
    public class PMSAdminException :Exception,IException
    {
        public PMSAdminException()
            : base("Operation Failed")
        {
            Code = (int)ApiExceptionCode.Unknown;
        }

        public PMSAdminException(string message)
            : base(message)
        {
            Code = (int)ApiExceptionCode.Unknown;
        }
        public PMSAdminException(int code, string message)
            : base(message)
        {
            Code = code;
        }


        public PMSAdminException(string message, Exception ex)
            : base(message,ex)
        {
            Code = (int)ApiExceptionCode.Unknown;
        }

        public PMSAdminException(int code, string message, Exception ex)
            : base(message,ex)
        {
            Code = code;
        }

        public int Code { get; private set; }
    }

    //[Serializable]
    //public class PMSAdminDuplicateExeption : PMSAdminException, IDuplicateException
    //{
    //    public PMSAdminDuplicateExeption(string typeName, string keyName)
    //        : base(100, "Duplicate " + keyName + " in " + typeName)
    //    {
    //        this.DomainObjectName = typeName;
    //        this.PropertyName = keyName;
    //    }
    //    public string DomainObjectName { get; private set; }
    //    public string PropertyName { get; private set; }
    //}

    //[Serializable]
    //public class PMSAdminOperationArgumentExeption : PMSAdminException
    //{
    //    public PMSAdminOperationArgumentExeption(string parameterName)
    //        : base("پارامتر " + parameterName + " صحیح نمی باشد ")
    //    {

    //    }
    //    public PMSAdminOperationArgumentExeption()
    //        : base("پارامتر های ارسالی صحیح نمی باشد")
    //    {
    //    }


    //}

    //[Serializable]
    //public class PMSAdminDeleteTransactionExeption : PMSAdminException
    //{
    //    public PMSAdminDeleteTransactionExeption(string message)
    //        : base(message)
    //    {

    //    }
    //    public PMSAdminDeleteTransactionExeption()
    //        : base("امکان حذف به علت ارتباط با سایر داده های سیستم وجود ندارد")
    //    {
    //    }


    //}


}
