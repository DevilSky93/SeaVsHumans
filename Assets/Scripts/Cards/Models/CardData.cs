using Cards.Enum;
using UnityEngine;

namespace Cards.Models
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Unit/Unit Data", order = 1)]
    public class CardData : ScriptableObject
    {
        public string cardName;
        [TextArea(3, 10)]
        public string description;
        public Sprite cardImage;
        public int essenceMarine;
        public int goldValue;
        public AreaTarget areaTarget;
        public int hp;
        public int attack;
        public float speed;
        public CardType cardType;
    }
}