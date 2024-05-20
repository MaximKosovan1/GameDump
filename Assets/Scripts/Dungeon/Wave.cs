using System;
[Serializable]
public class Wave
{
    public string WaveName;
    public EnemySpawnPoint[] Enemies;
    public float enemieSpawnDelay = 0.25f;
}
