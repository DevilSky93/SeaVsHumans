using System.Collections.Generic;
using System.Linq;
using AI.States;
using Cards.Models;
using Helpers;
using StateMachine;
using Unit;
using UnityEngine;

namespace AI
{
    public class AiStateMachine : StateMachine.StateMachine
    {
        [SerializeField] private DeckData deck;
        [SerializeField] private EssenceMarine.EssenceMarine essenceMarine;
        private readonly List<CardData> _hand = new();
        public BaseState PlacementState { get; private set; }
        public BaseState FightingState { get; private set; }
        public BaseState AISupportState { get; private set; }

        private List<UnitStateMachineBase> Units { get; } = new();
        protected override void Awake()
        {
            
        }

        protected override void Start()
        {
            List<CardData> flattenDeck = deck.cards
                .SelectMany(c => Enumerable.Repeat(c.card, c.quantity))
                .ToList();
            flattenDeck.Shuffle();
            Stack<CardData> shuffleDeck = new(flattenDeck);
            for (int i = 0; i < 6; i++)
            {
                _hand.Add(shuffleDeck.Pop());
            }
            // TODO : need draw card at the start of each round
            PlacementState = new AIPlacementState(this, _hand, essenceMarine, Units);
            FightingState = new AIFightingState(this);
            AISupportState = new AISupportState(this, _hand, essenceMarine, Units);
            base.Start();
        }

        protected override BaseState GetInitialState()
        {
            return PlacementState;
        }
        
        public void OnRoundEnd()
        {
            ChangeState(AISupportState);
        }
        
    }
}