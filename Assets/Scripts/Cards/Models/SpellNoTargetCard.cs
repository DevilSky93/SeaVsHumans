using Events.Trigger;
using UnityEngine;

namespace Cards.Models
{
    public class SpellNoTargetCard : CardBase
    {
        [SerializeField] private EventTrigger onTriggerCard;
        [SerializeField] private EventTrigger onPlaySpell;
        protected override void AllowPlace()
        {
            onPlaySpell.Raise();
        }
    }
}