using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards.Models
{
    public class CardBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CardData cardData;
        private Card _card;
        private float _originalYPosition;

        private void Awake()
        {
            _card = new Card(cardData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _originalYPosition = transform.position.y;
            transform.DOMoveY(transform.position.y + 0.2f, .25f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOKill();
            transform.DOMoveY(_originalYPosition, .01f);
        }
    }
}