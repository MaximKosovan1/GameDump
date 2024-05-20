using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : Entity
{
    [HideInInspector] public static PlayerInputs _playerInputsAction;
    private PlayerMovement _playerMovement;
    private PlayerInteract _playerInteract;
    private PlayerItemsInteract _playerItemsInteract;
    
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

    private void Awake()
    {
        _playerInputsAction = new PlayerInputs();
        _playerInputsAction.Player.Enable();

        _playerMovement = new PlayerMovement(GetComponent<Rigidbody2D>(), 
            _playerCamera, _playerHand);
        _playerInteract = gameObject.AddComponent<PlayerInteract>();
        _playerInteract.Initialize(_interactableLayer, _radius);
        _playerItemsInteract = new PlayerItemsInteract(CurrentWeapon);
    }

    public void Update()
    {
        _playerMovement.ProcessPlayerMovement(WalkSpeed);
        _playerMovement.ProcessHandMovement(_smoothness, _threshold);
        _playerMovement.ProcessWeaponRotation(CurrentWeapon);
        _playerMovement.ProcessCameraMovement(_cameraThreshhold);
        _playerInteract.CheckForInteractable();
    }

    public Weapon CurrentWeapon
    {
        get => _currentWeapon;
        set
        {
            _currentWeapon = value;
            _playerItemsInteract.UpdateCurrentWeapon(_currentWeapon);
        }
    }

    private Weapon _currentWeapon;
}