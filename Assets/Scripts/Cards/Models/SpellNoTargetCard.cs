using Events.Trigger;
using Grid;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cards.Models
{
    public class SpellNoTargetCard : CardBase
    {
        [SerializeField] private EventTrigger onPlaySpell;
        [SerializeField] private EventTrigger onDestroyUnit;
        private Vector3 _originalPosition;
        private bool _isMoving;
        private Transform _originalParent;
        private Camera _camera;

        protected override void Awake()
        {
            base.Awake();
            _originalParent = transform.parent;
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_isMoving)
            {
                Vector3 screenToWorldPoint = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                transform.position = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
            }
        }

        protected override void CardPreExecute()
        {
            transform.SetParent(null, false);
            _isMoving = true;
        }

        public override void IsPlaced()
        {
            base.IsPlaced();
            _originalPosition = transform.position;
        }

        protected override void CardPostExecute()
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (GridManager.Instance.IsCardHoveringOnGrid(mousePos.x, mousePos.y))
            {
                GetComponent<CardUI>().OnPlaySpell();
                onPlaySpell.Raise();
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