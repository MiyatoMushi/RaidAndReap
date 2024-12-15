using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    private int damage; // Amount of damage the sword deals
    public string targetTag = "Enemy"; // Tag of objects that can take damage
    public float knockbackDistance = 0.5f; // Force of the knockback
    public float knockbackDuration = 0.2f; // Time it takes to move the opponent
    private AudioSource audioSource;
    public AudioClip swingSound;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        
        audioSource.clip = swingSound;
    }

    public void GetDamage(int damages){
        damage = damages;
        startSwing();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag)) // Check if the collided object has the correct tag
        {
            // Deal damage
            var health = collision.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            // Apply knockback
            var enemyMovement = collision.GetComponent<EnemyKnockback>();
            if (enemyMovement != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                enemyMovement.ApplyKnockback(knockbackDirection, knockbackDistance, knockbackDuration);
            }
        }
    }

    private void startSwing(){
        audioSource.Play();
    }
}
