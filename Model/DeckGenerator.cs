using System;
using System.Collections.Generic;
using System.Linq;
using Model.Enums;

namespace Model
{
    public class DeckGenerator
    {
        public List<Card> GetDeck(bool isSimpleDeck = false)
        {
            var deck = GetFilledDeck(isSimpleDeck);

            var shuffledDeck = ShuffleDeck(deck);

            return shuffledDeck;
        }

        private List<Card> GetFilledDeck(bool isSimpleDeck)
        {
            var deck = new List<Card>();

            foreach (Color color in Enum.GetValues(typeof(Color)))
            {
                if (isSimpleDeck && color != Color.Red) continue;

                foreach (SymbolType symbolType in Enum.GetValues(typeof(SymbolType)))
                {
                    foreach (Filling filling in Enum.GetValues(typeof(Filling)))
                    {
                        foreach (NumberOfSymbols numberOfSymbols in Enum.GetValues(typeof(NumberOfSymbols)))
                        {
                            deck.Add(new Card(numberOfSymbols, color, filling, symbolType));
                        }
                    }
                }
            }
            return deck;
        }

        private List<Card> ShuffleDeck(List<Card> deck)
        {
            var randomizer = new Random();
            var shuffledDeck = new List<Card>(deck.Count);


            while (deck.Any())
            {
                var randomNumber = randomizer.Next(0, deck.Count - 1);
                var card = deck[randomNumber];
                deck.Remove(card);
                shuffledDeck.Add(card);
            }

            return shuffledDeck;
        }
    }
}
