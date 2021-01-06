using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon
{
    protected override void CalculateFireTimeAndFire()
    {
        if (Time.time >= fireDelayCounter)
        {
            //assult rifle keeps firing as long as mouse button is pressed down
            if (Input.GetMouseButton(0)) 
            {
                FireWeapon();
                fireDelayCounter = Time.time + fireDelay;
            }
        }
    }
}
