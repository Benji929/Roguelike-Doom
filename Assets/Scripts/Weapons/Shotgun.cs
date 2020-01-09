using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [Header("Shotgun Specifics")]
    [SerializeField] protected int numberOfBullets; //number of bullets each time shotgun is fired
    [SerializeField] protected float angleOfBulletSpread; // widest angle at which bullets from the shotgun will travel
    protected float dragOfThisBullet;

    protected override void FireWeapon()
    {
        FireShotgun();
    }

    protected virtual void FireShotgun()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            float angleOfThisBullet = Random.Range(-angleOfBulletSpread/2, angleOfBulletSpread/2);
            FireBulletAtAngle(angleOfThisBullet);
        }
    }

    protected virtual void FireBulletAtAngle(float angleOffset = 0f)
    {
        GameObject bullet = ObjectPooler.GetPooledObject(playerBulletPool.tag);
        Bullet bulletPlayerBulletScript = bullet.GetComponent<Bullet>();

        //add angle offset to this bullet
        Vector2 thisBulletTravelDir = Quaternion.AngleAxis(angleOffset, Vector3.forward) * travelDir;
        
        //variation in drag between bullets to give scattered effect
        dragOfThisBullet = Random.Range(1.5f * bulletDrag, 0.8f * bulletDrag);

        //calculate travelDir component of player velocity
        float thisBulletSpeed;
        thisBulletSpeed = bulletSpeed - playerController.Velocity.magnitude * Mathf.Cos(Vector2.Angle(travelDir, playerController.Velocity));

        bulletPlayerBulletScript.Speed = thisBulletSpeed;
        bulletPlayerBulletScript.TravelDir = thisBulletTravelDir;
        bulletPlayerBulletScript.Lifetime = bulletLifeTime;
        bulletPlayerBulletScript.linearDrag = dragOfThisBullet;

        //pass values to the object before activating (because values are initialized in OnEnable())
        ObjectPooler.SpawnPooledObject(playerBulletPool.tag, transform.position, Quaternion.LookRotation(travelDir), bullet);
    }

}
