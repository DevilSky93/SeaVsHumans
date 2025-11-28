using Grid;
using Unit.States;
using UnityEngine;

namespace Unit.Types
{
    public class BigWave : Wave
    {
        protected override void Awake()
        {
            base.Awake();
            MovementState = new BigUnitMovementState(this, transform, unitCardData.speed, enemyLayerMask, tileMask);
            FightingState = new BigUnitFightingState(this, HealthController, enemyLayerMask);
        }

        public override void OnPlaceUnit(float x, float y)
        {
            Vector2? position = GridManager.Instance.GetPositionInGrid(x, y);
            if (!position.HasValue) return;
            GridManager.Instance.SetPositionOccupiedInGrid(x, y, true);
            GridManager.Instance.SetPositionOccupiedInGrid(x + 1, y, true);
            GridManager.Instance.SetPositionOccupiedInGrid(x, y - 1, true);
            GridManager.Instance.SetPositionOccupiedInGrid(x + 1, y - 1, true);
            Vector3 newPos = new(position.Value.x + GridManager.Instance.CellSize / 2, position.Value.y - GridManager.Instance.CellSize / 2, 0);
            transform.position = newPos;
            onPlaceUnit.enabled = false;

            ChangeState(IdleState);
        }

        public override void DestroyUnit()
        {
            GridManager.Instance.SetPositionOccupiedInGrid(transform.position.x - .5f, transform.position.y + .5f, false);
            GridManager.Instance.SetPositionOccupiedInGrid(transform.position.x + .5f, transform.position.y + .5f, false);
            GridManager.Instance.SetPositionOccupiedInGrid(transform.position.x - .5f, transform.position.y - .5f, false);
            GridManager.Instance.SetPositionOccupiedInGrid(transform.position.x + .5f, transform.position.y - .5f, false);
            Destroy(gameObject);
            onCheckUnitStillOnField.Raise();
        }
    }
}