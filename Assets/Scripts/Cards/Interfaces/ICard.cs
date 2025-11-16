using System;
using Cards.Models;
using UI;

namespace Cards.Interfaces
{
    public interface ICard
    {
        Card Card { get; }
        event Func<bool> OnPlacingRequested;
        void IsPlaced();
        void SetIsPlaced();
        void SetCard(CardData newCardData);
    }
}