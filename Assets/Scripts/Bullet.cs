using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float Damage;

    protected ObjectPooler objectPooler;

    protected virtual void Awake()
    {
        objectPooler = GameManager.instance.gameObject.GetComponent<ObjectPooler>();
    }

    protected virtual void DespawnBullet()
    {
        objectPooler.DespawnObjectIntoPool(gameObject);
    }
}
