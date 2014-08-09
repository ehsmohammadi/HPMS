using System;
using System.Globalization;
using System.Windows.Data;
using MITD.Core;

namespace MITD.PMS.Presentation.UI.Common
{
    public class PersianDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return value != null ? PDateHelper.GregorianToHijri((DateTime)value, false).ToPersianNumbers() : null;
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return PDateHelper.HijriToGregorian(((string)value));
            return null;
        }
    }
}
