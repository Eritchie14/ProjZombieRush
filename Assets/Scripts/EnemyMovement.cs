using System;
using System.Collections;
//using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Range(0,50)] [SerializeField] float attackRange = 5, sightRange=20, timeBetweenAtacks=1;

    private NavMeshAgent thisEnemy;
    private Transform playerPos;

    private bool attacking; //is enemy currently attacking 
    private bool isDead;//is the player dead

    private void Start()
    {
        thisEnemy.GetComponent<NavMeshAgent>(); //enemy AI brain, this is what allows enemy to path
        playerPos = FindFirstObjectByType<HealthSys>().transform; //detect first object on scene that contains a healthSys compentent (script) and stores value
    }

    private void Update()
    {
        float distanceFromPlayer = Vector3.Distance(playerPos.position, this.transform.position); // distance between player and enemy 

        if(distanceFromPlayer <= sightRange && distanceFromPlayer > attackRange)
        {
            attacking = false;
            thisEnemy.isStopped = false;
            StopAllCoroutines();
            chasePlayer();
        }

        if(distanceFromPlayer <= attackRange && !attacking)
        {
            thisEnemy.isStopped = true; // enemy stops moving to attack
            StartCoroutine(AttackPlayer()); // enemy starts attacking player
        }Debug.Log("Running Update");
    }

    private void chasePlayer()
    {
        thisEnemy.SetDestination(playerPos.position);//sets enemy destination to player
    }

    private IEnumerator AttackPlayer()
    {
        attacking = true;
        yield return new WaitForSeconds(timeBetweenAtacks); //wait for time between attacks
        Debug.Log("hurtplayer");
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
