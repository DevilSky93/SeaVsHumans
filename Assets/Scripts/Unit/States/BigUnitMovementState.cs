using Grid;
using UnityEngine;

namespace Unit.States
{
    public class BigUnitMovementState : UnitMovementState
    {
        public BigUnitMovementState(UnitStateMachineBase state, Transform unitTransform, float unitSpeed, LayerMask enemyLayerMask, LayerMask tileMask)
            : base(state, unitTransform, unitSpeed, enemyLayerMask, tileMask)
        {
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            if ((enemyLayerMask.value & (1 << other.gameObject.layer)) > .1f)
            {
                state.ChangeState(state.FightingState);
            }

            if ((tileMask.value & (1 << other.gameObject.layer)) > .1f)
            {
                GridManager.Instance.SetPositionOccupiedInGrid(other.transform.position.x, other.transform.position.y, true);
                GridManager.Instance.SetPositionOccupiedInGrid(other.transform.position.x + 1, other.transform.position.y, true);
                GridManager.Instance.SetPositionOccupiedInGrid(other.transform.position.x, other.transform.position.y + 1, true);
                GridManager.Instance.SetPositionOccupiedInGrid(other.transform.position.x + 1, other.transform.position.y + 1, true);
            }
        }
        
        public override void OnTriggerExit2D(Collider2D other)
        {
            GridManager.Instance.SetPositionOccupiedInGrid(other.transform.position.x, other.transform.position.y, false);
            GridManager.Instance.SetPositionOccupiedInGrid(other.transform.position.x + 1, other.transform.position.y, false);
            GridManager.Instance.SetPositionOccupiedInGrid(other.transform.position.x, other.transform.position.y + 1, false);
            GridManager.Instance.SetPositionOccupiedInGrid(other.transform.position.x + 1, other.transform.position.y + 1, false);
        }
    }
}