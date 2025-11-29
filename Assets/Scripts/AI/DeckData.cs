using System.Collections.Generic;
using Deck;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(fileName = "AiDeck", menuName = "AI/Deck Data", order = 1)]
    public class DeckData : ScriptableObject
    {
        public List<CardInDeck> cards;
    }
}