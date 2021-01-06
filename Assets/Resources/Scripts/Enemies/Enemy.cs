using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public float health;
    public float maxHealth;

    //empty game object with a transform for where this enemy will move
    [SerializeField] protected GameObject transformToSetAsPatrolArea;
    [SerializeField] protected float patrolRange;

    protected ObjectPooler objectPooler;
    protected PlayerController playerController;

    protected IAstarAI ai;
    protected AIDestinationSetter aiDestinationSetter;
    protected AstarPath aStarPath;
    protected GridGraph gridGraph;

    protected virtual void Awake()
    {
        objectPooler = GameManager.singleton.GetComponent<ObjectPooler>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        health = maxHealth;
    }

    protected abstract void Move();

    protected abstract void Attack();

    void IDamageable.DamagedByAmount(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        //TODO: animation or timer for fade
        objectPooler.DespawnObjectIntoPool(gameObject);
    }


}
