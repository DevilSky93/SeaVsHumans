using System;
using Cards.Enum;
using Grid;
using StateMachine;
using Unit;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Spells.States
{
    public class HealActionState : BaseState
    {
        private SpellCardStateMachineBase _state;
        private readonly int _healAmount;

        public HealActionState(SpellCardStateMachineBase state, int healAmount) : base(state, "Heal Action State")
        {
            _healAmount = healAmount;
            _state = state;
        }

        public override void Enter()
        {
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            UnitStateMachineBase unit = GridManager.Instance.GetObjectInGrid<UnitStateMachineBase>(screenToWorldPoint.x ,screenToWorldPoint.y);
            if (unit == null)
            {
                throw new ArgumentException("No unit found at the targeted position.");
            }

            Debug.Log("Trying to heal");
            if (unit.Card.CardType == CardType.Unit)
            {
                Debug.Log("Healing unit");
                unit.HealthController.Heal(_healAmount);
            }
        }
    }
}