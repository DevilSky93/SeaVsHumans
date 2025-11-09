using System;
using Cards.Enum;
using Cards.Models;
using Events.Float;
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
        [SerializeField] private EventFloat onEssenceSpend;

        public Unit.Unit Unit { get; private set; }

        private CardBase _cardBase;
        public event Action<CardBase> OnDestroyRequested;
        public SpriteRenderer BackgroundSpriteRenderer => backgroundSpriteRenderer;
        public SpriteRenderer ImageSpriteRenderer => imageSpriteRenderer;
        public Canvas CanvasRenderer => canvasRenderer;

        private void Awake()
        {
            _cardBase = GetComponent<CardBase>();
            Unit = _cardBase.Unit;
            if (Unit.CardType == CardType.Unit)
            {
                hpText.text = Unit.Hp.ToString();
                attackText.text = Unit.Attack.ToString();   
            }
            cardNameText.text = Unit.CardName;
            costText.text = Unit.EssenceMarine.ToString();
            descriptionText.text = Unit.Description;
            iconImage.sprite = Unit.CardImage;
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
            onEssenceSpend.Raise(-Unit.CostValue);
            OnDestroyRequested?.Invoke(_cardBase);
        }
    }
}