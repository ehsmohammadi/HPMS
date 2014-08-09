using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MITD.PMS.Exceptions
{
    [Serializable]
    public class PMSOperationException :Exception
    {
        public PMSOperationException(string message)
            : base(message)
        { }
        public PMSOperationException()
            : base("شما قادر به انجام عملیات مورد نظر نیستید")
        {
        }
    }

    [Serializable]
    public class PMSOperationArgumentExeption : PMSOperationException
    {
        public PMSOperationArgumentExeption(string parameterName)
            : base("پارامتر " + parameterName + " صحیح نمی باشد ")
        {
            
        }
        public PMSOperationArgumentExeption(): base("پارامتر های ارسالی صحیح نمی باشد")
        {
        }


    }

    [Serializable]
    public class PMSDeleteTransactionExeption : PMSOperationException
    {
        public PMSDeleteTransactionExeption(string message)
            : base(message)
        {

        }
        public PMSDeleteTransactionExeption()
            : base("امکان حذف به علت ارتباط با سایر داده های سیستم وجود ندارد")
        {
        }


    }



}
