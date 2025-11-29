using System.Collections.Generic;
using Grid;
using StateMachine;
using Unit.Interfaces;
using UnityEngine;

namespace Unit.States
{
    public class UnitMovementState : BaseState, IPhysicsEventHandler
    {
        private readonly Transform _unitTransform;
        private readonly float _unitSpeed;
        protected readonly UnitStateMachineBase state;
        protected readonly LayerMask enemyLayerMask;
        protected readonly LayerMask tileMask;
        
        private readonly List<Vector2> _occupiedTiles = new();

        public UnitMovementState(UnitStateMachineBase state, Transform unitTransform, float unitSpeed,
            LayerMask enemyLayerMask, LayerMask tileMask) : base(state,
            "Card Movement State")
        {
            this.state = state;
            _unitTransform = unitTransform;
            _unitSpeed = unitSpeed;
            this.enemyLayerMask = enemyLayerMask;
            this.tileMask = tileMask;
        }

        public override void UpdateLogics()
        {
            _unitTransform.transform.Translate(Vector3.right * (_unitSpeed * Time.deltaTime));
        }

        public override void Exit()
        {
            // TODO : when go in fighting, do not clean occupied tiles
            foreach (Vector2 tile in _occupiedTiles)
            {
                GridManager.Instance.SetPositionOccupiedInGrid(tile.x, tile.y, false);
            }
            _occupiedTiles.Clear();
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if ((enemyLayerMask.value & (1 << other.gameObject.layer)) > .1f)
            {
                state.ChangeState(state.FightingState);
            }

            if ((tileMask.value & (1 << other.gameObject.layer)) > .1f)
            {
                GridManager.Instance.SetPositionOccupiedInGrid(other.transform.position.x, other.transform.position.y, true);
                _occupiedTiles.Add(other.transform.position);
            }
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            GridManager.Instance.SetPositionOccupiedInGrid(other.transform.position.x, other.transform.position.y, false);
        }
    }
}