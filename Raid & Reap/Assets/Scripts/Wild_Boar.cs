using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wild_Boar : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public Transform moveSpot;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float detectionRange; 
    public float attackRange;   
    private Transform player;    

    private bool isHostile = false; 
    private bool isAttacking = false; 

    private void Start()
    {
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));


        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (isHostile)
        {
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else if (distanceToPlayer <= detectionRange)
            {
                ChasePlayer();
            }
            else
            {
                StopBeingHostile();
            }
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                waitTime = startWaitTime;
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            Debug.Log("Boar is attacking the player!");
            // Add attack logic reduce player's health
            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f); 
        isAttacking = false;
    }

    public void BecomeHostile()
    {
 
        isHostile = true;
        Debug.Log("Boar has become hostile!");
    }

    private void StopBeingHostile()
    {
       
        isHostile = false;
        Debug.Log("Boar has calmed down and returned to neutral.");
    }

    private void OnDrawGizmosSelected()
    {
        // Visual guide para sa range 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            // player's attack triggers this collision
            // Replace this with proper check if the player has attacked
            BecomeHostile();
        }
    }
}
