using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField] private Player _playerStats;
    [SerializeField] private Image _gunContainer;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private TextMeshProUGUI _hpCount;

    private void Start()
    {
        UpdateHealthUI(_playerStats.CurrentHealthPoints, _playerStats.MaxHealthPoints);
        _playerStats.OnHealthChanged += UpdateHealthUI;
        _playerStats.OnWeaponChanged += UpdateGunSprite;
    }

    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        _hpBar.value = (float)currentHealth / maxHealth;
        _hpCount.text = $"{currentHealth}/{maxHealth}";
    }
    
    private void UpdateGunSprite(Weapon oldWeapon, Weapon newWeapon)
    {
        if(!newWeapon) return;
        var sprite = newWeapon.GetComponent<SpriteRenderer>().sprite;
        _gunContainer.sprite = sprite;
    }

    private void OnDestroy()
    {
        _playerStats.OnHealthChanged -= UpdateHealthUI;
    }
}