using UnityEngine;

namespace Cards.Models
{
    public abstract class CardBase : MonoBehaviour
    {
        [SerializeField] private CardData cardData;
        private Card _card;

        private void Awake()
        {
            _card = new Card(cardData);
        }
    }
}