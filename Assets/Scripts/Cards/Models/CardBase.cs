using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards.Models
{
    public class CardBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CardData cardData;
        private float _originalYPosition;
        private bool _isPlaced;

        public void IsPlaced()
        {
            _isPlaced = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isPlaced) return;
            _originalYPosition = transform.position.y;
            transform.DOMoveY(transform.position.y + 0.2f, .25f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isPlaced) return;
            transform.DOKill();
            transform.DOMoveY(_originalYPosition, .01f);
        }
    }
}