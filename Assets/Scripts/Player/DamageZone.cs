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
                UnitStateMachine unit = other.GetComponent<UnitStateMachine>();
                healthController.Hit(Mathf.Floor(unit.Unit.RemainingHp / 2f));
                unit.DestroyUnit();
            }
        }
    }
}