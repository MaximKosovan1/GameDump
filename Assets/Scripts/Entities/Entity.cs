using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [field: SerializeField] public int HealthPoints {get; set;} = 1;
    [field: SerializeField] public float WalkSpeed {get; set;} = 1;
    [field: SerializeField] public Weapon CurrentWeapon { get; set; }
}
