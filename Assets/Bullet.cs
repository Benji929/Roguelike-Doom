using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float damage;

    protected ObjectPooler objectPooler;

    protected virtual void Awake()
    {
        objectPooler = GameManager.instance.GetComponent<ObjectPooler>();
    }

    protected virtual void DespawnBullet()
    {
        objectPooler.DespawnObjectIntoPool(gameObject);
    }
}
