using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Model.Enums;
using Brushes = System.Windows.Media.Brushes;
using Color = Model.Enums.Color;

namespace ImageGenerator.CardImage.Converters
{
    internal class CardFillToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Filling filling;
            Color color;
            try
            {
                filling = (Filling)values[0];
                color = (Color)values[1];
            }
            catch (Exception)
            {
                return Brushes.Transparent;
            }

            switch (filling)
            {
                case Filling.Filled:
                    return GetSolidBrushOnCardColor(color);

                case Filling.Linear:
                    return GetDrawingBrushOnCardColor(color);

                default:
                    return Brushes.Transparent;
            }

        }

        private object GetDrawingBrushOnCardColor(Color color)
        {
            var geometry0 = new LineGeometry(new Point(0, 0), new Point(0, 6));
            var geometry1 = new LineGeometry(new Point(6, 0), new Point(6, 6));
            var geometryGroup = new GeometryGroup();
            geometryGroup.Children.Add(geometry0);
            geometryGroup.Children.Add(geometry1);

            var stroke = new Pen {Thickness = 1.0, Brush = GetSolidBrushOnCardColor(color)};

            var drawing = new GeometryDrawing {Pen = stroke, Geometry = geometryGroup};

            var brush = new DrawingBrush();
            brush.TileMode= TileMode.Tile;
            brush.Viewport = new Rect(0,0, 6, 6);
            brush.ViewportUnits = BrushMappingMode.Absolute;

            brush.Drawing = drawing;

            return brush;
        }

        private Brush GetSolidBrushOnCardColor(Color color)
        {
            switch (color)
            {
                case Color.Blue:
                    return UsableColors.BrusheBlue;

                case Color.Red:
                    return UsableColors.BrusheRed;

                case Color.Green:
                    return UsableColors.BrusheGreen;

                default:
                    return Brushes.Transparent;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
