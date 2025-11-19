using Events.FloatFloat;
using UnityEngine;

namespace Cards.Models
{
    public class UnitCard : CardBase
    {
        [SerializeField] private GameEventFloatFloatListener onPlaceUnitListener;

        // Deactivate Placing
        public void OnDestroyUnit()
        {
            onPlaceUnitListener.enabled = false;
        }

        protected override void CardPreExecute()
        {
            onPlaceUnitListener.enabled = true;
        }

        protected override void CardPostExecute()
        {
            
        }
    }
}