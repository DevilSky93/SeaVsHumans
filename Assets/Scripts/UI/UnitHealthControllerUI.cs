using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnitHealthControllerUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image fillUI;
        private UnitHealthController _healthController;

        private void Awake()
        {
            _healthController = GetComponent<UnitHealthController>();
            _healthController.OnHealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(float amount)
        {
            healthSlider.value = amount;
            fillUI.color = amount <= .3f ? Color.red : Color.green;
        }
    }
}