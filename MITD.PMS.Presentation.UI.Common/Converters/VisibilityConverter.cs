using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace MITD.PMS.Presentation.UI.Common
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, global::System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;
            if ((bool)value)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, global::System.Globalization.CultureInfo culture)
        {
            return (((Visibility)value) == Visibility.Visible);
        }
    }

    public class VisibilityObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, global::System.Globalization.CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, global::System.Globalization.CultureInfo culture)
        {
            if (((Visibility) value) == Visibility.Visible)
                return value;
            return null;
        }
    }


    public class VisibilityListObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, global::System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return (((IList)value).Count == 0 || ( ((IList)value).Count ==1 && ((IList)value)[0] == null ) )? Visibility.Collapsed : Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, global::System.Globalization.CultureInfo culture)
        {
            if (((Visibility)value) == Visibility.Visible)
                return value;
            return null;
        }
    }
}
