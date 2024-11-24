using UnityEngine;

public class Tilemap_SFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip grassFootstep;
    public AudioClip soilFootstep;
    public AudioClip woodFootstep;
    public AudioClip stoneFootstep;

    private Rigidbody2D playerRb;
    private Vector3 previousPosition;
    private bool isPlayerMoving = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        previousPosition = transform.position;
    }

    void Update()
    {
        // Check if the player is moving
        isPlayerMoving = Vector3.Distance(previousPosition, transform.position) > 0.01f;

        if (!isPlayerMoving && audioSource.isPlaying)
        {
            audioSource.Stop(); // Stop the sound if the player is stationary
        }

        previousPosition = transform.position; // Update the previous position
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayerMoving) // Only play sound if the player is moving
        {
            switch (other.tag)
            {
                case "GrassGround":
                    PlayFootstep(grassFootstep);
                    break;
                case "SoilGround":
                    PlayFootstep(soilFootstep);
                    break;
                case "WoodGround":
                    PlayFootstep(woodFootstep);
                    break;
                case "StoneGround":
                    PlayFootstep(stoneFootstep);
                    break;
            }
        }
    }

    private void PlayFootstep(AudioClip clip)
    {
        if (audioSource.clip != clip || !audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}