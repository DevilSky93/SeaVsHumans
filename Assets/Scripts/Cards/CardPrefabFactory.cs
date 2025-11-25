using System;
using Cards.Enum;
using Cards.Models;
using UnityEngine;

namespace Cards
{
    public class CardPrefabFactory : MonoBehaviour
    {
        [SerializeField] private UnitCard unitCardPrefab;
        [SerializeField] private SpellNoTargetCard spellNoTargetCardPrefab;
        [SerializeField] private SpellWithTargetCard spellWithTargetCardPrefab;

        public CardBase Build(CardType type)
        {
            switch (type)
            {
                case CardType.Field:
                case CardType.Unit:
                    return unitCardPrefab;
                case CardType.SpellNoTarget:
                    return spellNoTargetCardPrefab;
                case CardType.SpellWithTarget:
                    return spellWithTargetCardPrefab;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}