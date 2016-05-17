using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace ImageGenerator.CardImage.Converters
{
    internal class CardColorToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return new SolidBrush(Color.Transparent);

            var enumValue = (Model.Enums.Color)value;

            switch (enumValue)
            {
                case Model.Enums.Color.Blue:
                    return UsableColors.BrusheBlue;

                case Model.Enums.Color.Red:
                    return UsableColors.BrusheRed;

                case Model.Enums.Color.Green:
                    return UsableColors.BrusheGreen;

                default:
                    return new SolidBrush(Color.Transparent);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
