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
            _unit = GetComponent<UnitStateMachineBase>();
            Vector3 gridPos = _unit.transform.position;
            bool isUnitTwoByTwo = _unit.Card.AreaTarget != AreaTarget.OneXOne;
            GridManager.Instance.SetPositionOccupiedInGrid(gridPos.x, gridPos.y, false);
            if (isUnitTwoByTwo)
            {
                GridManager.Instance.SetPositionOccupiedInGrid(gridPos.x + 1, gridPos.y, false);
                GridManager.Instance.SetPositionOccupiedInGrid(gridPos.x, gridPos.y - 1, false);
                GridManager.Instance.SetPositionOccupiedInGrid(gridPos.x + 1, gridPos.y - 1, false);
            }

            _unit.ChangeState(_unit.PlacingState);
            isTwoByTwo.Raise(isUnitTwoByTwo);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            bool isUnitTwoByTwo = _unit.Card.AreaTarget != AreaTarget.OneXOne;
            if (isUnitTwoByTwo)
            {
                if (GridManager.Instance.IsPositionInvalidInGrid(mousePos.x, mousePos.y) ||
                    GridManager.Instance.IsPositionInvalidInGrid(mousePos.x + 1, mousePos.y) ||
                    GridManager.Instance.IsPositionInvalidInGrid(mousePos.x, mousePos.y - 1) ||
                    GridManager.Instance.IsPositionInvalidInGrid(mousePos.x + 1, mousePos.y - 1) ||
                    !GridManager.Instance.IsMouseHoveringOnGrid(mousePos.x, mousePos.y) ||
                    !GridManager.Instance.IsMouseHoveringOnGrid(mousePos.x + 1, mousePos.y) ||
                    !GridManager.Instance.IsMouseHoveringOnGrid(mousePos.x, mousePos.y - 1) ||
                    !GridManager.Instance.IsMouseHoveringOnGrid(mousePos.x + 1, mousePos.y - 1))
                {
                    _unit?.ChangeState(_unit.IdleState);
                    return;
                }
            }
            else if (GridManager.Instance.IsPositionInvalidInGrid(mousePos.x, mousePos.y) ||
                     !GridManager.Instance.IsMouseHoveringOnGrid(mousePos.x, mousePos.y))
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