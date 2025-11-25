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
            return cardData.cardType switch
            {
                CardType.SpellNoTarget or CardType.SpellWithTarget => SpellFactory.BuildSpell(cardData,
                    Instance.cardDatabase.spells),
                CardType.Field or CardType.Unit => UnitFactory.BuildUnit(cardData, Instance.cardDatabase.units),
                _ => throw new ArgumentOutOfRangeException("No such card type")
            };
        }
    }
}