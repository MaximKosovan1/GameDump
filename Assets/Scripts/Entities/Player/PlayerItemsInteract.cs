using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemsInteract
{
    private PlayerInputs _playerInput { get; set; }
    private Weapon _currentWeapon;
    
    public PlayerItemsInteract(Weapon currentWeapon)
    {
        _playerInput = Player._playerInputsAction;
        _currentWeapon = currentWeapon;
        _playerInput.Player.Attack.performed += UseWeapon;
    }
    
    private void UseWeapon(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _currentWeapon.Attack();
        }
    }
}