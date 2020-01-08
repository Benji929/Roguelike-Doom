using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireDelay; //minimum delay between each bullet fired
    protected float fireDelayCounter = 0; 
    public float Damage;
    public float BulletSpeed;

    private Camera cam;
    private Vector2 target; //mouse position in world space
    private Vector2 travelDir; //direction in which bullet(s) will be fired in

    public ObjectPooler ObjectPooler;
    [SerializeField] protected Pool playerBulletPool; //pool information for player's bullet that spawns/despawns
    private PlayerController playerController;

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
        PlayerBullet bulletPlayerBulletScript = bullet.GetComponent<PlayerBullet>();
        bulletPlayerBulletScript.TravelDir = travelDir;
        bulletPlayerBulletScript.Speed = BulletSpeed;

        //pass values to the object before activating (because values are initialized in OnEnable())
        ObjectPooler.SpawnPooledObject(playerBulletPool.tag, transform.position, Quaternion.LookRotation(travelDir), bullet);
        
    }
}
