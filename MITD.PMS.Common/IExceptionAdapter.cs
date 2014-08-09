using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.Core
{
    public interface IExceptionConverter
    {
        Dictionary<string, object> TryConvert(IException exp);
        IException TryConvertBack(Dictionary<string, object> expData);
    }

    public abstract class IExceptionConverter<T> : IExceptionConverter where T:IException
    {
        public Dictionary<string, object> TryConvert(IException exp)
        {
            return Convert((T)exp);
        }

        public IException TryConvertBack(Dictionary<string, object> expData)
        {
            return ConvertBack(expData);
        }

        public abstract Dictionary<string, object> Convert(T exception);
        public abstract T ConvertBack(Dictionary<string, object> expData);
    }
}
