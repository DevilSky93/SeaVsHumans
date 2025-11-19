using Grid;
using UnityEngine;

namespace Unit.Types
{
    public class BigWave : Wave
    {
        public override void OnPlaceUnit(float x, float y)
        {
            Vector2? position = GridManager.Instance.GetPositionInGrid(x, y);
            if (!position.HasValue) return;
            GridManager.Instance.SetPositionOccupiedInGrid(x, y, true);
            GridManager.Instance.SetPositionOccupiedInGrid(x + 1, y, true);
            GridManager.Instance.SetPositionOccupiedInGrid(x, y + 1, true);
            GridManager.Instance.SetPositionOccupiedInGrid(x + 1, y + 1, true);
            Vector3 newPos = new(position.Value.x, position.Value.y, 0);
            transform.position = newPos;
            onPlaceUnit.enabled = false;

            ChangeState(IdleState);
        }
    }
}