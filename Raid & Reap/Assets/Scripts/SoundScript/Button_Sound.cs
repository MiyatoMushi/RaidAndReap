using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Sound : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}

