using System.Globalization;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EssenceMarineUI : MonoBehaviour
    {
        [SerializeField] private EssenceMarine.EssenceMarine essenceMarine;
        [SerializeField] private TMP_Text current;
        [SerializeField] private TMP_Text maxAmount;

        private void Awake()
        {
            current.text = essenceMarine.Current.ToString();
            maxAmount.text = essenceMarine.MaxAmount.ToString();
        }
        
        public void OnEssenceChanged(float newCurrent)
        {
            current.text = newCurrent.ToString(CultureInfo.InvariantCulture);
        }

        public void OnMaxEssenceChanged(float newCurrent)
        {
            maxAmount.text = essenceMarine.MaxAmount.ToString(CultureInfo.InvariantCulture);
        }
    }
}