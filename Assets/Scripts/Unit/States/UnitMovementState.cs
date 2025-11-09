using Grid;
using StateMachine;
using Unit.Interfaces;
using UnityEngine;

namespace Unit.States
{
    public class UnitMovementState : BaseState, IPhysicsEventHandler
    {
        private readonly UnitStateMachine _state;
        private readonly Transform _unitTransform;
        private readonly float _unitSpeed;
        private readonly LayerMask _enemyLayerMask;
        private readonly LayerMask _tileMask;

        public UnitMovementState(UnitStateMachine state, Transform unitTransform, float unitSpeed,
            LayerMask enemyLayerMask, LayerMask tileMask) : base(state,
            "Unit Movement State")
        {
            _state = state;
            _unitTransform = unitTransform;
            _unitSpeed = unitSpeed;
            _enemyLayerMask = enemyLayerMask;
            _tileMask = tileMask;
        }

        public override void UpdateLogics()
        {
            _unitTransform.transform.Translate(Vector3.right * (_unitSpeed * Time.deltaTime));
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if ((_enemyLayerMask.value & (1 << other.gameObject.layer)) > .1f)
            {
                _state.ChangeState(_state.FightingState);
            }

            if ((_tileMask.value & (1 << other.gameObject.layer)) > .1f)
            {
                GridManager.Instance.SetPositionOccupiedInGrid(other.transform.position.x, other.transform.position.y, true);
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            GridManager.Instance.SetPositionOccupiedInGrid(other.transform.position.x, other.transform.position.y, false);
        }
    }
}