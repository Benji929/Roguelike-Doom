using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotgunBullet : PlayerBullet
{
    private float tempBulletScale;
    private float tempMaxLifetime;

    protected override void OnEnable()
    {
        base.OnEnable();
        //temp bullet scale used so object retains original bullet scale when enabled from pool
        tempBulletScale = bulletScale;
        tempMaxLifetime = Lifetime;
    }

    protected override void Update()
    {
        base.Update();
        ShrinkBulletUntilLifetimeOver();
    }

    /// <summary>
    /// bullet shrinks, when lifetime = 0, local scale will also be 0
    /// </summary>
    protected virtual void ShrinkBulletUntilLifetimeOver()
    {
        tempBulletScale = Lifetime / tempMaxLifetime;
        transform.localScale = new Vector3(1f, 1f, 1f) * tempBulletScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamageable>() != null)
        {
            collision.GetComponent<IDamageable>().DamagedByAmount(Damage);

        }

        DespawnBullet();
    }
}
