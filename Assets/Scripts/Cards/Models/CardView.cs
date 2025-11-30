using UI;
using UnityEngine;

namespace Cards.Models
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private CardUI cardUI;
        public static CardView Instance { get; private set; }

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShowCard(Card card)
        {
            cardUI.FillCardData(card);
            cardUI.gameObject.SetActive(true);
        }

        public void HideCard()
        {
            cardUI.gameObject.SetActive(false);
        }
    }
}