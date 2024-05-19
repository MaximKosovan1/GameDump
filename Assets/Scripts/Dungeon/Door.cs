using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    public DoorDirection _doorDirection;
    [HideInInspector] public bool _isAvailable = true;
    private bool isOpen = true;
    private Collider2D doorCollider;

    private void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
        IsOpen = isOpen;
    }

    [field: SerializeField] public bool IsOpen
    {
        get => isOpen;
        set
        {
            isOpen = value;
            if (isOpen)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }

    public DoorDirection GetOppositeDirection()
    {
        switch (_doorDirection)
        {
            case DoorDirection.Top:
                return DoorDirection.Bottom;
            case DoorDirection.Bottom:
                return DoorDirection.Top;
            case DoorDirection.Left:
                return DoorDirection.Right;
            case DoorDirection.Right:
                return DoorDirection.Left;
        }

        return DoorDirection.Top;
    }

    public void Open()
    {
        doorCollider.enabled = false;
        Debug.Log("Door opened"); // Додано для перевірки
    }

    public void Close()
    {
        doorCollider.enabled = true;
        Debug.Log("Door closed"); // Додано для перевірки
    }
}

public enum DoorDirection
{
    Top,
    Bottom,
    Left,
    Right
};