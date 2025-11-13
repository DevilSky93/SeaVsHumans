using Cards.Models;
using StateMachine;
using UnityEngine;

namespace Unit.States
{
    public class UnitFightingState : BaseState
    {
        private readonly UnitStateMachineBase _state;
        private readonly UnitHealthController _unitHealthController;
        private UnitStateMachineBase _overlapEnemy;
        private readonly float _attackSpeed;
        private readonly int _attack;

        private float _attackSpeedTimer;
        private readonly LayerMask _enemyLayerMask;

        public UnitFightingState(UnitStateMachineBase state, UnitHealthController unitHealthController, LayerMask enemyLayerMask) : base(state, "Card Fighting State")
        {
            _state = state;
            _unitHealthController = unitHealthController;
            Card unit = state.Card;
            _attackSpeed = unit.AttackSpeed;
            _attackSpeedTimer = unit.AttackSpeed;
            _attack = unit.Attack;
            _enemyLayerMask = enemyLayerMask;
        }

        public override void Enter()
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(_state.transform.position, _state.transform.right, 5, _enemyLayerMask);
            _overlapEnemy = raycastHit2D ? raycastHit2D.collider.GetComponent<UnitStateMachineBase>() : null;
        }

        public override void UpdateLogics()
        {
            if (_unitHealthController.IsDead)
            {
                BoxCollider2D rb = _state.gameObject.GetComponent<BoxCollider2D>();
                rb.enabled = false;
                StateMachine.ChangeState(_state.DyingState);
                return;
            }

            if (!_overlapEnemy) return;
            _attackSpeedTimer -= Time.deltaTime;
            if (_attackSpeedTimer <= 0)
            {
                _overlapEnemy.HealthController.Hit(_attack);
                _attackSpeedTimer = _attackSpeed;
            }
            if (_overlapEnemy.HealthController.IsDead)
            {
                Debug.Log(_overlapEnemy.name + " is dead.");
                StateMachine.ChangeState(_state.MovementState);
            }
        }
    }
}