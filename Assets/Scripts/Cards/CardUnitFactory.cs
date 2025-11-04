using System;
using Cards.Enum;
using Cards.Models;
using Unit;
using UnityEngine;

namespace Cards
{
    public class CardUnitFactory : MonoBehaviour
    {
        [SerializeField] private CardDatabase cardDatabase;

        private static CardUnitFactory _instance;
        public static CardUnitFactory Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public UnitStateMachine Build(CardData cardData)
        {
            switch (cardData.cardType)
            {
                case CardType.Field:
                    break;
                case CardType.Spell:
                    break;
                case CardType.Unit:
                    return UnitFactory.BuildUnit(cardData, Instance.cardDatabase.units);
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }
    }
}