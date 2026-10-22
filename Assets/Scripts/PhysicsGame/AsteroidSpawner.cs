using UnityEngine;

namespace PhysicsGame
{
    public class AsteroidSpawner : MonoBehaviour
    {
        private const float CenterX = 80f;
        private const float MaxDistanceX = 8f;
        private const float SpawnPositionY = -13f;

        private const float Duration = 60f;
        private const float StartInterval = 1.2f;
        private const float MinInterval = 0.25f;
        private const float IntervalShrinkPerSecond = 0.02f;

        private const float InitialSpeed = 3f;
        private const float MaxSpeed = 10f;
        private const float SpeedIncreasePerSecond = 0.15f;

        [SerializeField] private AsteroidMovement asteroidPrefab;

        private float _timer;
        private float _elapsed;

        private void Update()
        {
            if (!StartGameTp.StartPhys)
            {
                _elapsed = 0f;
                _timer = 0f;
                return;
            }
            
            _elapsed += Time.deltaTime;
            _timer += Time.deltaTime;

            if (_elapsed >= Duration) return;

            var interval = Mathf.Max(MinInterval, StartInterval - _elapsed * IntervalShrinkPerSecond);
            if (_timer < interval) return;

            _timer -= interval;
            Spawn();
        }

        private void Spawn()
        {
            var speed = Mathf.Min(MaxSpeed, InitialSpeed + _elapsed * SpeedIncreasePerSecond);
            var spawnPositionX = Random.Range(CenterX - MaxDistanceX, CenterX + MaxDistanceX);
            var asteroid = Instantiate(asteroidPrefab, new Vector3(spawnPositionX, SpawnPositionY, 0f), Quaternion.identity, transform);
            
            asteroid.Initialize(speed);
        }
    }
}
