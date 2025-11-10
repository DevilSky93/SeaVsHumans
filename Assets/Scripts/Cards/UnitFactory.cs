using System.Collections.Generic;
using System.Linq;
using Cards.Models;
using Spells;
using Unit;

namespace Cards
{
    public static class UnitFactory
    {
        public static UnitStateMachineBase BuildUnit(CardData cardData, List<UnitStateMachineBase> unitDatabase)
        {
            UnitStateMachineBase unit = unitDatabase.FirstOrDefault(u => u.Card.CardName.Equals(cardData.cardName));
            return unit;
        }
    }
    
    public static class SpellFactory
    {
        public static SpellStateMachineBase BuildSpell(CardData cardData, List<SpellStateMachineBase> spellDatabase)
        {
            SpellStateMachineBase spell = spellDatabase.FirstOrDefault(u => u.Card.CardName.Equals(cardData.cardName));
            return spell;
        }
    }
}