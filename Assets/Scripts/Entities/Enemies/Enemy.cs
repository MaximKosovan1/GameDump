using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SimpleFlash))]
public class Enemy : Entity
{
    private SimpleFlash _flashEffect;
    public event Action<Enemy> OnDeath;
    private IMovable _enemyMovement;
    private Rigidbody2D _rigidbody;
    private string _playerTag = "Player";

    [SerializeField] private float walkSpeed = 3.5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackCooldown = 2.0f; // Время перезарядки атаки

    private float _lastAttackTime;

    private void Awake()
    {
        _enemyMovement = GetComponent<IMovable>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _flashEffect = GetComponent<SimpleFlash>();
        _lastAttackTime = -attackCooldown; // Обеспечить, чтобы враг мог атаковать сразу после начала
    }

    private void Update()
    {
        _enemyMovement.Move(walkSpeed);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag(_playerTag) && Time.time >= _lastAttackTime + attackCooldown)
        {
            Vector2 forceDirection = (other.transform.position - transform.position).normalized;
            other.collider.GetComponent<Player>().TakeDamage(damage, forceDirection);
            _lastAttackTime = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(_playerTag) && Time.time >= _lastAttackTime + attackCooldown)
        {
            Vector2 forceDirection = (other.transform.position - transform.position).normalized;
            other.GetComponent<Player>().TakeDamage(damage, forceDirection);
            _lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damage, Vector2 forceDirection)
    {
        CurrentHealthPoints -= damage;
        _flashEffect.Flash();

        if (CurrentHealthPoints <= 0)
        {
            CurrentHealthPoints = 0;
            ProcessDeath();
        }
        else
        {
            ApplyForce(-forceDirection);
        }
    }

    private void ApplyForce(Vector2 forceDirection)
    {
        _rigidbody.AddForce(forceDirection, ForceMode2D.Impulse);
    }

    public void ProcessDeath()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}