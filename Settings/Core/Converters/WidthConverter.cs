using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Settings.Core.Converters
{
    public class WidthConverter : MarkupExtension, IMultiValueConverter
    {
        private static WidthConverter _converter = null;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null) _converter = new WidthConverter();
            return _converter;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is double && values[1] is Thickness)
            {
                var width = (double)values[0];
                var thickness = (Thickness)values[1];
                width -= (thickness.Left + thickness.Right);
                return width;
            }
            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
