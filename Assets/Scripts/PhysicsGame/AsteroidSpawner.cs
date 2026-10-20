using UnityEngine;

namespace PhysicsGame
{
    public class AsteroidSpawner : MonoBehaviour
    {
        private const float MaxDistanceX = 8f;
        private const float SpawnPositionY = 6f;

        private const float Duration = 30f;
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
            _elapsed += Time.deltaTime;
            _timer += Time.deltaTime;

            if (_elapsed >= Duration)
            {
                enabled = false;
                return;
            }

            var interval = Mathf.Max(MinInterval, StartInterval - _elapsed * IntervalShrinkPerSecond);

            if (_timer < interval) return;

            _timer -= interval;
            Spawn();
        }

        private void Spawn()
        {
            var speed = Mathf.Min(MaxSpeed, InitialSpeed + _elapsed * SpeedIncreasePerSecond);
            var spawnPositionX = Random.Range(-MaxDistanceX, MaxDistanceX);
            var asteroid = Instantiate(asteroidPrefab, new Vector2(spawnPositionX, SpawnPositionY), Quaternion.identity, transform);
            
            asteroid.Initialize(speed);
        }
    }
}
