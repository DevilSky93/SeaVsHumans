using System;
using System.Collections.Generic;
using System.Linq;
using Cards.Interfaces;
using DG.Tweening;
using Player;
using UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

namespace Hand
{
    public class HandManager : MonoBehaviour
    {
        private const float MaxCardInHand = 10;
        [SerializeField] private int startingCardsHandNumber;
        [SerializeField] private CardUI cardPrefab;
        [SerializeField] private EssenceMarine.EssenceMarine essenceMarine;
        [SerializeField] private SplineContainer splineContainer;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private PlayerInputControls playerInputControls;
        
        private List<CardUI> _handCards = new();

        private void Start()
        {
            for (int i = 0; i < startingCardsHandNumber; i++)
            {
                DrawCard();
            }
        }
        
        public void OnCardsDrawn(float cardsToDraw)
        {
            for (int i = 0; i < cardsToDraw; i++)
            {
                DrawCard();
            }
        }

        public void OnRoundEnd()
        {
            for (int i = Math.Min(_handCards.Count, startingCardsHandNumber); i < startingCardsHandNumber; i++)
            {
                if (_handCards.Count >= startingCardsHandNumber) return;
                DrawCard();
            }
        }

        private void DrawCard()
        {
            CardUI newCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity, transform);
            newCard.OnDestroyRequested += HandleDestroyRequested(newCard);
            newCard.GetComponent<ICard>().OnPlacingRequested += HandlePlacingRequested(newCard.Card.EssenceMarine);
            _handCards.Add(newCard);
            UpdateCardPositions();
        }

        private Func<bool> HandlePlacingRequested(int unitCostValue)
        {
            return () => essenceMarine.HaveEnoughEssence(unitCostValue);
        }

        private void UpdateCardPositions()
        {
            if (!_handCards.Any()) return;
             _handCards = _handCards.Where(c => c != null).ToList();
            _handCards.ForEach(c => c.SetIsPlaced());

            const float cardSpacing = 1f / MaxCardInHand;
            float firstCardPosition = 0.5f - (_handCards.Count - 1) * cardSpacing / 2f;
            Spline spline = splineContainer.Spline;
            
            for (int i = 0; i < _handCards.Count; i++)
            {
                float p = firstCardPosition + i * cardSpacing;
                float3 splinePosition = spline.EvaluatePosition(p);
                float3 forward = spline.EvaluateTangent(p);
                float3 up = spline.EvaluateUpVector(p);
                Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);

                int index = i;
                _handCards[i].BackgroundSpriteRenderer.sortingOrder = i;
                _handCards[i].ImageSpriteRenderer.sortingOrder = i + 1;
                _handCards[i].CanvasRenderer.sortingOrder = i;
                _handCards[i].transform.DOMove(splinePosition, .25f).OnComplete(() => _handCards[index].IsPlaced());
                _handCards[i].transform.DOLocalRotateQuaternion(rotation, .25f);
            }
        }

        private Action<ICard> HandleDestroyRequested(CardUI newCard)
        {
            return _ =>
            {
                _handCards.Remove(newCard);
                UpdateCardPositions();
                Destroy(newCard.gameObject);
            };
        }
    }
}