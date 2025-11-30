using System;
using Cards.Enum;
using Cards.Interfaces;
using Cards.Models;
using DG.Tweening;
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

        public Card Card { get; private set; }

        private ICard _cardBase;
        public event Action<ICard> OnDestroyRequested;
        public SpriteRenderer BackgroundSpriteRenderer => backgroundSpriteRenderer;
        public SpriteRenderer ImageSpriteRenderer => imageSpriteRenderer;
        public Canvas CanvasRenderer => canvasRenderer;

        private void Awake()
        {
            _cardBase = GetComponent<ICard>();
        }
        
        public void IsPlaced()
        {
            _cardBase.IsPlaced();
        }
        
        public void SetIsPlaced()
        {
            Card = _cardBase.Card;
            FillCardData(Card);
            _cardBase.SetIsPlaced();
        }

        public void FillCardData(Card card)
        {
            if (card.CardType == CardType.Unit)
            {
                hpText.text = card.Hp.ToString();
                attackText.text = card.Attack.ToString();   
            }
            else
            {
                hpText.text = "";
                attackText.text = "";   
            }
            cardNameText.text = card.CardName;
            costText.text = card.EssenceMarine.ToString();
            descriptionText.text = card.Description;
            iconImage.sprite = card.CardImage;
        }

        public void OnPlaceUnit(float x, float y)
        {
            onEssenceSpend.Raise(-Card.EssenceMarine);
            OnDestroyRequested?.Invoke(_cardBase);
        }

        public void OnPlaySpell()
        {
            transform.DOKill();
            onEssenceSpend.Raise(-Card.EssenceMarine);
            OnDestroyRequested?.Invoke(_cardBase);
        }
    }
}