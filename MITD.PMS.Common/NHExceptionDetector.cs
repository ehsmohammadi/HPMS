﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Common
{
    public static class NHExceptionDetector
    {
        public static bool IsDeleteHasRelatedDataException(Exception exp, out string typeName, out string keyname)
        {
            typeName = keyname = "";
            if (exp == null)
                return false;
            var res = false;
            var x = exp;
            do
            {
                if (x.Message.ToLower().Contains("could not delete"))
                {
                    res = true;
                    break;
                }
                x = x.InnerException;

            } while (x != null);
            return res;
        }
        public static bool IsDublicateException(Exception exp, out string typeName, out string keyname)
        {
            typeName = keyname = "";
            if (exp == null)
                return false;
            var res = false;
            var x = exp;
            do
            {
                if (x.Message.ToLower().Contains("duplicate"))
                {
                    res = true;
                    break;
                }
                x = x.InnerException;

            } while (x != null);
            return res;
        }

    }
}
