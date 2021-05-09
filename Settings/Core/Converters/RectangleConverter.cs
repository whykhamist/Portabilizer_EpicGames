using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Settings.Core.Converters
{
    public class RectangleConverter : MarkupExtension, IMultiValueConverter
    {
        private static RectangleConverter _converter = null;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null) _converter = new RectangleConverter();
            return _converter;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 4 && values[0] is double width && values[1] is double height && values[2] is CornerRadius radius && values[3] is Thickness borderThickness)
            {

                if (width < Double.Epsilon || height < Double.Epsilon)
                {
                    return Geometry.Empty;
                }

                // Actually we need more complex geometry, when CornerRadius has different values.
                // But let me not to take this into account, and simplify example for a common value.
                var clip = new RectangleGeometry(
                    new Rect(
                        0,
                        0,
                        width - borderThickness.Left - borderThickness.Right,
                        height - borderThickness.Top - borderThickness.Bottom
                    ), radius.TopLeft - borderThickness.Left/2, radius.TopLeft - borderThickness.Left/2);
                clip.Freeze();

                return clip;
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
