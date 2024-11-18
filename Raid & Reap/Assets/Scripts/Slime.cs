using System.Collections;
using System.Collections.Generic;
using UnityEditor.AdaptivePerformance.Editor.Metadata;
using UnityEngine;

public class Slime : MonoBehaviour
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
    private bool isAttacking = false;
    public Animator animator;


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
        Vector3 direction = Vector3.zero; 

        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
            direction = player.position - transform.position; 
        }
        else if (distanceToPlayer <= detectionRange)
        {
            direction = player.position - transform.position; 
            ChasePlayer();
        }
        else
        {
            direction = moveSpot.position - transform.position; 
            Patrol();
        }

        AnimateMovement(direction.normalized); 
    }

   


    void AnimateMovement(Vector3 direction) {
        if (animator != null) {
            if (direction.magnitude > 0)
            {
                animator.SetBool("IsMoving", true);
                animator.SetFloat("Horizontal", direction.x);
                animator.SetFloat("Vertical", direction.y);
            }
            else {
                animator.SetBool("IsMoving", false);
            }
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
            Debug.Log("Slime is attacking the player!");
            // Add attack logic reduce player's health
            PlayerStats.PlayerHealth -= 10;
            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f); 
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Visual guide para sa range 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
