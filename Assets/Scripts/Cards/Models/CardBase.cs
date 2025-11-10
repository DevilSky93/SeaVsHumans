using System;
using Cards.Interfaces;
using DG.Tweening;
using Unit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Cards.Models
{
    public abstract class CardBase : MonoBehaviour, ICard, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IPointerDownHandler
    {
        [SerializeField] private CardData cardData;
        private float _originalYPosition;
        private bool _isPlaced;
        public event Func<bool> OnPlacingRequested;

        public Card Card { get; private set; }

        private void Awake()
        {
            Card = new Card(cardData);
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

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (OnPlacingRequested?.Invoke() == false)
            {
                Debug.LogWarning("Not enough essence to place unit");
                return;
            }
            StateMachine.StateMachine unit = CardFactory.Build(cardData).GetComponent<StateMachine.StateMachine>();
            StateMachine.StateMachine unitGameObject =
                Instantiate(unit, Mouse.current.position.ReadValue(), Quaternion.identity);
            // unitGameObject.Initialize();
            unitGameObject.gameObject.SetActive(true);
            AllowPlace();
        }

        protected abstract void AllowPlace();
    }
}