using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MITD.Presentation.UI
{
    public class DataGridColumnHelper
    {
        public static readonly DependencyProperty HeaderBindingProperty = DependencyProperty.RegisterAttached(
        "HeaderBinding",
        typeof(object),
        typeof(DataGridColumnHelper),
        new PropertyMetadata(null, DataGridColumnHelper.HeaderBinding_PropertyChanged));

        public static object GetHeaderBinding(DependencyObject source)
        {
            return (object)source.GetValue(DataGridColumnHelper.HeaderBindingProperty);
        }

        public static void SetHeaderBinding(DependencyObject target, object value)
        {
            target.SetValue(DataGridColumnHelper.HeaderBindingProperty, value);
        }

        private static void HeaderBinding_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataGridColumn column = d as DataGridColumn;

            if (column == null) { return; }

            column.Header = e.NewValue;
        }
    }
}
