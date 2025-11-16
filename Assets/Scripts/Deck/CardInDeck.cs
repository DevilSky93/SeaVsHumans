using System;
using Cards.Models;

namespace Deck
{
    [Serializable]
    public class CardInDeck
    {
        public CardData card;
        public int quantity;
    }
}