using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Rooms")]
    [SerializeField] private GameObject[] _corridor;
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

    private Queue<GameObject> _roomsForGeneration = new Queue<GameObject>();
    private void Awake()
    {
        GenerateDungeon();
    }

    public void GenerateDungeon()   
    {
        SetupSpawnRoom();
        int genericRoomsCount = Random.Range(_minRoomsCount, _maxRoomsCount + 1);
        while (genericRoomsCount > 0)
        {
            GenerateRoomRoots(_roomsForGeneration.Peek());
            _roomsForGeneration.Dequeue();
            genericRoomsCount--;
        }
    }

    private void SetupSpawnRoom()
    { 
        var spawnRoom = Instantiate(_spawnRoom, Vector2.zero, Quaternion.identity);
        GenerateRoomRoots(spawnRoom);
    }
    private bool _isRoomGenerated;
    private void GenerateRoomRoots(GameObject originRoom)
    {
        List<GameObject> corridors = GenerateCorridors(originRoom);
            foreach (var corridor in corridors)
            {
                _isRoomGenerated = false;
                while (_isRoomGenerated == false)
                {
                    GenerateRoomFromCorridor(corridor, GetRandomRoom(_genericRooms));
                }
            }
    }

    private GameObject GetRandomRoom(GameObject[] rooms)
    {
        return rooms[Random.Range(0, rooms.Length)];
    }

    private void AlignRoomPosition(Door doorPoint, Room generatedRoom, int index)
    {
        Vector2 position = doorPoint.transform.position + 
                           (generatedRoom.Doors[index].transform.position * (-1));
        generatedRoom.transform.position = position;
        doorPoint._isAvailable = false;
        generatedRoom.Doors[index]._isAvailable = false;
    }
    private bool HasNedeedDoor(DoorDirection neededDoorDirection, Room room)
    {
        foreach (var door in room.Doors)
        {
            if (door._doorDirection == neededDoorDirection) return true;
        }
        return false;
    }
    private void GenerateRoomFromCorridor(GameObject originCorridor, GameObject roomToGenerate)
    {
        var originalCorridor = originCorridor.GetComponent<Room>();
        Door availableDoor = null;
        int i;
        for (i = 0; i < originalCorridor.Doors.Length; i++)
        {
            if (originalCorridor.Doors[i]._isAvailable)
            {
                availableDoor = originalCorridor.Doors[i];
                break;
            }
        }
        if(availableDoor == null) return;
        DoorDirection neededNextDoorDirection = availableDoor.GetOppositeDirection();
        var generatedRoom = Instantiate(roomToGenerate, Vector2.zero, Quaternion.identity).GetComponent<Room>();
        for (int j = 0; j < generatedRoom.Doors.Length; j++)
        {
            if (generatedRoom.Doors[j]._doorDirection == neededNextDoorDirection)
            {
                AlignRoomPosition(availableDoor, generatedRoom, j);
                generatedRoom.Doors[j]._isAvailable = false;
                originalCorridor.Doors[i]._isAvailable = false;
                _isRoomGenerated = true;

                _roomsForGeneration.Enqueue(generatedRoom.gameObject);
                return;
            }
        }
        _isRoomGenerated = false;
        Destroy(generatedRoom.gameObject);
    }
    

    private List<GameObject> GenerateCorridors(GameObject originRoom)
    {
        List<GameObject> generatedCorridors = new List<GameObject>();
        var originalRoom = originRoom.GetComponent<Room>();
        for (int i = 0; i < originalRoom.Doors.Length; i++)
        {
            if(originalRoom.Doors[i]._isAvailable == false) continue;
            DoorDirection neededNextDoorDirection = originalRoom.Doors[i].GetOppositeDirection();
            var randomCorridor = GetRandomRoom(_corridor).GetComponent<Room>();
            while (HasNedeedDoor(neededNextDoorDirection, randomCorridor) == false)
            {
                randomCorridor = GetRandomRoom(_corridor).GetComponent<Room>();
            }

            var generatedCorridor = Instantiate(randomCorridor, Vector2.zero, Quaternion.identity).GetComponent<Room>();
            for (int j = 0; j < generatedCorridor.Doors.Length; j++)
            {
                if (generatedCorridor.Doors[j]._doorDirection == neededNextDoorDirection)
                {
                    AlignRoomPosition(originalRoom.Doors[i], generatedCorridor, j);
                    generatedCorridor.Doors[j]._isAvailable = false;
                    originalRoom.Doors[i]._isAvailable = false;
                    generatedCorridors.Add(generatedCorridor.gameObject);
                }
                else
                {
                    Destroy(generatedCorridor);
                }
            }
        }

        return generatedCorridors;
    }
}
