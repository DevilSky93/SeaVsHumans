using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(UnitStateMachineBase))]
    public class UnitHealthController : MonoBehaviour
    {
        private float _currentHealth;
        private int _maxHealth;

        public bool IsDead => _currentHealth <= 0;

        private void Awake()
        {
            _maxHealth = GetComponent<UnitStateMachineBase>().Card.Hp;
            _currentHealth = _maxHealth;
        }

        public void Heal(float healAmount)
        {
            _currentHealth += healAmount;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }

        }

        public void Hit(float dmg)
        {
            Debug.Log($"{name} got hit for {dmg} damage");
            _currentHealth -= dmg;
            if (_currentHealth <= 0)
            {
                // TODO : Game Over
            }
        }
    }
}