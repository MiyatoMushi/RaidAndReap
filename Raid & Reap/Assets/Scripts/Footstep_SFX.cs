using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep_SFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footstepWood;
    public AudioClip footstepStone;
    public AudioClip footstepSoil;
    public AudioClip footstepGrass;

    //For changing Pitch / Volume, para maging Dynamic medyo ung sounds
    public float pitchVariation = 0.1f;
    public float volumeVariation = 0.1f;

    private GameObject player;
    private Rigidbody2D playerRigidbody;
    private bool playerInTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger && playerRigidbody != null)
        {
            // Check if the player is moving
            if (playerRigidbody.velocity.magnitude > 0.1f) // Adjust threshold as needed
            {
                if (!audioSource.isPlaying)
                {
                    PlayDynamicFootstep();
                }
            }
            else
            {
                // Stop the sound when the player is not moving
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
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
                audioSource.Stop(); // Stop sound when player leaves the area
            }
        }
    }

    void PlayDynamicFootstep()
    {
        // Adjust pitch and volume dynamically
        audioSource.pitch = Random.Range(1.0f - pitchVariation, 1.0f + pitchVariation);
        audioSource.volume = Random.Range(1.0f - volumeVariation, 1.0f);
        audioSource.clip = footstepWood;
        audioSource.loop = true; // Keep the sound looping while moving
        audioSource.Play();
    }
}
