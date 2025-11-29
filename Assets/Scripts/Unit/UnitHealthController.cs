using System;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(UnitStateMachineBase))]
    public class UnitHealthController : MonoBehaviour
    {
        public float CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }

        public event Action<float> OnHealthChanged;

        public bool IsDead => CurrentHealth <= 0;

        private void Awake()
        {
            MaxHealth = GetComponent<UnitStateMachineBase>().Card.Hp;
            CurrentHealth = MaxHealth;
        }

        public void Heal(float healAmount)
        {
            CurrentHealth += healAmount;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            OnHealthChanged?.Invoke(CurrentHealth / MaxHealth);
        }

        public void Hit(float dmg)
        {
            Debug.Log($"{name} got hit for {dmg} damage");
            CurrentHealth -= dmg;
            if (CurrentHealth <= 0)
            {
                // TODO : Game Over
            }
            OnHealthChanged?.Invoke(CurrentHealth / MaxHealth);
        }
    }
}