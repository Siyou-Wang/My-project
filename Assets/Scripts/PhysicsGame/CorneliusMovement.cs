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

        private float _originX;
        private float _yPosition;
        private float _moveInput;

        private void Awake()
        {
            _originX = transform.position.x;
            _yPosition = transform.position.y;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!StartGameTp.StartPhys)
            {
                _moveInput = 0f;
                return;
            }
            
            
            _moveInput = Input.GetAxisRaw("Horizontal");
            if (_moveInput != 0f)
            {
                _spriteRenderer.flipX = _moveInput < 0f;
            }
        }

        private void FixedUpdate()
        {
            if (!StartGameTp.StartPhys) return;
            
            var x = _rigidbody.position.x + _moveInput * SpeedX * Time.fixedDeltaTime;
            x = Mathf.Clamp(x, _originX - MaxDistanceX, _originX + MaxDistanceX);
            
            _rigidbody.MovePosition(new Vector2(x, _yPosition));
        }
    }
}
