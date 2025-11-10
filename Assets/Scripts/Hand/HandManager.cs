using System;
using System.Collections.Generic;
using System.Linq;
using Cards.Interfaces;
using Cards.Models;
using DG.Tweening;
using UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace Hand
{
    public class HandManager : MonoBehaviour
    {
        [SerializeField] private int maxHandSize;
        [SerializeField] private CardUI cardPrefab;
        [SerializeField] private EssenceMarine.EssenceMarine essenceMarine;
        [SerializeField] private SplineContainer splineContainer;
        [SerializeField] private Transform spawnPoint;
        
        private List<CardUI> _handCards = new();

        private void Start()
        {
            for (int i = 0; i < maxHandSize; i++)
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
            for (int i = Math.Min(_handCards.Count, maxHandSize); i < maxHandSize; i++)
            {
                DrawCard();
            }
        }

        private void DrawCard()
        {
            if (_handCards.Count >= maxHandSize) return;
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

            float cardSpacing = 1f / maxHandSize;
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