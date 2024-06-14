using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider2D))]
[ExecuteAlways]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private Enemy[] enemyPool;
    private Room _currentRoom;
    private List<Enemy> _activeEnemies = new List<Enemy>();
    private bool _hasActivated = false;

    private void Awake()
    {
        _currentRoom = GetComponent<Room>();
    }

    private string playerTag = "Player";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasActivated || other.CompareTag(playerTag) == false) return;
        _hasActivated = true; 
        ActivateDoors(false);
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            yield return StartCoroutine(SpawnEnemies(waves[i]));
            yield return new WaitUntil(() => _activeEnemies.Count == 0);
        }
        ActivateDoors(true);
    }

    private IEnumerator SpawnEnemies(Wave wave)
    {
        foreach (var spawnPoint in wave.spawnPoints)
        {
            var enemy = Instantiate(GetRandomEnemy(), 
                (Vector2)_currentRoom.transform.position + (Vector2)spawnPoint, Quaternion.identity);
            _activeEnemies.Add(enemy);
            enemy.OnDeath += HandleEnemyDeath;
            yield return new WaitForSeconds(wave.enemieSpawnDelay);
        }
    }

    private Enemy GetRandomEnemy()
    {
        return enemyPool[Random.Range(0, enemyPool.Length)];
    }
    private void HandleEnemyDeath(Enemy enemy)
    {
        _activeEnemies.Remove(enemy);
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
            if (wave.spawnPoints == null)
                continue;

            Gizmos.color = Color.red;
            foreach (var spawnPoint in wave.spawnPoints)
            {
                if (spawnPoint != null)
                {
                    Gizmos.DrawSphere(spawnPoint, 0.5f);
                }
            }
        }
    }
}
