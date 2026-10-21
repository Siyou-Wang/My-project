using System;
using UnityEngine;

namespace PhysicsGame
{
    public class CorneliusCollision : MonoBehaviour
    {
        public event Action Hit;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<AsteroidMovement>(out _)) return;
            
            Destroy(other.gameObject);
            Hit?.Invoke();
        }
    }
}
