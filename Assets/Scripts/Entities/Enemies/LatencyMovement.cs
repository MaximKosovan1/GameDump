using UnityEngine;

public class LatencyMovement : MonoBehaviour, IMovable
{
    private Transform _playerTransform;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _initialRotationStrength = 10f; 
    [SerializeField] private float _minRotationStrength = 1f;
    [SerializeField] private float _rotationAcceleration = 2f; 
    [SerializeField] private float _acceleration = 2f; 
    [SerializeField] private float _rotationAngle = 20f; 

    private Vector2 _currentVelocity;
    private float _rotationStrength;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        _rigidbody.gravityScale = 0f;
        _rigidbody.isKinematic = true;
        _acceleration += Random.Range(0, 0.5f);
        _rotationStrength = _initialRotationStrength;
    }

    public void Move(float movementSpeed)
    {
        if (_playerTransform == null) return;

        Vector2 directionToPlayer = (_playerTransform.position - transform.position).normalized;
        Vector2 desiredVelocity = directionToPlayer * movementSpeed;
        _currentVelocity = Vector2.Lerp(_currentVelocity, desiredVelocity, _acceleration * Time.fixedDeltaTime);
        _currentVelocity = Vector2.ClampMagnitude(_currentVelocity, movementSpeed);
        _rigidbody.MovePosition((Vector2)transform.position + _currentVelocity * Time.fixedDeltaTime);

        float smoothAngle = 0f;
        if (_currentVelocity.x > 0.1f)
        {
            smoothAngle = Mathf.LerpAngle(transform.eulerAngles.z, -_rotationAngle, _rotationStrength * Time.fixedDeltaTime);
            _spriteRenderer.flipX = false; // Face right
        }
        else if (_currentVelocity.x < -0.1f)
        {
            smoothAngle = Mathf.LerpAngle(transform.eulerAngles.z, _rotationAngle, _rotationStrength * Time.fixedDeltaTime);
            _spriteRenderer.flipX = true; // Face left
        }

        transform.rotation = Quaternion.Euler(0, 0, smoothAngle);
        _rotationStrength = Mathf.Max(_minRotationStrength, _rotationStrength - _rotationAcceleration * Time.fixedDeltaTime);
    }
}
