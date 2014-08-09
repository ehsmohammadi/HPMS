using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Application
{
    public static class ApplicationExceptionHelper
    {
        public static Exception ResolveException(Exception exception)
        {
            if (exception.InnerException != null)
                return exception.InnerException;
            return exception;
        }
    }
}
