using Events.Bool;
using Events.FloatFloat;
using UnityEngine;

namespace Cards.Models
{
    public class UnitCard : CardBase
    {
        [SerializeField] private EventBool canPlaceUnitEvent;
        [SerializeField] private GameEventFloatFloatListener onPlaceUnitListener;

        public void DeactivatePlacing()
        {
            onPlaceUnitListener.enabled = false;
        }

        protected override void AllowPlace()
        {
            canPlaceUnitEvent.Raise(true);
            onPlaceUnitListener.enabled = true;
        }
    }
}