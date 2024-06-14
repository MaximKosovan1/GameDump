using UnityEngine;

public class LatencyMovement : MonoBehaviour, IMovable
{
    private Transform _playerTransform;
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _rotationAcceleration = 2f;
    [SerializeField] private float _rotationStrength = 10f;
    [SerializeField] private float _minRotationStrength = 1f;
    [SerializeField] private float _acceleration = 1f;

    // Velocity and rotation variables
    private Vector2 _currentVelocity = Vector2.zero;
    private float _currentRotationSpeed = 0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        _rigidbody.gravityScale = 0f;
        _rigidbody.isKinematic = true;
    }

    public void Move(float movementSpeed)
    {
        if (_playerTransform == null) return;

        Vector2 direction = (_playerTransform.position - transform.position).normalized;
        float targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        _rotationStrength = Mathf.Max(_minRotationStrength, _rotationStrength - _rotationAcceleration * Time.deltaTime);

        float angleDifference = Mathf.DeltaAngle(transform.rotation.eulerAngles.z, targetRotation);
        _currentRotationSpeed = angleDifference * _rotationStrength * Time.deltaTime;

        transform.Rotate(0f, 0f, _currentRotationSpeed);

        Vector2 desiredVelocity = direction * movementSpeed;

        _currentVelocity = Vector2.Lerp(_currentVelocity, desiredVelocity, _acceleration * Time.deltaTime);

        Vector2 newPosition = (Vector2)transform.position + _currentVelocity * Time.deltaTime;
        transform.position = newPosition;
    }
}
