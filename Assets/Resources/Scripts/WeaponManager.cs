using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int weaponEquippedIndex; //index of weapon equipped
    public List<Weapon> weaponList = new List<Weapon>(); //list of all weapons

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
            //if weapon equipped is at end of the list (for out of bounds exception)
            SwapWeapon(weaponList[0]);
            weaponEquippedIndex = 0;
        }
        else
        {
            //increment weaponEquippedIndex by 1 and swaps to that weapon;
            SwapWeapon(weaponList[weaponEquippedIndex + 1]);
            weaponEquippedIndex += 1;
        }
    }

    private void SwapWeapon(Weapon weapon)
    {
        //case where weapon passed in is already equipped
        if (weaponList[weaponEquippedIndex] == weapon)
        {
            return;
        }

        //disable all weapon scripts
        foreach (Weapon w in weaponList)
        {
            w.enabled = false;
        }

        //enable the weapon script that is passed in
        weapon.enabled = true;
    }
}
