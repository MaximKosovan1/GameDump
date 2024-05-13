using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Rooms")]
    [SerializeField] private GameObject _spawnRoom;
    [SerializeField] private GameObject _preBossRoom;
    [SerializeField] private GameObject _bossRoom;
    [SerializeField] private GameObject _exitRoom;
    [Space(20)] 
    [SerializeField] private GameObject[] _genericRooms;

    [Header("Generate configuration")] 
    [SerializeField, Min(6)] private int _maxRoomsCount = 6;
    [SerializeField, Min(6)] private int _minRoomsCount = 6;
    [SerializeField, Min(3)] private int _corridorsLength = 5;
    public void GenerateDungeon()
    {
        SetupSpawnRoom();
    }

    private void SetupSpawnRoom()
    { 
        Instantiate(_spawnRoom, Vector2.zero, Quaternion.identity);
    }
}
