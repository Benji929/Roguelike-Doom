using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    protected float health;
    protected float maxHeath;

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
        objectPooler = GameManager.instance.GetComponent<ObjectPooler>();
        playerController = FindObjectOfType<PlayerController>();
    }

    protected virtual void Move()
    {
        
    }

    protected virtual void Patrol()
    {
        //while
               // aiDestinationSetter.targetPosition = 
    }

    protected virtual void SetNewPatrolDestination()
    {
        //while (gridGraph.)
        {
            Vector2 tempDestination = (Vector2)transformToSetAsPatrolArea.transform.position + new Vector2(Random.Range(-patrolRange, patrolRange), Random.Range(-patrolRange, patrolRange));

        }

    }

    protected abstract void Attack();

    void IDamageable.DamagedByAmount(float damage)
    {
        health -= damage;
    }

    protected virtual void Die()
    {
        //TODO: animation or timer for fade
        objectPooler.DespawnObjectIntoPool(gameObject);
    }


}
