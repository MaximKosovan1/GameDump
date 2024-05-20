using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SimpleFlash))]
public  class Enemy : Entity
{
    private SimpleFlash _flashEffect;
    public event Action<Enemy> OnDeath;
    private void Awake()
    {
        _flashEffect = GetComponent<SimpleFlash>();
    }

    public void TakeDamage(int damage)
    {
        HealthPoints -= damage;
        _flashEffect.Flash();
        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
            ProcessDeath();
        }
    }

    private void ProcessDeath()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
