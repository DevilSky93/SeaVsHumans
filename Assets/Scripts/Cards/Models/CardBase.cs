using System;
using Cards.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Cards.Models
{
    public abstract class CardBase : MonoBehaviour, ICard, IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private CardData cardData;
        private float _originalYPosition;
        private bool _isPlaced;
        public event Func<bool> OnPlacingRequested;

        public Card Card { get; private set; }

        protected virtual void Awake()
        {
            Card = new Card(cardData);
        }

        public virtual void IsPlaced()
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

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (OnPlacingRequested?.Invoke() == false)
            {
                Debug.LogWarning("Not enough essence to place card");
                return;
            }
            Debug.Log("Placing card");
            StateMachine.StateMachine card = CardFactory.Build(cardData);
            StateMachine.StateMachine cardGameObject =
                Instantiate(card, Mouse.current.position.ReadValue(), Quaternion.identity);
            cardGameObject.gameObject.SetActive(true);
            CardPreExecute();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CardPostExecute();
        }

        protected abstract void CardPreExecute();
        protected abstract void CardPostExecute();
    }
}