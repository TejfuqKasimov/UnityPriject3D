using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject weapon1; // Первое оружие
    [SerializeField] private GameObject weapon2; // Второе оружие
    private GameObject currentWeapon;

    void Start()
    {
        currentWeapon = weapon1; // Устанавливаем первое оружие по умолчанию
        weapon1.SetActive(true);
        weapon2.SetActive(false);
    }

    void Update()
    {
        // Проверка нажатия клавиши для переключения оружия
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        // Переключение между оружиями
        if (currentWeapon == weapon1)
        {
            currentWeapon.SetActive(false);
            currentWeapon = weapon2;
            currentWeapon.SetActive(true);
        }
        else
        {
            currentWeapon.SetActive(false);
            currentWeapon = weapon1;
            currentWeapon.SetActive(true);
        }
    }
}