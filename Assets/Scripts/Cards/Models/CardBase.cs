using System;
using DG.Tweening;
using Events.Bool;
using Events.FloatFloat;
using Unit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Cards.Models
{
    public class CardBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IPointerDownHandler
    {
        [SerializeField] private CardData cardData;
        [SerializeField] private EventBool canPlaceUnitEvent;
        [SerializeField] private GameEventFloatFloatListener onPlaceUnitListener;
        private float _originalYPosition;
        private bool _isPlaced;
        public event Func<bool> OnPlacingRequested;

        public Unit.Unit Unit { get; private set; }

        private void Awake()
        {
            Unit = new Unit.Unit(cardData);
        }

        public void IsPlaced()
        {
            _isPlaced = true;
            _originalYPosition = transform.position.y;
        }

        public void SetIsPlaced()
        {
            _isPlaced = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isPlaced) return;
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
            if (OnPlacingRequested?.Invoke() == false)
            {
                Debug.LogWarning("Not enough essence to place unit");
                return;
            }
            UnitStateMachine unit = CardUnitFactory.Instance.Build(cardData);
            UnitStateMachine unitGameObject =
                Instantiate(unit, Mouse.current.position.ReadValue(), Quaternion.identity);
            unitGameObject.Initialize();
            unitGameObject.gameObject.SetActive(true);
            canPlaceUnitEvent.Raise(true);
            onPlaceUnitListener.enabled = true;
        }
        
        public void DeactivatePlacing()
        {
            onPlaceUnitListener.enabled = false;
        }
    }
}