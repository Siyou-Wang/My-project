using UnityEngine;

namespace PhysicsGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CorneliusMovement : MonoBehaviour
    {
        private const float SpeedX = 8f;
        private const float MaxDistanceX = 8f;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        
        private float _yPosition;
        private float _moveInput;

        private void Awake()
        {
            _yPosition = transform.position.y;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _moveInput = Input.GetAxisRaw("Horizontal");

            if (_moveInput != 0f)
            {
                _spriteRenderer.flipX = _moveInput < 0f;
            }
        }

        private void FixedUpdate()
        {
            var newPositionX = _rigidbody.position.x + _moveInput * SpeedX * Time.fixedDeltaTime;
            newPositionX = Mathf.Clamp(newPositionX, -MaxDistanceX, MaxDistanceX);
            _rigidbody.MovePosition(new Vector2(newPositionX, _yPosition));
        }
    }
}
