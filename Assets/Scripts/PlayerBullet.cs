using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public Vector2 TravelDir;
    private Rigidbody2D rb;
    [SerializeField] private float bulletScale = 1;
    public float Speed;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }
    protected void OnEnable()
    {
        transform.localScale = new Vector3(1f, 1f, 1f) * bulletScale;

        transform.up = TravelDir;
        rb.velocity = TravelDir * Speed;
    }
}
