using Cards.Models;
using UnityEngine;

namespace Spells
{
    public class SpellStateMachineBase : StateMachine.StateMachine
    {
        [SerializeField] protected CardData spellCardData;
        
        private Card _card;
        public Card Card => _card ??= new Card(spellCardData);
        protected override void Awake()
        {
            
        }
    }
}