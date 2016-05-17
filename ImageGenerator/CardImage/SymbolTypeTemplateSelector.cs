using System.Windows;
using System.Windows.Controls;
using Model.Enums;

namespace ImageGenerator.CardImage
{

    internal class SymbolTypeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EllipseTemplate { get; set; }
        public DataTemplate DiamondTemplate { get; set; }
        public DataTemplate WaveTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(item is SymbolType))
                return new DataTemplate();

            var symbolType = (SymbolType)item;

            switch (symbolType)
            {
                case SymbolType.Diamond:
                    return DiamondTemplate;
                case SymbolType.Ellipse:
                    return EllipseTemplate;
                case SymbolType.Wave:
                    return WaveTemplate;
                default:
                    return new DataTemplate();
            }
        }
    }
}
