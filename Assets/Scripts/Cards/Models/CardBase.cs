using DG.Tweening;
using Events.Bool;
using Unit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Cards.Models
{
    public class CardBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
    {
        [SerializeField] private CardData cardData;
        [SerializeField] private EventBool canPlaceUnitEvent;
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

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("TODO : see details");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            UnitStateMachine unit = CardUnitFactory.Instance.Build(cardData);
            UnitStateMachine unitGameObject = Instantiate(unit, Mouse.current.position.ReadValue(), Quaternion.identity);
            unitGameObject.Initialize();
            unitGameObject.gameObject.SetActive(true);
            canPlaceUnitEvent.Raise(true);
        }
    }
}