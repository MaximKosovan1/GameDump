using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SimpleFlash))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Entity
{
    private SimpleFlash _flashEffect;
    public event Action<Enemy> OnDeath;
    private IMovable _enemyMovement;
    private Rigidbody2D _rigidbody;
    public float WalkSpeed = 3.5f;

    private void Awake()
    {
        _enemyMovement = GetComponent<IMovable>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _flashEffect = GetComponent<SimpleFlash>();
    }
    private void Update()
    {
        _enemyMovement.Move(WalkSpeed);
    }

    public void TakeDamage(int damage, Vector2 forceDirection)
    {
        HealthPoints -= damage;
        _flashEffect.Flash();

        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
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
    private void ProcessDeath()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}