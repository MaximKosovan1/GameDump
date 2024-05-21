using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [field: SerializeField] public int CurrentHealthPoints 
    { 
        get => _currentHealthPoints; 
        set
        {
            _currentHealthPoints = value;
            OnHealthChanged?.Invoke(_currentHealthPoints, MaxHealthPoints);
        }
    }

    public int MaxHealthPoints 
    { 
        get => _maxHealthPoints; 
        set
        {
            _maxHealthPoints = value;
            OnHealthChanged?.Invoke(CurrentHealthPoints, _maxHealthPoints);
        }
    }
    public Weapon CurrentWeapon 
    { 
        get => _currentWeapon; 
        set
        {
            OnWeaponChanged?.Invoke(_currentWeapon, value);
            _currentWeapon = value;
        }
    }

    [field: SerializeField] public float WalkSpeed { get; set; } = 5;

    private Weapon _currentWeapon;
    private int _currentHealthPoints = 3;
    private int _maxHealthPoints = 3;

    public event System.Action<int, int> OnHealthChanged;
    public event System.Action<Weapon, Weapon> OnWeaponChanged;
}