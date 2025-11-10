using System;
using Cards.Enum;
using Cards.Models;
using UnityEngine;

namespace Cards
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField] private CardDatabase cardDatabase;

        private static CardFactory _instance;
        public static CardFactory Instance { get; private set; }
        
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

        public static StateMachine.StateMachine Build(CardData cardData)
        {
            switch (cardData.cardType)
            {
                case CardType.Field:
                    break;
                case CardType.Spell:
                    return SpellFactory.BuildSpell(cardData, Instance.cardDatabase.spells);
                case CardType.Unit:
                    return UnitFactory.BuildUnit(cardData, Instance.cardDatabase.units);
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }
    }
}