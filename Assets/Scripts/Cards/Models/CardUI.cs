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
            cardNameText.text = _unit.CardName;
            attackText.text = _unit.Attack.ToString();
            hpText.text = _unit.Hp.ToString();
            costText.text = _unit.EssenceMarine.ToString();
            descriptionText.text = _unit.Description;
            iconImage.sprite = _unit.CardImage;
        }
    }
}