using StateMachine;

namespace Spells
{
    public abstract class SpellCardStateMachineBase : SpellStateMachineBase
    {
        public BaseState SpellActionState { get; protected set; }
        public BaseState DestroyCardState { get; protected set; }
        protected override void Awake()
        {
            DestroyCardState = new DestroyCardState(this);
        }

        public void OnPlaySpell()
        {
            ChangeState(SpellActionState);
            ChangeState(DestroyCardState);
        }

        public void OnDestroyUnit()
        {
            Destroy(gameObject);
        }
    }
}