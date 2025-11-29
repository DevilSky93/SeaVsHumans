using System.Collections.Generic;
using System.Linq;
using Cards.Enum;
using Cards.Models;
using StateMachine;
using Unit;

namespace AI.States
{
    public class AISupportState : BaseState
    {
        private readonly AiStateMachine _state;
        private readonly List<CardData> _hand;
        private readonly EssenceMarine.EssenceMarine _essenceMarine;
        private readonly List<UnitStateMachineBase> _units;

        public AISupportState(AiStateMachine state, List<CardData> hand, EssenceMarine.EssenceMarine essenceMarine,
            List<UnitStateMachineBase> units) : base(state, "AI Support State")
        {
            _units = units;
            _essenceMarine = essenceMarine;
            _hand = hand;
            _state = state;
        }

        public override void Enter()
        {
            CardData healCard = _hand.FirstOrDefault(c => c.cardType == CardType.SpellWithTarget);
            if (healCard == null) return;

            UnitStateMachineBase hurtUnit = HurtUnit();
            if (hurtUnit != null && CanAfford(healCard))
            {
                PlaySpell(healCard, hurtUnit);
            }
            _state.ChangeState(_state.PlacementState);
        }

        public override void Exit()
        {
            _essenceMarine.OnRoundEnd();
        }

        private UnitStateMachineBase HurtUnit()
        {
            UnitStateMachineBase target = _units
                .Where(u => u.HealthController.CurrentHealth < u.HealthController.MaxHealth * 0.5f)
                .OrderBy(u => u.HealthController.CurrentHealth)
                .FirstOrDefault();

            return target;
        }

        private void PlaySpell(CardData healCard, UnitStateMachineBase target)
        {
            _essenceMarine.OnEssenceSpend(-healCard.essenceMarine);
            target.HealthController.Heal(healCard.attack);
            _hand.Remove(healCard);
        }

        private bool CanAfford(CardData card)
        {
            return _essenceMarine.HaveEnoughEssence(card.essenceMarine);
        }
    }
}