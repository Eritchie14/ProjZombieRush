using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Range(0,50)] [SerializeField] float attackRange = 5, sightRange=20, timeBetweenAtacks=1;

    private NavMeshAgent thisEnemy;
    private Transform playerPos;
    private GameObject player;
    private HealthSys playerHealth;
    public NavAgentAnim anim;


    private bool attacking; //is enemy currently attacking 
    private bool isDead;//is the player dead 
    public bool isDropper;
    public float attackdamage = 10;
    public float maxHealth = 100;
    private float currHealth;

    private void Start()
    {
        thisEnemy = GetComponent<NavMeshAgent>(); //enemy AI brain, this is what allows enemy to path
        playerPos = FindFirstObjectByType<HealthSys>().transform; //detect first object on scene that contains a healthSys compentent (script) and stores value
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.transform.Find("HealthSystem").GetComponent<HealthSys>();
        //Debug.Log("Player Position" + playerPos.position);
        currHealth = maxHealth;

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
        //Debug.Log(playerHealth.CurrentHealth);
        //Debug.Log("hurtplayer: " + player.name);

        System.Random rand = new System.Random(); 
        int index = rand.Next(AudioManager.Instance.EnemyAttackSounds.Length);
        AudioManager.Instance.PlayAttack(index); // gets and plays random attack sound from audio manager array
        anim.SetBool("Attacking", true);
        
        
        attacking = false;
        anim.SetBool("Attacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }

    public void damageEnemy(float damage)
    {
        currHealth -= damage;
        if(currHealth <= 0)
        {
            isDead = true;
            Debug.Log("Killed Enemy");
            //TODO: Apply death animation or blood explosion or something to cover enemy despawning here
            Destroy(transform.root.gameObject);
            if(isDropper){
                this.GetComponent<DropItem>().Drop();
            }
        }
        
    }

    public float getHealth()
    {
        return currHealth;
    }
}
