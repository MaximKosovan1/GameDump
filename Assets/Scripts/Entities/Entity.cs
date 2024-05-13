using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [field: SerializeField] protected int HealthPoints {get; set;} = 1;
    [field: SerializeField] protected float WalkSpeed {get; set;} = 1;
    [field: SerializeField] protected Weapon CurrentWeapon { get; set; }
}
