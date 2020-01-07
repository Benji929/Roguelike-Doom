using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int weaponEquippedIndex;
    public List<Weapon> weaponList = new List<Weapon>();

    private void Awake()
    {
        //scan "Weapons" object for weapon scripts
        Weapon[] weapons = GetComponentsInChildren<Weapon>();

        //add each weapon script to the weaponList and disable each script
        foreach (Weapon w in weapons)
        {
            w.enabled = false;
            weaponList.Add(w);
        }

        //enable the default script
        weaponEquippedIndex = 0;
        weaponList[0].enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapToNextWeapon();
        }
    }

    protected void SwapToNextWeapon()
    {
        if (weaponEquippedIndex == weaponList.Count - 1)
        {
            SwapWeapon(weaponList[0]);
            weaponEquippedIndex = 0;
        }
        else
        {
            SwapWeapon(weaponList[weaponEquippedIndex + 1]);
            weaponEquippedIndex += 1;
        }
    }

    private void SwapWeapon(Weapon weapon)
    {
        if (weaponList[weaponEquippedIndex] == weapon)
        {
            return;
        }

        foreach (Weapon w in weaponList)
        {
            w.enabled = false;
        }

        weapon.enabled = true;
    }
}
