using System.Collections.Generic;
using Model;
using Model.Enums;

namespace ImageGenerator.CardImage
{
    public class CardImageViewModel
    {
        private readonly Card _card;

        public Color Color{get { return _card.Color; }}
        public Filling Filling { get { return _card.Filling; } }
        public SymbolType SymbolType { get { return _card.SymbolType; } }

        public List<object> ElementList { get; set; }

        public Card Card { get { return _card; } }
        public CardImageViewModel(Card card)
        {
            _card = card;

            InitElementList();
        }

        private void InitElementList()
        {
            ElementList = new List<object>();
            switch (_card.NumberOfSymbols)
            {
                case NumberOfSymbols.One:
                    ElementList.Add(SymbolType);
                    break;
                case NumberOfSymbols.Two:
                    ElementList.Add(SymbolType);
                    ElementList.Add(SymbolType);
                    break;
                case NumberOfSymbols.Three:
                    ElementList.Add(SymbolType); ElementList.Add(SymbolType); ElementList.Add(SymbolType);
                    break;
            }
        }
    }
}
