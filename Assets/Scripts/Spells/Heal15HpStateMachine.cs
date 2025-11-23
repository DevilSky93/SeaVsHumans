using Spells.States;

namespace Spells
{
    public class Heal15HpStateMachine : SpellCardStateMachineBase
    {
        public void OnTargetUnit(float amount)
        {
            SpellActionState = new HealActionState(this, (int)amount);
            ChangeState(SpellActionState);
            ChangeState(DestroyCardState);
        }
    }
}