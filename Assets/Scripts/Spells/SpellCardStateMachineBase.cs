using StateMachine;

namespace Spells
{
    public abstract class SpellCardStateMachineBase : SpellStateMachineBase
    {
        public BaseState SpellNoTargetState { get; private set; }
        public BaseState SpellActionState { get; protected set; }

        protected override void Awake()
        {
            SpellNoTargetState = new SpellNoTargetState(this);
        }
        
        public void OnPlaySpell()
        {
            ChangeState(SpellActionState);
        }

        public void OnDestroyUnit()
        {
            Destroy(gameObject);
        }

        protected override BaseState GetInitialState()
        {
            return SpellNoTargetState;
        }
    }
}