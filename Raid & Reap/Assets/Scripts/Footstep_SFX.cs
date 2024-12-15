using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep_SFX : MonoBehaviour
{
    public AudioSource audioSource;

    // For dynamic sound variation
    public float pitchVariation = 0.1f;
    public float volumeVariation = 0.1f;

    private GameObject player;
    private Rigidbody2D playerRigidbody;
    private bool playerInTrigger = false;

    void Start()
    {
        // Find player by tag and get Rigidbody2D component
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player with tag 'Player' not found!");
        }
        else
        {
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            if (playerRigidbody == null)
            {
                Debug.LogError("Player does not have a Rigidbody2D component!");
            }
        }

        // Validate AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
        }
    }

    void Update()
    {
        if (playerInTrigger && playerRigidbody != null && audioSource != null)
        {
            // Check if player is moving
            if (playerRigidbody.velocity.magnitude > 0.1f) // Adjust threshold as needed
            {
                if (!audioSource.isPlaying)
                {
                    PlayDynamicFootstep();
                }
                audioSource.UnPause(); // Resume playback if paused
            }
            else
            {
                // Pause sound when player stops moving
                if (audioSource.isPlaying)
                {
                    audioSource.Pause();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true; // Player is within the trigger area
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false; // Player has left the trigger area
            if (audioSource.isPlaying)
            {
                audioSource.Pause(); // Pause sound when player leaves the area
            }
        }
    }

    void PlayDynamicFootstep()
    {
        // Adjust pitch and volume dynamically
        audioSource.pitch = Random.Range(1.0f - pitchVariation, 1.0f + pitchVariation);
        audioSource.volume = Random.Range(1.0f - volumeVariation, 1.0f);
        audioSource.loop = true; // Ensure looping is enabled
        audioSource.Play(); // Start playback
    }
}
