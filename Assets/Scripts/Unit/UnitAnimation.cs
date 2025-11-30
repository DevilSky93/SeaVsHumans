using UnityEngine;

namespace Unit
{
    public class UnitAnimation
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Velocity = Animator.StringToHash("Velocity");
        private static readonly int Hurt = Animator.StringToHash("Hurt");
        private static readonly int Died = Animator.StringToHash("Died");
        private readonly Animator _animator;

        public UnitAnimation(Animator animator)
        {
            _animator = animator;
        }

        public void AttackAnimation()
        {
            _animator.SetTrigger(Attack);
        }

        public void WalkAnimation(float velocity)
        {
            _animator.SetFloat(Velocity, velocity);
        }
        
        public void HurtAnimation()
        {
            _animator.SetTrigger(Hurt);
        }

        public void DiedAnimation()
        {
            _animator.SetTrigger(Died);
        }
    }
}