using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBullet : Bullet
{
    public Vector2 travelDir;
    private Rigidbody2D rb;
    [SerializeField] private float bulletScale;
    public float speed;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    protected void OnEnable()
    {
        transform.localScale = new Vector3(1f, 1f, 1f) * bulletScale;

        rb.velocity = travelDir * speed;
        transform.up = travelDir;
    }
}
