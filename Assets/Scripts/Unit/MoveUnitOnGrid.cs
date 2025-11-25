using Cards.Enum;
using Events.Bool;
using Grid;
using Unit.States;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Unit
{
    public class MoveUnitOnGrid : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private EventBool isTwoByTwo;

        private UnitStateMachineBase _unit;

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Move unit");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2? gridPos = GridManager.Instance.GetPositionInGrid(mousePos.x, mousePos.y);
            if (!gridPos.HasValue)
            {
                Debug.Log("Can't move unit here");
                return;
            }
            GridManager.Instance.SetPositionOccupiedInGrid(gridPos.Value.x, gridPos.Value.y, false);
            _unit = GetComponent<UnitStateMachineBase>();
            _unit.ChangeState(_unit.PlacingState);
            isTwoByTwo.Raise(_unit.Card.AreaTarget != AreaTarget.OneXOne);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (GridManager.Instance.IsPositionInvalidInGrid(mousePos.x, mousePos.y))
            {
                _unit?.ChangeState(_unit.IdleState);
                return;
            }
            ((UnitIdleState)_unit.IdleState).ResetPosition();
            _unit?.OnPlaceUnit(mousePos.x, mousePos.y);
            _unit?.ChangeState(_unit.IdleState);
        }
    }
}