using Events.Float;
using UnityEngine;

namespace EssenceMarine
{
    public class EssenceMarine : MonoBehaviour
    {
        [SerializeField] private int current;
        [SerializeField] private int maxAmount;
        [SerializeField] private int maxEssenceLimit;
        [SerializeField] private EventFloat onEssenceChanged;

        public int Current => current;
        public int MaxAmount => maxAmount;

        public void OnEssenceSpend(float amount)
        {
            if (current == current + (int)amount) return;

            current = Mathf.Clamp(current + (int)amount, 0, maxAmount);
            onEssenceChanged.Raise(current);
        }
        
        public bool HaveEnoughEssence(int amount)
        {
            return amount <= current;
        }
        
        public void OnRoundEnd()
        {
            if (!HasReachedMaxLimit())
            {
                maxAmount += 1;
            }
            current = maxAmount;
            onEssenceChanged.Raise(current);
        }

        private bool HasReachedMaxLimit()
        {
            return maxAmount >= maxEssenceLimit;
        }
    }
}