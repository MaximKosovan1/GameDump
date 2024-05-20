using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemsInteract
{
    private PlayerInputs _playerInput;
    private Weapon _currentWeapon;
    
    public PlayerItemsInteract(Weapon currentWeapon)
    {
        _playerInput = Player._playerInputsAction;
        UpdateCurrentWeapon(currentWeapon);
        _playerInput.Player.Attack.performed += UseWeapon;
    }
    
    private void UseWeapon(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _currentWeapon != null)
        {
            _currentWeapon.Attack();
        }
    }

    public void UpdateCurrentWeapon(Weapon newWeapon)
    {
        _currentWeapon = newWeapon;
    }
}