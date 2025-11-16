using Deck;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DeckManagerUI : MonoBehaviour
    {
        [SerializeField] private DeckManager deckManager;
        [SerializeField] private TMP_Text numberOfRemainingCardInDeck;

        private void Awake()
        {
            deckManager.NumberOfRemainingCardInDeckChanged += UpdateNumberCard;
        }

        private void UpdateNumberCard(int numberOfCards)
        {
            numberOfRemainingCardInDeck.text = numberOfCards.ToString();
        }
    }
}