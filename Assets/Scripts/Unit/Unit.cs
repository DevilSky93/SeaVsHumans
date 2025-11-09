using Cards.Enum;
using Cards.Models;
using UnityEngine;

namespace Unit
{
    public class Unit
    {
        public string CardName { get; set; }

        public int EssenceMarine { get; set; }

        public int GoldValue { get; set; }

        public int CostValue { get; set; }

        public int Hp { get; set; }

        public AreaTarget AreaTarget { get; set; }

        public int Attack { get; set; }

        public float Speed { get; set; }

        public Sprite CardImage { get; set; }

        public string Description { get; set; }
        public CardType CardType { get; set; }

        public Unit(CardData cardData)
        {
            CardName = cardData.cardName;
            Description = cardData.description;
            CardImage = cardData.cardImage;
            EssenceMarine = cardData.essenceMarine;
            GoldValue = cardData.goldValue;
            CostValue = cardData.costValue;
            AreaTarget = cardData.areaTarget;
            Hp = cardData.hp;
            Attack = cardData.attack;
            Speed = cardData.speed;
            CardType = cardData.cardType;
        }
    }
}