using UnityEngine;
using System.Collections;

public enum NPCType{
    Slime,
    Boar
}

public class EnemyProximitySound : MonoBehaviour
{
     public NPCType npcType;
    private Transform player; // Reference to the player's Transform
    public float detectionRange = 5f; // Range within which the sound will play
    public AudioClip soundClip; // The sound to play
    private AudioSource audioSource; // AudioSource component
    private bool isInRange = false; // Tracks if the player is within range
    private Vector3 lastPosition; // NPC's position in the previous frame
    

    void Start()
    {
        // Dynamically find the player using the "Player" tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Ensure the player GameObject is tagged as 'Player'.");
        }

        // Add or get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set up the AudioSource
        audioSource.clip = soundClip;
        audioSource.loop = true; // Enable looping
        audioSource.playOnAwake = false; // Prevent the sound from playing at the start

        // Initialize the NPC's last position
        lastPosition = transform.position;
    }

    void Update()
    {
        if (player == null || soundClip == null) return;

        // Check if the player is within range
        float distance = Vector2.Distance(transform.position, player.position);
        isInRange = distance <= detectionRange;

        // Check if the NPC is moving
        lastPosition = transform.position;

        // Manage sound based on proximity and movement
        if (isInRange)
        {
            switch (npcType)
            {
                case NPCType.Slime:
                    HandleSound(PlayerStats.slimeIsMoving);
                    break;

                case NPCType.Boar:
                    HandleSound(PlayerStats.boarIsMoving);
                    break;
            }
        }
        else
        {
            HandleSound(false);
        }
    }

    void HandleSound(bool isMoving)
    {
        if (isMoving)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.loop = true; // Enable looping while moving
                audioSource.Play();
            }
        }
        else
        {
            if (!isMoving)
            {
                audioSource.loop = false; // Allow the clip to finish naturally
            }
        }
    }
}