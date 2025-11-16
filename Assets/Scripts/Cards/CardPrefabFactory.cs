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

        public CardBase Build(CardType type)
        {
            switch (type)
            {
                case CardType.Unit:
                    return unitCardPrefab;
                case CardType.Spell:
                    return spellNoTargetCardPrefab;
                case CardType.Field:
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}