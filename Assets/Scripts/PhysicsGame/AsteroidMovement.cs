using UnityEngine;

namespace PhysicsGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AsteroidMovement : MonoBehaviour
    {
        private const float MaxDistanceY = 6f;
        private const float FramesPerSecond = 4f;

        [SerializeField] private Sprite[] frames;
        
        private SpriteRenderer _spriteRenderer;
        
        private float _speedY = 3f;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            transform.Translate(0f, -_speedY * Time.deltaTime, 0f);

            if (frames.Length > 0)
            {
                var i = Mathf.FloorToInt(Time.time * FramesPerSecond) % frames.Length;
                _spriteRenderer.sprite = frames[i];
            }

            if (transform.position.y < -MaxDistanceY)
            {
                Destroy(gameObject);
            }
        }

        public void Initialize(float speedY)
        {
            _speedY = speedY;
        }
    }
}
