using Cards.Enum;
using Grid;
using StateMachine;
using Unit;
using Unit.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Spells.States
{
    public class HitActionState : BaseState, IPhysicsEventHandler
    {
        private UnitStateMachineBase _state;
        private readonly int _damageAmount;

        public HitActionState(UnitStateMachineBase state) : base(state, "Hit Action State")
        {
            _damageAmount = state.Card.Attack;
            _state = state;
        }

        public override void Enter()
        {
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            GridManager.Instance.SetPositionOccupiedInGrid(screenToWorldPoint.x, screenToWorldPoint.x, true);
            GridManager.Instance.SetTileType(screenToWorldPoint.x, screenToWorldPoint.x, TileType.Field);
            Vector2? gridPos = GridManager.Instance.GetPositionInGrid(screenToWorldPoint.x, screenToWorldPoint.y);
            if (!gridPos.HasValue)
            {
                Debug.Log("Can't place trap here");
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            UnitStateMachineBase unit = other.GetComponent<UnitStateMachineBase>();
            unit.HealthController.Hit(_damageAmount);
        }

        public void OnTriggerExit2D(Collider2D other)
        {

        }
    }
}