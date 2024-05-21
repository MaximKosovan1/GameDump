using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : Entity
{
    [HideInInspector] public static PlayerInputs _playerInputsAction;
    private PlayerMovement _playerMovement;
    private PlayerInteract _playerInteract;
    private PlayerItemsInteract _playerItemsInteract;
    private Rigidbody2D _rigidbody;
    public static bool _isPaused;

    [Header("Camera")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _cameraThreshhold = 5f;

    [Header("Hand movement")]
    [SerializeField] private GameObject _playerHand;
    [SerializeField] private float _smoothness = 10f;
    [SerializeField] private float _threshold = 2f;

    [Header("Interaction")]
    [SerializeField] private float _radius = 1f;
    [SerializeField] private LayerMask _interactableLayer;

    private MenuManager _menuManager;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInputsAction = new PlayerInputs();
        _playerInputsAction.Player.Enable();

        _playerMovement = new PlayerMovement(_rigidbody, _playerCamera, _playerHand);
        _playerInteract = gameObject.AddComponent<PlayerInteract>();
        _playerInteract.Initialize(_interactableLayer, _radius);
        _playerItemsInteract = new PlayerItemsInteract(CurrentWeapon);
        OnWeaponChanged += _playerItemsInteract.UpdateCurrentWeapon;

        _menuManager = FindObjectOfType<MenuManager>();
    }

    public void Update()
    {
        if (_isPaused) return;

        _playerMovement.ProcessPlayerMovement(WalkSpeed);
        _playerMovement.ProcessHandMovement(_smoothness, _threshold);
        _playerMovement.ProcessWeaponRotation(CurrentWeapon);
        _playerMovement.ProcessCameraMovement(_cameraThreshhold);
        _playerInteract.CheckForInteractable();
    }

    public void TakeDamage(int damage, Vector2 forceDirection)
    {
        if (_isPaused) return;

        CurrentHealthPoints -= damage;
        if (CurrentHealthPoints <= 0)
        {
            CurrentHealthPoints = 0;
            ProcessDeath();
        }
        else
        {
            ApplyForce(-forceDirection);
        }
    }

    private void ApplyForce(Vector2 forceDirection)
    {
        _rigidbody.AddForce(forceDirection, ForceMode2D.Impulse);
    }

    private void ProcessDeath()
    {
        _menuManager.ShowDeathMenu();
    }

    public static void SetPaused(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
