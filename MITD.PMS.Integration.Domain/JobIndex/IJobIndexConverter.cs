using MITD.PMS.Integration.Core;

namespace MITD.PMS.Integration.Domain
{
    public interface IJobIndexConverter:IConverter
    {
        void ConvertJobIndex(Period period);
    }
}
