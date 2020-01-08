using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaingun : Weapon
{
    protected override void CalculateFireTime()
    {
        if (Time.time >= base.fireDelayCounter)
        {
            if (Input.GetMouseButton(0)) //chaingun keeps firing as long as mouse button is pressed down
            {
                FireWeapon();
                fireDelayCounter = Time.time + fireDelay;
            }
        }
    }
}
