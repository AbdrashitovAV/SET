using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImageGenerator.CardImage;
using Model;
using Model.Enums;
using Color = Model.Enums.Color;

namespace ImageGenerator
{
    public class ImageGenerator
    {
        public Dictionary<string, BitmapFrame> CardDictionary { get; private set; }

        private Dictionary<int, BitmapFrame> RuleBitmapFrames { get; set; }

        public ImageGenerator()
        {
            FillDictionaryWithBitmapFromView();
            FillRulesDicitonary();
        }

        private void FillRulesDicitonary()
        {
            RuleBitmapFrames = new Dictionary<int, BitmapFrame>();
            for (var i = 1; i < 9; i++)
            {
                var bitmapFrameFromResource = GetBitmapFrameFromResource("ImageGenerator", "Resources/Set " + i.ToString("00") + ".png");
                RuleBitmapFrames.Add(i-1, bitmapFrameFromResource);
            }
        }

        private void FillDictionaryWithBitmapFromView()
        {
            CardDictionary = new Dictionary<string, BitmapFrame>();

            foreach (Color color in Enum.GetValues(typeof(Color)))
            {
                foreach (SymbolType symbolType in Enum.GetValues(typeof(SymbolType)))
                {
                    foreach (Filling filling in Enum.GetValues(typeof(Filling)))
                    {
                        foreach (NumberOfSymbols numberOfSymbols in Enum.GetValues(typeof(NumberOfSymbols)))
                        {
                            var card = new Card(numberOfSymbols, color, filling, symbolType);
                            var bitmapFrame = GetBitmapFrameForCard(card);
                            CardDictionary.Add(card.ToShortString(), bitmapFrame);

                            var encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(bitmapFrame);
                        }
                    }
                }
            }
        }

        private static BitmapFrame GetBitmapFrameForCard(Card card)
        {
            var control = new CardImageView(card);
            control.Arrange(new Rect(0, 0, 100, 180));
            control.UpdateLayout();

            var rtb = new RenderTargetBitmap((int)control.ActualWidth, (int)control.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(control);
            return BitmapFrame.Create(rtb);
        }

        private BitmapImage GetImageSourceFromResource(string psAssemblyName, string psResourceName)
        {
            var uri = new Uri("pack://application:,,,/" + psAssemblyName + ";component/" + psResourceName, UriKind.RelativeOrAbsolute);
            return new BitmapImage(uri);
        }

        internal BitmapFrame GetBitmapFrameFromResource(string psAssemblyName, string psResourceName)
        {
            var uri = new Uri("pack://application:,,,/" + psAssemblyName + ";component/" + psResourceName,
                UriKind.RelativeOrAbsolute);
            var pngBitmapDecoder = new PngBitmapDecoder(uri, BitmapCreateOptions.None, BitmapCacheOption.Default);
            return pngBitmapDecoder.Frames[0];

        }

        public BitmapFrame GetImageForCard(Card card)
        {
            return CardDictionary[card.ToShortString()];
        }

        public Dictionary<int, BitmapFrame> GetRuleImages()
        {
            return RuleBitmapFrames;
        }
    }
}
