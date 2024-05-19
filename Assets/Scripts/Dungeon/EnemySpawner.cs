using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[ExecuteAlways]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;

    private Room _currentRoom;
    private void Awake()
    {
        _currentRoom = GetComponent<Room>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ActivateDoors(false);
        // Ваш код для обробки події входження в тригер
    }

    private void ActivateDoors(bool isOpen = true)
    {
        for (int i = 0; i < _currentRoom.Doors.Length; i++)
            _currentRoom.Doors[i].IsOpen = isOpen;
    }
    private void OnDrawGizmos()
    {
        if (waves == null || waves.Length == 0)
            return;
        
        for (int i = 0; i < waves.Length; i++)
        {
            var wave = waves[i];
            if (wave.Enemies == null)
                continue;

            Gizmos.color = Color.red;
            foreach (var spawnPoint in wave.Enemies)
            {
                if (spawnPoint != null)
                {
                    Gizmos.DrawSphere(spawnPoint.Position, radius:0.5f);
                }
            }
        }
    }
}
