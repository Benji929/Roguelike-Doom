using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireDelay; //minimum delay between each bullet fired
    protected float fireDelayCounter = 0; 
    public float Damage;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float bulletLifeTime = 5;
    [SerializeField] protected float bulletDrag;

    private Camera cam;
    protected Vector2 target; //mouse position in world space
    protected Vector2 travelDir; //direction in which bullet(s) will be fired in

    public ObjectPooler ObjectPooler;
    [SerializeField] protected Pool playerBulletPool; //pool information for player's bullet that spawns/despawns
    protected PlayerController playerController;

    protected virtual void Awake()
    {
        ObjectPooler = GameManager.instance.GetComponent<ObjectPooler>();
        playerController = FindObjectOfType<PlayerController>();
    }

    protected virtual void OnEnable()
    {
        cam = Camera.main;
    }

    protected virtual void Update()
    {
        //travelDir set to normalized vector from player to mouse position
        target = cam.ScreenToWorldPoint(Input.mousePosition);
        travelDir = new Vector2(target.x - playerController.transform.position.x, target.y - playerController.transform.position.y);
        travelDir.Normalize();

        CalculateFireTime();
    }

    /// <summary>
    /// fires weapon after fireDelay seconds and when player clicks the right mouse button
    /// </summary>
    protected virtual void CalculateFireTime()
    {
        if (Time.time >= fireDelayCounter)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FireWeapon();
                fireDelayCounter = Time.time + fireDelay;
            }
        }
    }

    /// <summary>
    /// grabs bullet from object pooler, sets it as active and pass velocity values to the object. 
    /// </summary>
    protected virtual void FireWeapon()
    {
        GameObject bullet = ObjectPooler.GetPooledObject(playerBulletPool.tag);
        Bullet bulletPlayerBulletScript = bullet.GetComponent<Bullet>();
        bulletPlayerBulletScript.TravelDir = travelDir;

        //calculate travelDir component of player velocity
        float thisBulletSpeed;
        thisBulletSpeed = bulletSpeed + playerController.Velocity.magnitude * Mathf.Cos((Mathf.PI * Vector2.Angle(travelDir, playerController.Velocity))/180);

        bulletPlayerBulletScript.Speed = thisBulletSpeed;
        bulletPlayerBulletScript.Lifetime = bulletLifeTime;
        bulletPlayerBulletScript.linearDrag = bulletDrag;

        //pass values to the object before activating (because values are initialized in OnEnable())
        ObjectPooler.SpawnPooledObject(playerBulletPool.tag, transform.position, Quaternion.LookRotation(travelDir), bullet);
    }
}
