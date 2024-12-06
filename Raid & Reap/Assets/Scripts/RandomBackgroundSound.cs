using UnityEngine;

public class RandomBackgroundSound : MonoBehaviour
{
    public AudioClip[] backgroundClips; // Array of audio clips
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (backgroundClips.Length > 0)
        {
            PlayRandomSound();
        }
    }

    private void Update()
    {
        // If the current sound has finished playing, play a new one
        if (!audioSource.isPlaying)
        {
            PlayRandomSound();
        }
    }

    private void PlayRandomSound()
    {
        // Choose a random clip
        AudioClip clip = backgroundClips[Random.Range(0, backgroundClips.Length)];
        
        // Assign and play the clip
        audioSource.clip = clip;
        audioSource.Play();
    }
}
