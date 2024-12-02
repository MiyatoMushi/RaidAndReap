using System.Collections;
using UnityEngine;

public class PlayerHitHandler : MonoBehaviour
{
    [Header("Knockback Settings")]
    public float knockbackForce = 10f; // Knockback force applied to the player
    public float knockbackDuration = 0.2f; // Duration of knockback effect

    [Header("Damage Settings")]
    public int damageAmount = 10; // Amount of health to reduce when hit

    [Header("Audio Settings")]
    public AudioClip hitSound; // Sound effect to play on hit
    private AudioSource audioSource; // Reference to the AudioSource component

    private Rigidbody2D playerRb;

    private void Start()
    {
        // Ensure the player has a Rigidbody2D component
        playerRb = GetComponent<Rigidbody2D>();
        if (playerRb == null)
        {
            Debug.LogError("Player does not have a Rigidbody2D component.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Player does not have an AudioSource component. Adding one...");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void HandleHit(Vector2 hitSourcePosition)
    {
        // Apply damage
        PlayerStats.PlayerHealth -= damageAmount;

         // Play hit sound
        PlayHitSound();

        // Apply knockback
        Vector2 knockbackDirection = (transform.position - (Vector3)hitSourcePosition).normalized;
        StartCoroutine(ApplyKnockback(knockbackDirection));

        Debug.Log("Player hit! Health: " + PlayerStats.PlayerHealth);
    }

    private IEnumerator ApplyKnockback(Vector2 direction)
    {
         if (playerRb != null)
        {
            playerRb.velocity = direction * knockbackForce;

            // Optional: Disable player movement script during knockback
            PlayerMovement playerMovement = playerRb.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            yield return new WaitForSeconds(knockbackDuration);

            // Stop knockback and re-enable movement
            playerRb.velocity = Vector2.zero;
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
        }
    }

    private void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        else
        {
            Debug.LogWarning("Hit sound or AudioSource is missing!");
        }
    }
}
