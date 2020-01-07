using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireDelay;
    protected float fireDelayCounter = 0;
    public float Damage;
    public float BulletSpeed;

    private Camera cam;
    private Vector2 target;
    private Vector2 travelDir;

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

    // Update is called once per frame
    protected virtual void Update()
    {
        //Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        target = cam.ScreenToWorldPoint(Input.mousePosition);
        travelDir = new Vector2(target.x - playerController.transform.position.x, target.y - playerController.transform.position.y);
        travelDir.Normalize();

        CalculateFireTime();
    }

    protected virtual void CalculateFireTime()
    {
        if (Time.time >= fireDelayCounter)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FireWeapon();
                fireDelayCounter += fireDelay;
            }
        }
    }

    protected virtual void FireWeapon()
    {
        GameObject bullet = ObjectPooler.GetPooledObject(playerBulletPool.tag);
        PlayerBullet bulletPlayerBulletScript = bullet.GetComponent<PlayerBullet>();
        bulletPlayerBulletScript.TravelDir = travelDir;
        bulletPlayerBulletScript.Speed = BulletSpeed;
        ObjectPooler.SpawnPooledObject(playerBulletPool.tag, transform.position, Quaternion.LookRotation(travelDir), bullet);
        
    }
}
