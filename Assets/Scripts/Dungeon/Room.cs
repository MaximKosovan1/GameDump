using UnityEngine;

public class Room : MonoBehaviour
{
    public Door[] Doors;

    public Transform GetRandomCorridorPosition()
    {
        return Doors[Random.Range(0, Doors.Length-1)].transform;
    }
}
