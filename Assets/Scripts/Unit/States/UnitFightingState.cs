using Cards.Models;
using StateMachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Unit.States
{
    public class UnitFightingState : BaseState
    {
        private readonly UnitHealthController _unitHealthController;
        private readonly float _attackSpeed;
        private readonly int _attack;
        private float _attackSpeedTimer;

        protected UnitStateMachineBase overlapEnemy;
        protected readonly UnitStateMachineBase state;
        protected readonly LayerMask enemyLayerMask;

        public UnitFightingState(UnitStateMachineBase state, UnitHealthController unitHealthController, LayerMask enemyLayerMask) : base(state, "Card Fighting State")
        {
            this.state = state;
            _unitHealthController = unitHealthController;
            Card unit = state.Card;
            _attackSpeed = unit.AttackSpeed;
            _attackSpeedTimer = unit.AttackSpeed;
            _attack = unit.Attack;
            this.enemyLayerMask = enemyLayerMask;
        }

        public override void Enter()
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(state.transform.position, state.transform.right, 5, enemyLayerMask);
            
            overlapEnemy = raycastHit2D ? raycastHit2D.collider.GetComponent<UnitStateMachineBase>() : null;
        }

        public override void UpdateLogics()
        {
            if (_unitHealthController.IsDead)
            {
                BoxCollider2D rb = state.gameObject.GetComponent<BoxCollider2D>();
                rb.enabled = false;
                StateMachine.ChangeState(state.DyingState);
                return;
            }

            if (!overlapEnemy) return;
            _attackSpeedTimer -= Time.deltaTime;
            if (_attackSpeedTimer <= 0)
            {
                overlapEnemy.HealthController.Hit(_attack);
                _attackSpeedTimer = _attackSpeed;
            }
            if (overlapEnemy.HealthController.IsDead)
            {
                Debug.Log(overlapEnemy.name + " is dead.");
                StateMachine.ChangeState(state.MovementState);
            }
        }
    }
}