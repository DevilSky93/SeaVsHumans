using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

namespace Hand
{
    public class HandManager : MonoBehaviour
    {
        [SerializeField] private int maxHandSize;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private SplineContainer splineContainer;
        [SerializeField] private Transform spawnPoint;
        
        private readonly List<GameObject> _handCards = new();

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                DrawCard();
            }
        }

        private void DrawCard()
        {
            if (_handCards.Count >= maxHandSize) return;
            GameObject newCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity, transform);
            _handCards.Add(newCard);
            UpdateCardPositions();
        }

        private void UpdateCardPositions()
        {
            if (!_handCards.Any()) return;

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
                
                _handCards[i].transform.DOMove(splinePosition, .25f);
                _handCards[i].transform.DOLocalRotateQuaternion(rotation, .25f);
            }
        }
        
    }
}