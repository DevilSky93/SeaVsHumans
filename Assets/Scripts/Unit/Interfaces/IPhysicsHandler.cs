using UnityEngine;

namespace Unit.Interfaces
{
    public interface IPhysicsEventHandler
    {
        void OnTriggerEnter2D(Collider2D other);
        void OnTriggerExit2D(Collider2D other);
    }
}