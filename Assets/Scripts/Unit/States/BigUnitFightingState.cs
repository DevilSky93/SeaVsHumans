using UnityEngine;

namespace Unit.States
{
    public class BigUnitFightingState : UnitFightingState
    {
        public BigUnitFightingState(UnitStateMachineBase state, UnitHealthController unitHealthController, LayerMask enemyLayerMask) 
            : base(state, unitHealthController, enemyLayerMask)
        {
        }

        public override void Enter()
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(new Vector2(state.transform.position.x, state.transform.position.y + .5f),
                state.transform.right, 5, enemyLayerMask);
            if (!raycastHit2D)
            {
                raycastHit2D = Physics2D.Raycast(
                    new Vector2(state.transform.position.x, state.transform.position.y - .5f),
                    state.transform.right, 5, enemyLayerMask);
            }
            overlapEnemy = raycastHit2D ? raycastHit2D.collider.GetComponent<UnitStateMachineBase>() : null;
        }
    }
}