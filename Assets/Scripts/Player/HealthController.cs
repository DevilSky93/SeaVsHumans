using UnityEngine;

namespace Player
{
    public class HealthController : MonoBehaviour
    {
        // [SerializeField] private HealthChangedEvent onHealthChanged;
        [SerializeField] private float maxHealth;
        private float _currentHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void Heal(float healAmount)
        {
            _currentHealth += healAmount;
            if (_currentHealth > maxHealth)
            {
                _currentHealth = maxHealth;
            }

            // onHealthChanged?.Raise(new FloatFloat(_currentHealth, maxHealth));
        }

        public void Hit(float dmg)
        {
            Debug.Log($"{name} got hit for {dmg} damage");
            _currentHealth -= dmg;
            if (_currentHealth <= 0)
            {
                // TODO : Game Over
            }

            // onHealthChanged?.Raise(new FloatFloat(_currentHealth, maxHealth));
        }
    }
}