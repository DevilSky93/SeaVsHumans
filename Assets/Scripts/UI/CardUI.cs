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
            Card = _cardBase.Card;
            if (Card.CardType == CardType.Unit)
            {
                hpText.text = Card.Hp.ToString();
                attackText.text = Card.Attack.ToString();   
            }
            cardNameText.text = Card.CardName;
            costText.text = Card.EssenceMarine.ToString();
            descriptionText.text = Card.Description;
            iconImage.sprite = Card.CardImage;
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
            onEssenceSpend.Raise(-Card.EssenceMarine);
            OnDestroyRequested?.Invoke(_cardBase);
        }

        public void OnPlaySpell()
        {
            transform.DOKill();
            OnDestroyRequested?.Invoke(_cardBase);
        }
    }
}