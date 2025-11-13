using StateMachine;

namespace Unit.Types
{
    public class EnemyUnit : Wave
    {
        protected override BaseState GetInitialState()
        {
            return IdleState;
        }
    }
}