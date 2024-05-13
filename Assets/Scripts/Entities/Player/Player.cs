using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : Entity
{
    [HideInInspector] public static PlayerInputs _playerInputsAction;
    private PlayerMovement _playerMovement;
    private PlayerItemsInteract _playerItemsInteract;
    
    [Header("Camera")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _cameraThreshhold = 5f;

    [Header("Hand movement")] 
    [SerializeField] private GameObject _playerHand;
    [SerializeField] private float _smoothness = 10f;
    [SerializeField] private float _threshold = 2f;

    private void Awake()
    {
        _playerInputsAction = new PlayerInputs();
        _playerInputsAction.Player.Enable();

        _playerMovement = new PlayerMovement(GetComponent<Rigidbody2D>(), 
            _playerCamera, _playerHand);
        _playerItemsInteract = new PlayerItemsInteract(CurrentWeapon);

        _playerMovement.AttachWeaponToHand(CurrentWeapon);
    }
    public void Update()
    {
        _playerMovement.ProcessPlayerMovement(WalkSpeed);
        _playerMovement.ProcessHandMovement(_smoothness, _threshold);
        _playerMovement.ProcessWeaponRotation(CurrentWeapon);
        _playerMovement.ProcessCameraMovement(_cameraThreshhold);
    }
}
