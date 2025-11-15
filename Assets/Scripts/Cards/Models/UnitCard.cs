using Events.Bool;
using Events.FloatFloat;
using UnityEngine;

namespace Cards.Models
{
    public class UnitCard : CardBase
    {
        [SerializeField] private EventBool canPlaceUnitEvent;
        [SerializeField] private GameEventFloatFloatListener onPlaceUnitListener;

        // Deactivate Placing
        public void OnDestroyUnit()
        {
            onPlaceUnitListener.enabled = false;
        }

        protected override void CardPreExecute()
        {
            canPlaceUnitEvent.Raise(true);
            onPlaceUnitListener.enabled = true;
        }

        protected override void CardPostExecute()
        {
            
        }
    }
}