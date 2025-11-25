using System;
using Cards.Enum;
using Events.FloatFloat;
using Events.Trigger;
using Grid;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cards.Models
{
    public class UnitCard : CardBase
    {
        [SerializeField] private GameEventFloatFloatListener onPlaceUnitListener;
        [SerializeField] private EventTrigger onPlaceTrap;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        // Deactivate Placing
        public void OnDestroyUnit()
        {
            onPlaceUnitListener.enabled = false;
        }

        protected override void CardPreExecute()
        {
            onPlaceUnitListener.enabled = true;
        }

        protected override void CardPostExecute()
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (cardData.cardType == CardType.Field && !GridManager.Instance.IsPositionOccupiedInGrid(mousePos.x, mousePos.y))
            {
                onPlaceTrap.Raise();
            }
        }
    }
}