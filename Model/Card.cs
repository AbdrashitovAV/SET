using System;
using Model.Enums;

namespace Model
{
    public class Card
    {
        public NumberOfSymbols NumberOfSymbols { get; set; }
        public Color Color { get; set; }
        public Filling Filling { get; set; }
        public SymbolType SymbolType { get; set; }

        public bool IsSelected { get; set; }

        public Card(Card otherCard)
        {
            NumberOfSymbols = otherCard.NumberOfSymbols;
            Color = otherCard.Color;
            Filling = otherCard.Filling;
            SymbolType = otherCard.SymbolType;
        }

        public Card()
        {
        }

        public Card(NumberOfSymbols numberOfSymbols, Color color, Filling filling, SymbolType symbolType)
        {
            NumberOfSymbols = numberOfSymbols;
            Color = color;
            Filling = filling;
            SymbolType = symbolType;
        }

        public Card FindComplementaryCard(Card otherCard)
        {
            var complementaryCard = new Card
            (
                (NumberOfSymbols)FindComplementaryElement(NumberOfSymbols, otherCard.NumberOfSymbols),
                (Color)FindComplementaryElement(Color, otherCard.Color),
                (Filling)FindComplementaryElement(Filling, otherCard.Filling),
                (SymbolType)FindComplementaryElement(SymbolType, otherCard.SymbolType)
            );
            return complementaryCard;
        }

        private int FindComplementaryElement(object cardElement1, object cardElement2)
        {
            var elementInt1 = (int)cardElement1;
            var elementInt2 = (int)cardElement2;

            return elementInt1 == elementInt2 ? elementInt1 : 3 - elementInt1 - elementInt2;
        }

        public override bool Equals(object obj)
        {
            var otherCard = obj as Card;
            if (otherCard == null) return false;

            if (NumberOfSymbols != otherCard.NumberOfSymbols) return false;
            if (Color != otherCard.Color) return false;
            if (Filling != otherCard.Filling) return false;
            if (SymbolType != otherCard.SymbolType) return false;

            return true;
        }

        public override string ToString()
        {
            return NumberOfSymbols + Color.ToString() + Filling + SymbolType;
        }

        public string ToShortString()
        {
            var shortString = String.Empty;

            switch (NumberOfSymbols)
            {
                case NumberOfSymbols.One:
                    shortString += "1";
                    break;
                case NumberOfSymbols.Two:
                    shortString += "2";
                    break;
                case NumberOfSymbols.Three:
                    shortString += "3";
                    break;
            }

            shortString += Color.ToString().Substring(0, 1) + Filling.ToString().Substring(0, 1) + SymbolType.ToString().Substring(0, 1);

            return shortString;
        }
    }
}
