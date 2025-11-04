using System.Collections.Generic;
using System.Linq;
using Cards.Models;
using Unit;

namespace Cards
{
    public static class UnitFactory
    {
        public static UnitStateMachine BuildUnit(CardData cardData, List<UnitStateMachine> unitDatabase)
        {
            UnitStateMachine unit = unitDatabase.FirstOrDefault(u => u.Unit.CardName.Equals(cardData.cardName));
            return unit;
        }
    }
}