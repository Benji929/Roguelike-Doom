using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaingun : Weapon
{
    protected float timeFireHeld; //length of time the fire button as been held down for

    //how long the fire button must be held down before it starts firing
    [SerializeField] protected float startupTime;

    //how long it takes the chaingun to get to maxfiring rate
    [SerializeField] protected float timeButtonHeldForMaxFiringRate;

    //firing delay when the chaingun starts shooting
    [SerializeField] protected float initialFireDelay;

    //firing delay when chaingun is at full speed
    protected float finalFireDelay;

    protected override void OnEnable()
    {
        base.OnEnable();
        timeFireHeld = 0;
        finalFireDelay = fireDelay;
    }

    protected override void CalculateFireTimeAndFire()
    {
        if (Input.GetMouseButton(0))
        {
            timeFireHeld += Time.deltaTime;

            //calculate fireDelay based on timeFireHeld
            if (timeFireHeld >= timeButtonHeldForMaxFiringRate)
            {
                fireDelay = finalFireDelay;
            }
            else if (timeFireHeld >= startupTime)
            {
                //at startupTime, fireDelay = initialFireDelay
                //at timeButtonHeldForMaxFiringRate, fireDelay = finalFireDelay;
                fireDelay = initialFireDelay + (timeFireHeld - startupTime) / (timeButtonHeldForMaxFiringRate - startupTime) * (finalFireDelay - initialFireDelay);
            }
            else
            {
                return;
            }

            //fire the weapon after fireDelay seconds
            if (Time.time >= fireDelayCounter)
            {
                FireWeapon();
                fireDelayCounter = Time.time + fireDelay;
            }
        }
        else
        {
            timeFireHeld = 0;
        }
        
        
    }
}
