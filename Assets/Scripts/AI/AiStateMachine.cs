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
        private Stack<CardData> _shuffleDeck;
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
            _shuffleDeck = new Stack<CardData>(flattenDeck);
            DrawCards(6);
            PlacementState = new AIPlacementState(this, _hand, essenceMarine, Units);
            FightingState = new AIFightingState(this);
            AISupportState = new AISupportState(this, _hand, essenceMarine, Units, DrawCards);
            base.Start();
        }

        private void DrawCards(int amountToDraw)
        {
            for (int i = 0; i < amountToDraw; i++)
            {
                _hand.Add(_shuffleDeck.Pop());
            }
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