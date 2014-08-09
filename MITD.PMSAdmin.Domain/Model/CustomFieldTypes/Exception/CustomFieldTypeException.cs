using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.CustomFieldTypes
{
    public class CustomFieldTypeException : PMSAdminException
    {
        public CustomFieldTypeException(string message):base(message)
        {
        }

        public CustomFieldTypeException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public CustomFieldTypeException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }
    }
}
