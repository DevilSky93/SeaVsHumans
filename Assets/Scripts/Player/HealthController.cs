using System.Globalization;
using Events.Trigger;
using TMPro;
using UnityEngine;

namespace Player
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private EventTrigger onGameOver;
        private float _currentHealth;

        private bool IsDead => _currentHealth <= 0;

        private void Awake()
        {
            _currentHealth = maxHealth;
            UpdateHealthText();
        }

        public void Heal(float healAmount)
        {
            _currentHealth += healAmount;
            if (_currentHealth > maxHealth)
            {
                _currentHealth = maxHealth;
            }

        }

        public void Hit(float dmg)
        {
            Debug.Log($"{name} got hit for {dmg} damage");
            _currentHealth -= dmg;
            UpdateHealthText();
            if (IsDead)
            {
                Debug.LogWarning("Game over");
                onGameOver.Raise();
            }
        }

        private void UpdateHealthText()
        {
            healthText.text = _currentHealth.ToString(CultureInfo.InvariantCulture);
        }
    }
}