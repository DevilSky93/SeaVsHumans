using System;
using System.Collections.Generic;
using System.Linq;
using Cards.Models;
using Helpers;
using JetBrains.Annotations;
using UnityEngine;

namespace Deck
{
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private List<CardInDeck> cards;
        [SerializeField] private int maxCardsInDeck;
        private List<CardInDeck> _deck;
        private List<CardData> _currentStateDeck;
        
        public event Action<int> NumberOfRemainingCardInDeckChanged;

        private void Awake()
        {
            if (cards.Sum(c => c.quantity) > maxCardsInDeck)
            {
                Debug.LogError("Too much cards in deck");
            }
            _deck = cards.ToList();
            _currentStateDeck = _deck.SelectMany(d =>
            {
                List<CardData> cardList = new();
                for (int i = 0; i < d.quantity; i++)
                {
                    cardList.Add(d.card);
                }
                return cardList;
            }).ToList();
            ShuffleCard();
        }

        [CanBeNull]
        public CardData DrawCard()
        {
            if (_currentStateDeck.Count <= 0)
            {
                Debug.LogWarning("No more cards in deck");
                return null;
            }
            CardData first = _currentStateDeck[0];
            _currentStateDeck.RemoveAt(0);
            NumberOfRemainingCardInDeckChanged?.Invoke(_currentStateDeck.Count);
            return first;
        }

        private void ShuffleCard()
        {
            _currentStateDeck.Shuffle();
        }
    }
}