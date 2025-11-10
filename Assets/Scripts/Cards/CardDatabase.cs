using System;
using System.Collections.Generic;
using Spells;
using Unit;

namespace Cards
{
    [Serializable]
    public class CardDatabase
    {
        public List<UnitStateMachineBase> units;
        public List<SpellStateMachineBase> spells;
        // TODO : spells and fields
    }
}