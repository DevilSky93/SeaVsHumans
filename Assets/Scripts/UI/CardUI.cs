using System;
using Cards.Enum;
using Cards.Models;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text cardNameText;
        [SerializeField] private TMP_Text attackText;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private SpriteRenderer iconImage;

        [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
        [SerializeField] private SpriteRenderer imageSpriteRenderer;
        [SerializeField] private Canvas canvasRenderer;
        private Unit.Unit _unit;
        private CardBase _cardBase;
        
        public event Action<CardBase> OnDestroyRequested;

        public SpriteRenderer BackgroundSpriteRenderer => backgroundSpriteRenderer;
        public SpriteRenderer ImageSpriteRenderer => imageSpriteRenderer;
        public Canvas CanvasRenderer => canvasRenderer;

        private void Awake()
        {
            _cardBase = GetComponent<CardBase>();
            _unit = _cardBase.Unit;
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
        
        public void IsPlaced()
        {
            _cardBase.IsPlaced();
        }
        
        public void SetIsPlaced()
        {
            _cardBase.SetIsPlaced();
        }
        
        public void OnPlaceUnit(float x, float y)
        {
            OnDestroyRequested?.Invoke(_cardBase);
        }
    }
}