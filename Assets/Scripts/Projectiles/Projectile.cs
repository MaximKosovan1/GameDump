using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed = 1f;
    
    private int _damage = 1;
    private Rigidbody2D _bulletRigidbody2D;
    private Vector2 _direction;

    private void Awake()
    {
        _bulletRigidbody2D = GetComponent<Rigidbody2D>();
        _direction = transform.right; 
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        _bulletRigidbody2D.velocity = _direction * _projectileSpeed;
    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var isEnemy = other.TryGetComponent(out Enemy enemy);
        if (isEnemy)
        {
            enemy.TakeDamage(_damage, -_direction); 
        }
        ProcessImpact();
    }

    private void ProcessImpact()
    {
        Destroy(gameObject);
    }
}