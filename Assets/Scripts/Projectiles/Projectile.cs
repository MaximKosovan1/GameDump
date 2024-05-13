using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed = 1f;
    
    private int _damage = 1;
    private Rigidbody2D _bulletRigidbody2D;
    private void Awake()
    {
        _bulletRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        _bulletRigidbody2D.velocity = (transform.right) * _projectileSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       var isEnemy = other.TryGetComponent(out Enemy enemy);
       if (isEnemy)
       {
           enemy.TakeDamage(_damage);
       }
       ProcessImpact();
    }

    private void ProcessImpact()
    {
        Destroy(gameObject);
    }
}
