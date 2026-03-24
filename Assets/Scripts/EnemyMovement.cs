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
    private GameObject player;
    private HealthSys playerHealth;

    private bool attacking; //is enemy currently attacking 
    private bool isDead;//is the player dead
    public float attackdamage = 10;

    private void Start()
    {
        thisEnemy = GetComponent<NavMeshAgent>(); //enemy AI brain, this is what allows enemy to path
        playerPos = FindFirstObjectByType<HealthSys>().transform; //detect first object on scene that contains a healthSys compentent (script) and stores value
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.transform.Find("HealthSystem").GetComponent<HealthSys>();
        Debug.Log("Player Position" + playerPos.position);
    }

    private void Update()
    {
        float distanceFromPlayer = Vector3.Distance(playerPos.position, this.transform.position); // distance between player and enemy 
        if (playerHealth.isDead())
        {
            thisEnemy.isStopped = true;
            attacking = false;
        }
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
        }
    }

    private void chasePlayer()
    {
        thisEnemy.SetDestination(playerPos.position);//sets enemy destination to player
    }

    private IEnumerator AttackPlayer()
    {
        attacking = true;
        yield return new WaitForSeconds(timeBetweenAtacks); //wait for time between attacks
        playerHealth.DamagePlayer(attackdamage);
        Debug.Log(playerHealth.CurrentHealth);
        Debug.Log("hurtplayer: " + player.name);
        
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
