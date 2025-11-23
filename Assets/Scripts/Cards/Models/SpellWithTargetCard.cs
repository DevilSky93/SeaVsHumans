using Events.Float;
using Events.Trigger;
using Grid;
using UI;
using Unit;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cards.Models
{
    public class SpellWithTargetCard : CardBase
    {
        [SerializeField] private EventTrigger onDestroyUnit;
        [SerializeField] private EventFloat onTargetUnit;

        private Transform _originalParent;
        private Vector3 _originalPosition;
        private Camera _camera;
        private bool _isMoving;

        private void Awake()
        {
            _originalParent = transform.parent;
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!_isMoving) return;

            Vector3 screenToWorldPoint = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
        }

        public override void IsPlaced()
        {
            base.IsPlaced();
            _originalPosition = transform.position;
        }
        
        protected override void CardPreExecute()
        {
            transform.SetParent(null, false);
            _isMoving = true;
        }
        

        protected override void CardPostExecute()
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            UnitStateMachineBase unitStateMachineBase = GridManager.Instance.GetObjectInGrid<UnitStateMachineBase>(mousePos.x, mousePos.y);
            if (unitStateMachineBase != null)
            {
                GetComponent<CardUI>().OnPlaySpell();
                onTargetUnit.Raise(cardData.attack);
            }
            else
            {
                transform.SetParent(_originalParent, false);
                transform.localPosition = _originalPosition;
                _isMoving = false;
                onDestroyUnit.Raise();
            }
        }
    }
}