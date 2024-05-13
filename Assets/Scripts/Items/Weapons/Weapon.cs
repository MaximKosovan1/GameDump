using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : Items
{
    [Header("Stats")]
    [SerializeField] private float _attackCooldown = 0.5f;
    
    [Header("Projectiles")]    
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Projectile[] _weaponProjectiles;

    private bool _isCooldownEnded = true; 

    private int _currentProjectile = 0;
    private int CurrentProjectile
    {
        get => _currentProjectile;
        set
        {
            if (value < 0)
                throw new ApplicationException("Current projectile cannot be less than zero!!!");
            else if (value > (_weaponProjectiles.Length - 1))
                _currentProjectile = 0;
        }
    }
    public void Attack()
    {
        if (_isCooldownEnded == false) return;
        var bullet = Instantiate(_weaponProjectiles[_currentProjectile], _firePoint.position, _firePoint.rotation);
        CurrentProjectile++;
        StartCoroutine(IsCooldownEnded());
    }
    private IEnumerator IsCooldownEnded()
    {
        _isCooldownEnded = false;
        yield return new WaitForSeconds(_attackCooldown);
        _isCooldownEnded = true;
    }
}
