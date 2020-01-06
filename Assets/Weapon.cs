using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float _fireDelay;
    public float damage;
    public float bulletSpeed;

    private Camera cam;
    private Vector2 target;
    private Vector2 travelDir;

    public ObjectPooler objectPooler;
    protected Pool playerBulletPool; //pool information for player's bullet that spawns/despawns

    private void Awake()
    {
        objectPooler = GameManager.instance.GetComponent<ObjectPooler>();
    }

    private void OnEnable()
    {
        cam = Camera.main;
        Vector3 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        target = new Vector2(worldPoint.x, worldPoint.y);
        travelDir = target - (Vector2)FindObjectOfType<PlayerController>().transform.position;
    }

    protected virtual void FireWeapon()
    {
        GameObject bullet = objectPooler.SpawnPooledObject(playerBulletPool.tag, transform.position, Quaternion.LookRotation(travelDir));
        PlayerBullet bulletPlayerBulletScript = bullet.GetComponent<PlayerBullet>();
        bulletPlayerBulletScript.travelDir = travelDir;
        bulletPlayerBulletScript.speed = bulletSpeed;
    }
}
