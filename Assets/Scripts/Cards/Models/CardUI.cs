using Cards.Enum;
using TMPro;
using UnityEngine;

namespace Cards.Models
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text cardNameText;
        [SerializeField] private TMP_Text attackText;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private SpriteRenderer iconImage;

        private Unit.Unit _unit;
        private void Awake()
        {
            _unit = GetComponent<CardBase>().Unit;
            if (_unit.CardType == CardType.Unit)
            {
                hpText.text = _unit.Hp.ToString();
                attackText.text = _unit.Attack.ToString();   
            }
            cardNameText.text = _unit.CardName;
            costText.text = _unit.EssenceMarine.ToString();
            descriptionText.text = _unit.Description;
            iconImage.sprite = _unit.CardImage;
        }
    }
}