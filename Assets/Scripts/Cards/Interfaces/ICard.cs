using System;
using Cards.Models;

namespace Cards.Interfaces
{
    public interface ICard
    {
        Card Card { get; }
        event Func<bool> OnPlacingRequested;
        void IsPlaced();
        void SetIsPlaced();
    }
}