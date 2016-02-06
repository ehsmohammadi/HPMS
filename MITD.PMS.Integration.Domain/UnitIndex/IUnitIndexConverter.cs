using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Core;

namespace MITD.PMS.Integration.Domain

{
    public interface IUnitIndexConverter:IConverter
    {
        void ConvertUnitIndex(Period period);
    }
}
