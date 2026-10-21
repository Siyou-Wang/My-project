using UnityEngine;

namespace PhysicsGame
{
    public class MoodManager : MonoBehaviour
    {
        private const int AngerPerHit = 1;
        private const int CalmPerTick = 1;
        private const float SecondsPerCalmTick = 5f;
        
        [SerializeField] private CorneliusCollision cornelius;

        private float _calmTimer;

        private void OnEnable() => cornelius.Hit += OnHit;
        private void OnDisable() => cornelius.Hit -= OnHit;

        private void Update()
        {
            _calmTimer += Time.deltaTime;

            if (_calmTimer < SecondsPerCalmTick) return;
            
            _calmTimer -= SecondsPerCalmTick;
            AngerAndCalm.setState(CalmPerTick);
        }

        private void OnHit()
        {
            AngerAndCalm.setState(-AngerPerHit);
            _calmTimer = 0f; 
        }
    }
}
