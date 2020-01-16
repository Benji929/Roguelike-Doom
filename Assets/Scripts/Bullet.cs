using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float Damage;
    public float Lifetime;
    public float linearDrag;

    protected Rigidbody2D rb;
    protected ObjectPooler objectPooler;

    public Vector2 TravelDir;
    [SerializeField] protected float bulletScale = 1;
    public float Speed;

    protected virtual void Awake()
    {
        objectPooler = GameManager.instance.gameObject.GetComponent<ObjectPooler>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        //set drag of bullet
        rb.drag = linearDrag;

        //resize the bullet based in bulletScale
        transform.localScale = new Vector3(1f, 1f, 1f) * bulletScale;

        //set bullet to face the direction of travel
        transform.up = TravelDir;

        //set velocity of bullet
        rb.velocity = TravelDir * Speed;
    }

    protected virtual void DespawnBullet()
    {
        objectPooler.DespawnObjectIntoPool(gameObject);
    }

    protected virtual void Update()
    {
        Lifetime -= Time.deltaTime;

        if (Lifetime <= 0)
        {
            DespawnBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null || collision.GetComponent<PlayerController>() != null)
        {
            return;
        }

        if (collision.GetComponent<IDamageable>() != null)
        {
            collision.GetComponent<IDamageable>().DamagedByAmount(Damage);
            
        }

        DespawnBullet();
    }
}
