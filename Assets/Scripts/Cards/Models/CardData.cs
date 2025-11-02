using Cards.Enum;
using UnityEngine;

namespace Cards.Models
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Card/Card Data", order = 1)]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public string description;
        public Sprite cardImage;
        public int essenceMarine;
        public int goldValue;
        public AreaTarget areaTarget;
        public int hp;
        public int attack;
        public float speed;
    }
}