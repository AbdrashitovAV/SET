using System.Windows.Controls;
using Model;
using Model.Enums;

namespace ImageGenerator.CardImage
{
    /// <summary>
    /// Interaction logic for CardImageView.xaml
    /// </summary>
    public partial class CardImageView : UserControl
    {
        private CardImageViewModel _cardImageViewModel;

        public CardImageView(Card card)
        {
            _cardImageViewModel = new CardImageViewModel(card);
            DataContext = _cardImageViewModel;

            InitializeComponent();
        }

        public CardImageView()
        {
            _cardImageViewModel = new CardImageViewModel(new Card(NumberOfSymbols.Three, Color.Green, Filling.Linear, SymbolType.Wave));
            DataContext = _cardImageViewModel;

            InitializeComponent();
        }
    }
}
