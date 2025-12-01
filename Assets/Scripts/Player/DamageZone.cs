using Unit;
using UnityEngine;

namespace Player
{
    public class DamageZone : MonoBehaviour
    {
        [SerializeField] private LayerMask damageLayer;
        [SerializeField] private HealthController healthController;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((damageLayer.value & (1 << other.gameObject.layer)) > .1f)
            {
                UnitStateMachineBase unit = other.GetComponent<UnitStateMachineBase>();
                healthController.Hit(Mathf.Floor(Mathf.Min(unit.Card.RemainingHp / 3f, 5)));
                unit.DestroyUnit();
            }
        }
    }
}