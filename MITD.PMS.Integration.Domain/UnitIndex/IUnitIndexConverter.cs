using MITD.PMS.Integration.Core;

namespace MITD.PMS.Integration.Domain

{
    public interface IUnitIndexConverter:IConverter
    {
        void ConvertUnitIndex(Period period);
    }
}
