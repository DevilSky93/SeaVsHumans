using System.Collections.Generic;
using System.Linq;
using Cards;
using Cards.Enum;
using Cards.Models;
using Grid;
using StateMachine;
using Unit;
using UnityEngine;

namespace AI.States
{
    public class AIPlacementState : BaseState
    {
        private readonly AiStateMachine _state;
        private readonly List<CardData> _hand;
        private readonly EssenceMarine.EssenceMarine _essenceMarine;
        private readonly List<UnitStateMachineBase> _units;

        public AIPlacementState(AiStateMachine state, List<CardData> hand, EssenceMarine.EssenceMarine essenceMarine,
            List<UnitStateMachineBase> units) : base(state, "AI Placement State")
        {
            _essenceMarine = essenceMarine;
            _units = units;
            _hand = hand;
            _state = state;
        }

        public override void Enter()
        {
            IOrderedEnumerable<CardData> unitCards = _hand.Where(c => c.cardType == CardType.Unit)
                .OrderBy(c => c.essenceMarine);

            foreach (CardData card in unitCards)
            {
                if (!CanAfford(card)) continue;

                Vector2 pos = ChoosePlacementPosition(card);
                PlaceUnit(card, pos);
            }
            
            _state.ChangeState(_state.FightingState);
        }

        private void PlaceUnit(CardData card, Vector2 pos)
        {
            _essenceMarine.OnEssenceSpend(-card.essenceMarine);
            StateMachine.StateMachine cardGo = CardFactory.Build(card);
            StateMachine.StateMachine cardGameObject =
                Object.Instantiate(cardGo, pos, cardGo.transform.rotation);
            cardGameObject.gameObject.SetActive(true);
            _units.Add(cardGameObject.GetComponent<UnitStateMachineBase>());
            GridManager.Instance.SetPositionOccupiedInGrid(pos.x, pos.y, true);
            _hand.Remove(card);
        }

        private static Vector2 ChoosePlacementPosition(CardData card)
        {
            if (card.areaTarget == AreaTarget.OneXOne)
            {
                return GridManager.Instance.GetRandomFreeCell(TileType.Enemy);
            }

            // TODO : Big unit placement logic
            return new Vector2(-1, -1);
        }

        private bool CanAfford(CardData card)
        {
            return _essenceMarine.HaveEnoughEssence(card.essenceMarine);
        }
    }
}